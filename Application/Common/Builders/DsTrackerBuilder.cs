using SkiaSharp;

using DarkSideDiv.Enums;
using DarkSideDiv.Components;
using DarkSideDiv.Common;
using DarkSideDiv.Divs;
using Application.Device;
using Application.Common;

namespace Application.Builders
{
  public class DsTrackerBuilder
  {
    public delegate string GetRowTextDelegate(int row);

    GetRowTextDelegate _get_row_text;

    public delegate string GetColTextDelegate(int col);

    GetRowTextDelegate _get_col_text;

    public delegate ColorString GetCellColorDelegate(int idx);

    GetCellColorDelegate _get_cell_color;

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

    public ColorString GetCellColor(int idx)
    {
      idx = idx - GetStartIdx(); // Remove Offset

      var dt = new DateTime(YEAR, 1, 1);
      dt = dt.AddDays(idx);

      if (dt == new DateTime(YEAR, 7, 1))
      {
        return GetTrackerColor(1);
      }
      if (dt == new DateTime(YEAR, 7, 2))
      {
        return GetTrackerColor(2);
      }
      if (dt == new DateTime(YEAR, 7, 3))
      {
        return GetTrackerColor(3);
      }
      if (dt == new DateTime(YEAR, 7, 4))
      {
        return GetTrackerColor(4);
      }
      return GetTrackerColor(0);
    }
    public DsTrackerBuilder(IDeviceRepo device_repo, SKRect pic_rect)
    {
      _device_repo = device_repo;
      _pic_rect = pic_rect;
      _get_row_text = GetRowNameOfDay;
      _get_col_text = GetColNameOfMonth;
      _get_cell_color = GetCellColor;
    }

    public int GetStartIdx()
    {
      var dt = new DateTime(YEAR, 1, 1);
      var day_of_week = (int)dt.DayOfWeek;

      // Sunday is zero
      var start_idx = (day_of_week) % 7;
      return start_idx;
    }

    public int GetEndIdx()
    {
      DateTime newdate = new DateTime(YEAR + 1, 1, 1);
      //Substract one year
      var last_day = newdate.AddDays(-1);
      return last_day.DayOfYear + GetStartIdx();
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
      var col_startidx_this_week = col * DAYS - GetStartIdx();
      var col_startidx_last_week = (col - 1) * DAYS - GetStartIdx();

      var start_date_this_week = new DateTime(YEAR, 1, 1).AddDays(col_startidx_this_week);
      var start_date_last_week = new DateTime(YEAR, 1, 1).AddDays(col_startidx_last_week);

      // check if this week is the first "full" week in the month
      if (start_date_this_week.Month != start_date_last_week.Month) // first week which is completly in month
      {
        return GetMonth(start_date_this_week.Month);
      }
      return "";
    }

    public DsDiv CreateCell(int grid_idx)
    {
      var attribs = new DsDivAttribs()
      {
        Border = 0f, // outer border
        Margin = 5f,
        // Padding = 5f,
        ContentFillColor = _get_cell_color(grid_idx),
      };

      var div = new DsDiv(_device_repo.DivDevice, attribs);
      return div;
    }

    public DsDiv CreateHeaderRowCell(int col)
    {
      var color = _palette_algo.GetColorByIdx(2);
      var attribs = new DsDivAttribs()
      {
        Border = 0f, // outer border
        Margin = (0f, 0f, 0f, 20f)
        //Padding = 5f,
        //ContentFillColor = color,
      };

      var text_attribs = new DsDivComponentAlignedTextAttribs()
      {
        Text = _get_col_text(col),
        TextSize = 40,
        Alignment = DsAlignment.BottomLeft
      };

      var text_comp = new DsDivComponentAlignedText(_device_repo.DivTextDevice, text_attribs);


      var div = new DsDiv(_device_repo.DivDevice, attribs);
      div.Append(text_comp);
      return div;
    }

    public DsDiv CreateHeaderColCell(int row)
    {
      var color = _palette_algo.GetColorByIdx(2);
      var attribs = new DsDivAttribs()
      {
        Border = 0f, // outer border
        Padding = 5f,
        // Padding = 5f,
        // ContentFillColor = color,
      };

      var text_attribs = new DsDivComponentAlignedTextAttribs()
      {
        Text = _get_row_text(row),
        TextSize = 40,
        Alignment = DsAlignment.Left
      };

      var text_comp = new DsDivComponentAlignedText(_device_repo.DivTextDevice, text_attribs);

      var div = new DsDiv(_device_repo.DivDevice, attribs);
      div.Append(text_comp);
      return div;
    }


