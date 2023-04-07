using SkiaSharp;

using DarkSideDiv.Enums;
using DarkSideDiv.Common;
using DarkSideDiv.Divs;
using Application.Device;
using Application.Common;

namespace Application.Builders
{
  public class DsTrackerBuilder
  {
    struct DateCellProperties
    {
      public int value;
    };

    Dictionary<DateTime, DateCellProperties> _cell_properties;

    public DsTrackerBuilder(DsGridBuilder grid_builder)
    {
      _grid_builder = grid_builder;
      _days = 7;
      _weeks = 4;
      _year = 2023;
      _cell_properties = new Dictionary<DateTime, DateCellProperties>();
      _pic_rect = new SKRect(0f,0f,100f,100f);
    }

    public DsRoot Build()
    {
      _grid_builder.Reset(_pic_rect, (int)_weeks, (int)_days);

      for (int i = 0; i < (int)_days; i++)
      {
        _grid_builder.SetRowLabel(
          i,
          GetRowNameOfDay(i)
        );
      }

      for (int i = 0; i < (int)_weeks; i++)
      {
        _grid_builder.SetColLabel(
          i,
          GetColNameOfMonth(i)
        );
      }
      for (int i = 0; i < (int)_weeks * (int)_days; i++)
      {
        _grid_builder.SetCellColor(
          i / (int)_days,
          i % (int)(_days),
          GetCellColor(i)
        );
      }
      return _grid_builder.Build();
    }

    public void SetDateProps(DateTime dt, int value)
    {
      var cell = _cell_properties.GetValueOrDefault(dt);
      cell.value = value;
      _cell_properties.Add(dt, cell);
    }

    // The reset method clears the object being built.
    public void Reset(SKRect pic_rect, int days, int weeks, int year)
    {
      _days = days;
      _weeks = weeks;
      _year = year;
      _cell_properties = new Dictionary<DateTime, DateCellProperties>();
      _pic_rect = pic_rect;
    }

    ColorString GetTrackerColor(int idx)
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

    int GetStartIdx()
    {
      var dt = new DateTime(_year, 1, 1);
      var day_of_week = (int)dt.DayOfWeek;

      // Sunday is zero
      var start_idx = (day_of_week) % 7;
      return start_idx;
    }

    int GetEndIdx()
    {
      DateTime newdate = new DateTime(_year + 1, 1, 1);
      //Substract one year
      var last_day = newdate.AddDays(-1);
      return last_day.DayOfYear + GetStartIdx();
    }


    ColorString GetCellColor(int idx)
    {
      if ((idx < GetStartIdx()) || (idx >= GetEndIdx()))
      {
        return new ColorString("#ffffff");
      }

      idx = idx - GetStartIdx(); // Remove Offset

      var dt = new DateTime(_year, 1, 1);
      dt = dt.AddDays(idx);

      if (_cell_properties.ContainsKey(dt))
      {
        var cell_props = _cell_properties.GetValueOrDefault(dt);
        return GetTrackerColor(cell_props.value);
      }
      return GetTrackerColor(0);
    }

    string GetRowNameOfDay(int row)
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

    string GetMonth(int month)
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
    string GetColNameOfMonth(int col)
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


    DsGridBuilder _grid_builder;

      private SKRect _pic_rect;
  }

}