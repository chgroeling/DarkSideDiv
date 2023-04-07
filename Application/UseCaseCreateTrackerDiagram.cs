using SkiaSharp; // TODO: Move to Infrastructure
using Application.Common.Interfaces;
using Application.Device;
using DarkSideDiv.Common;

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
    var topic_list = _document_converter.GetTableOfContents(text);

    tracker_builder.SetDateProps(new DateTime(YEAR, 2, 1), 1);
    tracker_builder.SetDateProps(new DateTime(YEAR, 2, 2), 2);
    tracker_builder.SetDateProps(new DateTime(YEAR, 2, 3), 3);
    tracker_builder.SetDateProps(new DateTime(YEAR, 2, 4), 4);

     tracker_builder.SetDateProps(new DateTime(YEAR, 6, 1), 1);
    tracker_builder.SetDateProps(new DateTime(YEAR, 6, 2), 2);
    tracker_builder.SetDateProps(new DateTime(YEAR, 6, 3), 3);
    tracker_builder.SetDateProps(new DateTime(YEAR, 6, 4), 4);

    var ds_root = tracker_builder.Build();

    // Draw Lotus
    ds_root.Draw();

    // Save the image to disk
    SKFileWStream fs = new SKFileWStream(output_filename);
    bmp.Encode(fs, SKEncodedImageFormat.Jpeg, quality: 90);
  }

}
