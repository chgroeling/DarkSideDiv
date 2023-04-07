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

    public DsTrackerBuilder(IDeviceRepo device_repo, SKRect pic_rect)
    {
      _device_repo = device_repo;
      _pic_rect = pic_rect;
    }

    public DsDiv CreateTrackerCell()
    {
      var color = _palette_algo.GetColorByIdx(0);
      var attribs = new DsDivAttribs()
      {
        Border = 0f, // outer border
        Margin = 5f,
        // Padding = 5f,
        ContentFillColor = color,
      };

      var div = new DsDiv(_device_repo.DivDevice, attribs);
      return div;
    }

    public DsDiv CreateHeaderRowCell()
    {
      var color = _palette_algo.GetColorByIdx(2);
      var attribs = new DsDivAttribs()
      {
        Border = 0f, // outer border
        Margin = 5f,
        // Padding = 5f,
        ContentFillColor = color,
      };

      var div = new DsDiv(_device_repo.DivDevice, attribs);
      return div;
    }

     public DsDiv CreateHeaderColCell()
    {
      var color = _palette_algo.GetColorByIdx(2);
      var attribs = new DsDivAttribs()
      {
        Border = 0f, // outer border
        Padding = 5f,
        // Padding = 5f,
        //ContentFillColor = color,
      };

      var text_attribs = new DsDivComponentAlignedTextAttribs()
      {
        Text = "Mon",
        TextSize = 30,
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
    
      for (int i = 0; i < cols * rows; i++)
      {
        var col = i % ((int)cols);
        var row = i / ((int)cols);

        var cell = CreateTrackerCell();
        base_grid.Attach(col, row, cell);
      }

      var attribs = new DsDivAttribs()
      {
        // Border = 2f, 
        //BorderColor = new ColorString("#ffffff"),
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
        var title_div = CreateHeaderRowCell();
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
        var title_div = CreateHeaderColCell();
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
          (QuantityType.Percent, 100f / (cols / rows) * (1f-LEFT_SPACE_IN_PERC*0.01f))  // bottom
      ) };

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

    const float LEFT_SPACE_IN_PERC = 2.5f;
    const float TOP_SPACE_IN_PERC = 20f;
  }

}