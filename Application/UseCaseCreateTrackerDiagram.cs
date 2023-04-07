using SkiaSharp; // TODO: Move to Infrastructure
using Application.Common.Interfaces;
using Application.Device;
using DarkSideDiv.Common;
using System;
using System.Globalization;
using System.Text.RegularExpressions;


namespace Application;

public class UseCaseCreateTrackerDiagram
{
  private IReadTextFile _read_text_file;

  private IDocumentConverter _document_converter;

  public UseCaseCreateTrackerDiagram(IDocumentConverter document_converter, IReadTextFile read_text_file)
  {
    _read_text_file = read_text_file;
    _document_converter = document_converter;
  }

  public void Execute(string input_filename, string output_filename)
  {
    const int WEEKS = 53;
    const int DAYS = 7;
    // WEEKS * DAYS = 371 ... > 366 on a leap year.
    const int YEAR = 2023;

    // Create an image and fill it blue
    SKBitmap bmp = new SKBitmap(3200, 600); // 5::3
    using SKCanvas canvas = new SKCanvas(bmp);
    canvas.Clear(SKColor.Parse("#003366"));

    var device_repo = new DeviceRepoSkia();
    device_repo.TextDeviceSkia = new DsDivComponentAlignedTextSkia();
    device_repo.DivDeviceSkia = new DsDivSkia();
    device_repo.SetCanvas(canvas);


    var pic_rect = new SKRect(0.0f, 0.0f, bmp.Width, bmp.Height);
    var grid_layout_algorithmn = new GridLayoutAlgorithmn();
    var grid_builder = new Builders.DsGridBuilder(
      device_repo,
      grid_layout_algorithmn
    );

    var tracker_builder = new Builders.DsTrackerBuilder(grid_builder);
    tracker_builder.Reset(pic_rect, DAYS, WEEKS, YEAR);

    // Read Markdown File
    var text = _read_text_file.Read(input_filename);
    var lines = text.Split("\n");
    string pattern = "yyyy-MM-dd";

    Regex rx = new Regex(@"([+-]?([0-9]*[.])?[0-9]+)h",
      RegexOptions.Compiled |
      RegexOptions.IgnoreCase
    );

    foreach (var i in lines)
    {
      var datestr_and_text = i.Split("  ");

      var date = datestr_and_text[0].Split(" ");

      if (date.Count() == 3) // Conventional date line
      {
        DateTime parsedDate;
        if (DateTime.TryParseExact(date[0], pattern, null,
                                        DateTimeStyles.None, out parsedDate))
        {
          var content = datestr_and_text[1];
          //Console.WriteLine(content);

          var m = rx.Match(content);
          if (m.Success)
          {
            var intensity = float.Parse(m.Groups[1].ToString());
            intensity = intensity / 3f * 4f; // 3 -> 4

            var ceil_intensity = Math.Floor(intensity); // 2.9 --> 3.0

            var i32_intensity = (int)ceil_intensity;
            if (i32_intensity > 4)
            {  // dont get bigger than 4
              i32_intensity = 4;
            }

            tracker_builder.SetDateProps(parsedDate, (int)intensity);
          }
        }
      }
    }

    var ds_root = tracker_builder.Build();

    // Draw Lotus
    ds_root.Draw();

    // Save the image to disk
    SKFileWStream fs = new SKFileWStream(output_filename);
    bmp.Encode(fs, SKEncodedImageFormat.Jpeg, quality: 90);
  }

}
