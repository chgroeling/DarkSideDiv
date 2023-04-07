using SkiaSharp; // TODO: Move to Infrastructure
using Application.Common.Interfaces;
using Application.Device;
using DarkSideDiv.Common;

namespace Application;

public class UseCaseCreateTrackerDiagram
{
  class Tracker
  {
    public Tracker(int days, int weeks, int year)
    {
      _days = days;
      _weeks = weeks;
      _year = year;
    }
    
    public ColorString GetTrackerColor(int idx)
    {
      switch (idx)
      {
        case 0:
          return new ColorString("#eef0f3"); // grey
        case 1:
          return new ColorString("#bef29b"); // light green
        case 2:
          return new ColorString("#4edc84"); // green
        case 3:
          return new ColorString("#00b454"); // dark green
        case 4:
          return new ColorString("#007c3c"); // darkest green
      }

      return new ColorString("#eef0f3");
    }

    public int GetStartIdx()
    {
      var dt = new DateTime(_year, 1, 1);
      var day_of_week = (int)dt.DayOfWeek;

      // Sunday is zero
      var start_idx = (day_of_week) % 7;
      return start_idx;
    }

    public int GetEndIdx()
    {
      DateTime newdate = new DateTime(_year + 1, 1, 1);
      //Substract one year
      var last_day = newdate.AddDays(-1);
      return last_day.DayOfYear + GetStartIdx();
    }


    public ColorString GetCellColor(int idx)
    {
      if ((idx < GetStartIdx()) || (idx >= GetEndIdx()))
      {
        return new ColorString("#ffffff");
      }

      idx = idx - GetStartIdx(); // Remove Offset



      var dt = new DateTime(_year, 1, 1);
      dt = dt.AddDays(idx);

      if (dt == new DateTime(_year, 7, 1))
      {
        return GetTrackerColor(1);
      }
      if (dt == new DateTime(_year, 7, 2))
      {
        return GetTrackerColor(2);
      }
      if (dt == new DateTime(_year, 7, 3))
      {
        return GetTrackerColor(3);
      }
      if (dt == new DateTime(_year, 7, 4))
      {
        return GetTrackerColor(4);
      }

      return GetTrackerColor(0);
    }

    public string GetRowNameOfDay(int row)
    {
      switch (row)
      {
        case 1:
          return "Mon";
        case 3:
          return "Wed";
        case 5:
          return "Fri";
      }
      return "";
    }

    public string GetMonth(int month)
    {
      switch (month)
      {
        case 1:
          return "Jan";
        case 2:
          return "Feb";
        case 3:
          return "Mar";
        case 4:
          return "Apr";
        case 5:
          return "May";
        case 6:
          return "Jun";
        case 7:
          return "Jul";
        case 8:
          return "Aug";
        case 9:
          return "Sep";
        case 10:
          return "Oct";
        case 11:
          return "Nov";
        case 12:
          return "Dec";

      }
      return "";
    }
    public string GetColNameOfMonth(int col)
    {
      if (col <= 1)
      {
        if (col == 0)
        {
          return GetMonth(col + 1);
        }
        return "";
      }
      var col_startidx_this_week = col * _days - GetStartIdx();
      var col_startidx_last_week = (col - 1) * _days - GetStartIdx();

      var start_date_this_week = new DateTime(_year, 1, 1).AddDays(col_startidx_this_week);
      var start_date_last_week = new DateTime(_year, 1, 1).AddDays(col_startidx_last_week);

      // check if this week is the first "full" week in the month
      if (start_date_this_week.Month != start_date_last_week.Month) // first week which is completly in month
      {
        return GetMonth(start_date_this_week.Month);
      }
      return "";
    }


   int _year = 2020;

    float _weeks;
    float _days;

  }

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
    SKBitmap bmp = new SKBitmap(3200, 1000); // 5::3
    using SKCanvas canvas = new SKCanvas(bmp);
    canvas.Clear(SKColor.Parse("#003366"));

    var device_repo = new DeviceRepoSkia();
    device_repo.TextDeviceSkia = new DsDivComponentAlignedTextSkia();
    device_repo.DivDeviceSkia = new DsDivSkia();
    device_repo.SetCanvas(canvas);


    var graph_rect = new SKRect(0.0f, 0.0f, bmp.Width, bmp.Height);
    var tracker = new Tracker(DAYS, WEEKS, YEAR);

    var grid_builder = new Builders.DsGridBuilder(
      device_repo,
      graph_rect,
      (int)WEEKS,
      (int)DAYS
    );

    // Read Markdown File
    var text = _read_text_file.Read(input_filename);
    var topic_list = _document_converter.GetTableOfContents(text);

    for (int i = 0; i < (int)DAYS; i++)
    {
      grid_builder.SetRowLabel(
        i,
        tracker.GetRowNameOfDay(i)
      );
    }

    for (int i = 0; i < (int)WEEKS; i++)
    {
      grid_builder.SetColLabel(
        i,
        tracker.GetColNameOfMonth(i)
      );
    }

    for (int i = 0; i < (int)WEEKS * (int)DAYS; i++)
    {
      grid_builder.SetCellColor(
        i / (int)DAYS,
        i % (int)(DAYS),
        tracker.GetCellColor(i)
      );
    }

    var ds_root = grid_builder.Build();

    // Draw Lotus
    ds_root.Draw();

    // Save the image to disk
    SKFileWStream fs = new SKFileWStream(output_filename);
    bmp.Encode(fs, SKEncodedImageFormat.Jpeg, quality: 90);
  }

}
