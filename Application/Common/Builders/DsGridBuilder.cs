using SkiaSharp;

using DarkSideDiv.Enums;
using DarkSideDiv.Common;
using DarkSideDiv.Divs;
using Application.Device;
using Application.Common;

namespace Application.Builders
{
  public class DsGridBuilder
  {
    struct CellSettings
    {
      public ColorString Color { get; set; }
    }

    Dictionary<(int col, int row), CellSettings> _cell_settings;

    struct RowSettings
    {
      public string Label { get; set; }
    }

    Dictionary<int, RowSettings> _row_settings;

    struct ColSettings
    {
      public string Label { get; set; }
    }

    Dictionary<int, ColSettings> _col_settings;


    public DsGridBuilder(IDeviceRepo device_repo, IGridLayoutAlgorithmn grid_layout_algorithmn)
    {
      _grid_layout_algorithmn = grid_layout_algorithmn;
      _device_repo = device_repo; 
      _pic_rect = new SKRect(0f,0f,100f,100f);
      _cols = 1;
      _rows = 1;
      _cell_settings = new Dictionary<(int col, int row), CellSettings>();
      _row_settings = new Dictionary<int, RowSettings>();
      _col_settings = new Dictionary<int, ColSettings>();
    }

    // The reset method clears the object being built.
    public void Reset(SKRect pic_rect, int cols, int rows)
    {
      _cols = cols;
      _rows = rows;
      _cell_settings = new Dictionary<(int col, int row), CellSettings>();
      _row_settings = new Dictionary<int, RowSettings>();
      _col_settings = new Dictionary<int, ColSettings>();
      _pic_rect = pic_rect;
    }

    public void SetCellColor(int col, int row, ColorString color)
    {
      var cell = _cell_settings.GetValueOrDefault((col, row));
      cell.Color = color;
      _cell_settings.Add((col, row), cell);
    }

    public void SetRowLabel(int row, string label)
    {
      var row_set = _row_settings.GetValueOrDefault(row);
      row_set.Label = label;
      _row_settings.Add(row, row_set);
    }


    public void SetColLabel(int row, string label)
    {
      var col_set = _col_settings.GetValueOrDefault(row);
      col_set.Label = label;
      _col_settings.Add(row, col_set);
    }


    DsDiv CreateCell(int grid_idx)
    {
      var col = grid_idx / ((int)_rows);
      var row = grid_idx % ((int)_rows);

      var color = new ColorString("#000000");
      if (_cell_settings.ContainsKey((col, row)))
      {
        color = _cell_settings.GetValueOrDefault((col, row)).Color;
      }

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

   DsDiv CreateHeaderRowCell(int col)
    {
      var attribs = new DsDivAttribs()
      {
        Border = 0f, // outer border
        Margin = (0f, 0f, 0f, 20f)
      };

      ColSettings col_set;
      string col_label = "";
      if (_col_settings.TryGetValue(col, out col_set))
      {
        col_label = col_set.Label;
      }

      var text_attribs = new DsDivAlignedTextAttribs()
      {
        Text = col_label,
        TextSize = 40,
        Alignment = DsAlignment.BottomLeft
      };

      var text_comp = new DsDivAlignedText(_device_repo.DivTextDevice, _grid_layout_algorithmn, text_attribs);
      var div = new DsDiv(_device_repo.DivDevice, attribs);
      div.Append(text_comp);
      return div;
    }

    DsDiv CreateHeaderColCell(int row)
    {
      var attribs = new DsDivAttribs()
      {
        Border = 0f, // outer border
        Padding = 5f,
      };

      RowSettings row_set;
      string row_label = "";
      if (_row_settings.TryGetValue(row, out row_set))
      {
        row_label = row_set.Label;
      }
      var text_attribs = new DsDivAlignedTextAttribs()
      {
        Text = row_label,
        TextSize = 40,
        Alignment = DsAlignment.Left
      };

      var text_comp = new DsDivAlignedText(_device_repo.DivTextDevice, _grid_layout_algorithmn, text_attribs);

      var div = new DsDiv(_device_repo.DivDevice, attribs);
      div.Append(text_comp);
      return div;
    }


    DsDiv CreateGrid()
    {
      var base_grid = new DsDivGrid(
        _grid_layout_algorithmn,
        (int)_cols,
        (int)_rows
      );
      base_grid.SetDivSpacing(2f);

      for (int i = 0; i < _cols * _rows; i++)
      {
        var col = i / ((int)_rows);
        var row = i % ((int)_rows);

        var cell = CreateCell(i);
        base_grid.Attach(col, row, cell);
      }

      var attribs = new DsDivAttribs()
      {
        ContentFillColor = new ColorString("#ffffff"),
      };

      var div = new DsDiv(_device_repo.DivDevice, attribs);
      div.Append(base_grid);
      return div;
    }


    DsDiv CreateHeaderRow()
    {
      var base_grid = new DsDivGrid(
        _grid_layout_algorithmn,
        (int)_cols,
        1
      );
      base_grid.SetDivSpacing(2f);

      var attribs = new DsDivAttribs()
      {
      };

      var div = new DsDiv(_device_repo.DivDevice, attribs);
      for (int i = 0; i < _cols; i++)
      {
        var title_div = CreateHeaderRowCell(i);
        base_grid.Attach(i, 0, title_div);
      }

      div.Append(base_grid);

      return div;
    }


    DsDiv CreateSpacerAndHeaderRow()
    {
      var base_grid = new DsDivGrid(
        _grid_layout_algorithmn,
        2,
        1
      );
      base_grid.SetColPercFactor(0, LEFT_SPACE_IN_PERC);

      var attribs = new DsDivAttribs()
      {
      };

      var div = new DsDiv(_device_repo.DivDevice, attribs);
      base_grid.Attach(1, 0, CreateHeaderRow());
      div.Append(base_grid);

      return div;
    }

    DsDiv CreateHeaderCol()
    {
      var base_grid = new DsDivGrid(
        _grid_layout_algorithmn,
        1,
        (int)_rows
      );
      base_grid.SetDivSpacing(2f);

      var attribs = new DsDivAttribs()
      {
      };

      var div = new DsDiv(_device_repo.DivDevice, attribs);

      for (int i = 0; i < _rows; i++)
      {
        var title_div = CreateHeaderColCell(i);
        base_grid.Attach(0, i, title_div);
      }

      div.Append(base_grid);
      return div;
    }

    DsDiv CreateHeaderColAndGrid()
    {
      var base_grid = new DsDivGrid(
        _grid_layout_algorithmn,
        2,
        1
      );
      base_grid.SetColPercFactor(0, LEFT_SPACE_IN_PERC);

      var attribs = new DsDivAttribs()
      {
        Border = 1f,
        Position = PositionType.Absolute, // place it absolute to relative rect
        Height = HeightType.Zero,
        Padding = (
          (QuantityType.FixedInPixel, 0.0f),  // left
          (QuantityType.FixedInPixel, 0.0f),  // top
          (QuantityType.FixedInPixel, 0.0f),  // right
          (QuantityType.Percent, 100f / (_cols / _rows) * (1f - LEFT_SPACE_IN_PERC * 0.01f))  // bottom
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
      var header_row_and_content = new DsDivGrid(
        _grid_layout_algorithmn,
        1,
        2
      );

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

    IDeviceRepo _device_repo;

    private SKRect _pic_rect;

    const float LEFT_SPACE_IN_PERC = 3.4f;
    const float TOP_SPACE_IN_PERC = 14f;

    int _rows;

    int _cols;

    IGridLayoutAlgorithmn _grid_layout_algorithmn;
  }

}