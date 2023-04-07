namespace DarkSideDiv.Common
{
  internal class GridLayout
  {
    struct RowColAttribs
    {
      public float factor;

      public float fixed_value;

      public bool is_fixed;
    }

    public GridLayout(int cols, int rows)
    {
      Columns = cols;
      Rows = rows;

      col_attribs = new RowColAttribs[cols];
      for (int i = 0; i < cols; i++)
      {
        col_attribs[i] = new RowColAttribs()
        {
          factor = 1.0f,
          fixed_value = 0f,
          is_fixed = false
        };
      }

      col_factors_sum = col_attribs.Sum(i => i.factor);

      row_attribs = new RowColAttribs[rows];
      for (int i = 0; i < rows; i++)
      {
        row_attribs[i] = new RowColAttribs()
        {
          factor = 1.0f,
          fixed_value = 0f,
          is_fixed = false
        };
      }
      row_factors_sum = row_attribs.Sum(i => i.factor);

    }

    public void SetColFixed(int col, float value)
    {
      col_attribs[col].fixed_value = value;
      col_attribs[col].is_fixed = true;
      col_factors_sum = col_attribs.Sum(i => i.is_fixed == false ? i.factor : 0f);
    }

    public void SetRowFixed(int row, float value)
    {
      row_attribs[row].fixed_value = value;
      row_attribs[row].is_fixed = true;
      row_factors_sum = row_attribs.Sum(i => i.is_fixed == false ? i.factor : 0f);
    }

    public void SetRowPropFactor(int row, float prop_factor)
    {
      row_attribs[row].factor = prop_factor;
      row_factors_sum = row_attribs.Sum(i => i.factor);
    }

    public void SetColPropFactor(int col, float prop_factor)
    {
      col_attribs[col].factor = prop_factor;
      col_factors_sum = col_attribs.Sum(i => i.factor);
    }

    private IEnumerable<(int row, Rect rect)> GetCellsInCol(float left, float right, Rect draw_rect)
    {
      float row_offset = 0f;
      float reduced_height = draw_rect.Height;

      // Calculate reduced height due to fixed rows
      for (int row = 0; row < Rows; row++)
      {
        if (row_attribs[row].is_fixed)
        {
          reduced_height -= row_attribs[row].fixed_value;
        }
      }

      for (int row = 0; row < Rows; row++)
      {
        var height_row = 0f;

        if (row_attribs[row].is_fixed)
        {
          height_row = row_attribs[row].fixed_value;
        }
        else
        {
          var row_rel = row_attribs[row].factor / row_factors_sum;
          height_row = reduced_height * row_rel;
        }
        var top = draw_rect.Top + row_offset;
        var bottom = draw_rect.Top + row_offset + height_row;

        Rect rect = new Rect(left, top, right, bottom);
        yield return (row, rect);

        row_offset += height_row;
      }
    }

    public IEnumerable<(int col, int row, Rect rect)> GetRects(Rect draw_rect)
    {
      float col_offset = 0f;
      float reduced_width = draw_rect.Width;

      // Calculate reduced width due to fixed cols
      for (int col = 0; col < Columns; col++)
      {
        if (col_attribs[col].is_fixed)
        {
          reduced_width -= col_attribs[col].fixed_value;
        }
      }

      for (int col = 0; col < Columns; col++)
      {
        var width_col = 0f;
        if (col_attribs[col].is_fixed)
        {
          width_col = col_attribs[col].fixed_value;
        }
        else
        {
          var col_rel = col_attribs[col].factor / col_factors_sum;
          width_col = reduced_width * col_rel;
        }

        // col dimension
        var left = draw_rect.Left + col_offset;
        var right = draw_rect.Left + col_offset + width_col;

        foreach (var cell in GetCellsInCol(left, right, draw_rect))
          yield return (col, cell.row, cell.rect);

        col_offset += width_col;
      }
    }

    public int Columns
    {
      get;
    }

    public int Rows
    {
      get;
    }

    private RowColAttribs[] row_attribs;
    private float row_factors_sum;

    private RowColAttribs[] col_attribs;
    private float col_factors_sum;
  }
}