    public DsDiv CreateGrid()
    {
      var rows = DAYS;
      var cols = WEEKS;
      var base_grid = new DsDivComponentGrid(
        (int)cols,
        (int)rows
      );
      base_grid.SetDivSpacing(2f);

      var idx_offst = GetStartIdx();
      var idx_end = GetEndIdx();

      for (int i = 0; i < cols * rows; i++)
      {
        var col = i / ((int)rows);
        var row = i % ((int)rows);

        var cell = CreateCell(i);

        if ((i >= idx_offst) && (i < idx_end))
        {
          base_grid.Attach(col, row, cell);
        }
      }

      var attribs = new DsDivAttribs()
      {
        ContentFillColor = new ColorString("#ffffff"),
      };

      var div = new DsDiv(_device_repo.DivDevice, attribs);
      div.Append(base_grid);
      return div;
    }


    public DsDiv CreateHeaderRow()
    {
      var cols = WEEKS;
      var base_grid = new DsDivComponentGrid((int)cols, 1);
      base_grid.SetDivSpacing(2f);

      var attribs = new DsDivAttribs()
      {
      };

      var div = new DsDiv(_device_repo.DivDevice, attribs);
      //var content_div = CreateContentDiv();
      for (int i = 0; i < cols; i++)
      {
        var title_div = CreateHeaderRowCell(i);
        base_grid.Attach(i, 0, title_div);
      }

      div.Append(base_grid);

      return div;
    }


    public DsDiv CreateSpacerAndHeaderRow()
    {
      var base_grid = new DsDivComponentGrid(2, 1);
      base_grid.SetColPercFactor(0, LEFT_SPACE_IN_PERC);

      var attribs = new DsDivAttribs()
      {
      };

      var div = new DsDiv(_device_repo.DivDevice, attribs);
      base_grid.Attach(1, 0, CreateHeaderRow());
      div.Append(base_grid);

      return div;
    }

    public DsDiv CreateHeaderCol()
    {
      var rows = DAYS;
      var base_grid = new DsDivComponentGrid(1, (int)rows);
      base_grid.SetDivSpacing(2f);

      var attribs = new DsDivAttribs()
      {
      };

      var div = new DsDiv(_device_repo.DivDevice, attribs);

      for (int i = 0; i < rows; i++)
      {
        var title_div = CreateHeaderColCell(i);
        base_grid.Attach(0, i, title_div);
      }

      div.Append(base_grid);
      return div;
    }

    public DsDiv CreateHeaderColAndGrid()
    {
      var base_grid = new DsDivComponentGrid(2, 1);
      base_grid.SetColPercFactor(0, LEFT_SPACE_IN_PERC);

      var rows = DAYS;
      var cols = WEEKS;
      var attribs = new DsDivAttribs()
      {
        Border = 1f,
        Position = PositionType.Absolute, // place it absolute to new rect
        //BorderColor = new ColorString("#000000"),
        Height = HeightType.Zero,
        Padding = (
          (QuantityType.FixedInPixel, 0.0f),  // left
          (QuantityType.FixedInPixel, 0.0f),  // top
          (QuantityType.FixedInPixel, 0.0f),  // right
          (QuantityType.Percent, 100f / (cols / rows) * (1f - LEFT_SPACE_IN_PERC * 0.01f))  // bottom
      )
      };

      var div = new DsDiv(_device_repo.DivDevice, attribs);

      var header_col = CreateHeaderCol();
      base_grid.Attach(0, 0, header_col);

      var grid = CreateGrid();
      base_grid.Attach(1, 0, grid);

      div.Append(base_grid);

      return div;
    }

    public DsRoot Build()
    {
      var header_row_and_content = new DsDivComponentGrid(1, 2);
      //base_grid.SetDivSpacing(2f);
      header_row_and_content.SetRowPercFactor(0, TOP_SPACE_IN_PERC);
      //      base_grid.SetColFixedInPixel(0, 200f);
      var attribs = new DsDivAttribs()
      {
        Border = 2f, // outer border
        Padding = 20f,
        Position = PositionType.Relative,
        BorderColor = new ColorString("#00ff00"),
        ContentFillColor = new ColorString("#ffffff"),
      };

      var div = new DsDiv(_device_repo.DivDevice, attribs);
      var spacer_and_header_row = CreateSpacerAndHeaderRow();
      header_row_and_content.Attach(0, 0, spacer_and_header_row);

      var header_col_and_content = CreateHeaderColAndGrid();
      header_row_and_content.Attach(0, 1, header_col_and_content);
      div.Append(header_row_and_content);

      var root_div = new DsRoot(ConversionFactories.ToRect(_pic_rect));
      root_div.Attach(div);
      return root_div;
    }


    DsPalette _palette_algo = new DsPalette();

    IDeviceRepo _device_repo;

    private SKRect _pic_rect;

    const float WEEKS = 53f;
    const float DAYS = 7f;

    const float LEFT_SPACE_IN_PERC = 3.4f;
    const float TOP_SPACE_IN_PERC = 10f;

    const int YEAR = 2020;
  }

}