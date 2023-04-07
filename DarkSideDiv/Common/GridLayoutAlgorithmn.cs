using DarkSideDiv.Enums;

namespace DarkSideDiv.Common
{
  public class GridLayoutAlgorithmn : IGridLayoutAlgorithmn
  {
    public GridLayoutAlgorithmn()
    {
      row_attribs = new Quantity[0];
      col_attribs = new Quantity[0];
    }

    private void Reset(int cols, int rows)
    {
      col_attribs = new Quantity[cols];
      for (int i = 0; i < cols; i++)
      {
        col_attribs[i] = new Quantity()
        {
          QType = QuantityType.Weight,
          Value = 1f
        };
      }

      col_factors_sum = col_attribs.Sum(i => i.Value);

      row_attribs = new Quantity[rows];
      for (int i = 0; i < rows; i++)
      {
        row_attribs[i] = new Quantity()
        {
          QType = QuantityType.Weight,
          Value = 1f
        };
      }
      row_factors_sum = row_attribs.Sum(i => i.Value);

    }

    private IEnumerable<(int row, Rect rect)> GetCellsInCol(
      GridLayoutOptions options,
      float left,
      float right,
      Rect draw_rect
    )
    {
      float row_offset = 0f;
      float reduced_height = draw_rect.Height;

      // Calculate reduced height due to fixed rows
      for (int row = 0; row < options.Rows; row++)
      {
        if (row_attribs[row].QType == QuantityType.FixedInPixel)
        {
          reduced_height -= row_attribs[row].Value;
        }
        else if (row_attribs[row].QType == QuantityType.Percent)
        {
          reduced_height -= row_attribs[row].Value * 0.01f * draw_rect.Height;
        }
      }

      // Remove the spacing from the reduced height
      reduced_height -= (options.Rows - 1) * options.CellSpacing;

      for (int row = 0; row < options.Rows; row++)
      {
        var height_row = 0f;

        if (row_attribs[row].QType == QuantityType.FixedInPixel)
        {
          height_row = row_attribs[row].Value;
        }
        else if (row_attribs[row].QType == QuantityType.Percent)
        {
          height_row = row_attribs[row].Value * 0.01f * draw_rect.Height;
        }
        else
        {
          var row_rel = row_attribs[row].Value / row_factors_sum;
          height_row = reduced_height * row_rel;
        }
        var top = draw_rect.Top + row_offset;
        var bottom = draw_rect.Top + row_offset + height_row;

        Rect rect = new Rect(left, top, right, bottom);
        yield return (row, rect);

        row_offset += height_row + options.CellSpacing;
      }
    }

    public IEnumerable<(int col, int row, Rect rect)> GetRectsObj(GridLayoutOptions options, Rect draw_rect)
    {
      Reset(options.Cols, options.Rows);

      if (options.ColOptions != null)
      {
        for (int col = 0; col < options.Cols; col++)
        {
          if (col < options.ColOptions.Count())
          {
            col_attribs[col] = options.ColOptions[col];
          }
        }
        col_factors_sum = col_attribs.Sum(i => i.QType == QuantityType.Weight ? i.Value : 0f);
      }

      if (options.RowOptions != null)
      {

        for (int row = 0; row < options.Rows; row++)
        {
          if (row < options.RowOptions.Count())
          {
            row_attribs[row] = options.RowOptions[row];
          }
        }
        row_factors_sum = row_attribs.Sum(i => i.QType == QuantityType.Weight ? i.Value : 0f);
      }

      float col_offset = 0f;
      float reduced_width = draw_rect.Width;


      // Calculate reduced width due to fixed cols
      for (int col = 0; col < options.Cols; col++)
      {
        if (col_attribs[col].QType == QuantityType.FixedInPixel)
        {
          reduced_width -= col_attribs[col].Value;
        }
        else if (col_attribs[col].QType == QuantityType.Percent)
        {
          reduced_width -= col_attribs[col].Value * 0.01f * draw_rect.Width;
        }
      }

      // Remove the spacing from the reduced height
      reduced_width -= (options.Cols - 1) * options.CellSpacing;

      for (int col = 0; col < options.Cols; col++)
      {
        var width_col = 0f;
        if (col_attribs[col].QType == QuantityType.FixedInPixel)
        {
          width_col = col_attribs[col].Value;
        }
        else if (col_attribs[col].QType == QuantityType.Percent)
        {
          width_col = col_attribs[col].Value * 0.01f * draw_rect.Width;
        }
        else
        {
          var col_rel = col_attribs[col].Value / col_factors_sum;
          width_col = reduced_width * col_rel;
        }

        // col dimension
        var left = draw_rect.Left + col_offset;
        var right = draw_rect.Left + col_offset + width_col;

        foreach (var cell in GetCellsInCol(options, left, right, draw_rect))
          yield return (col, cell.row, cell.rect);

        col_offset += width_col + options.CellSpacing;
      }
    }


     public IEnumerable<(int col, int row, Rect rect)> GetRects(GridLayoutOptions options, Rect draw_rect)
    {
      var gridlayout = new GridLayoutAlgorithmn();
      return gridlayout.GetRectsObj(options, draw_rect);
    }

    private Quantity[] row_attribs;
    private float row_factors_sum;

    private Quantity[] col_attribs;
    private float col_factors_sum;
  }
}

