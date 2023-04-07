namespace DarkSideDiv.Common
{
  internal class GridLayout
  {
    public GridLayout(int cols, int rows)
    {
      Columns = cols;
      Rows = rows;

      col_factors = new float[cols];
      for (int i = 0; i < cols; i++)
      {
        col_factors[i] = 1.0f;
      }
      col_factors_sum = col_factors.Sum();

      row_factors = new float[rows];
      for (int i = 0; i < rows; i++)
      {
        row_factors[i] = 1.0f;
      }
      row_factors_sum = row_factors.Sum();

    }

    public void SetRowPropFactor(int row, float prop_factor)
    {
      row_factors[row] = prop_factor;
      row_factors_sum = row_factors.Sum();
    }

    public void SetColPropFactor(int col, float prop_factor)
    {
      col_factors[col] = prop_factor;
      col_factors_sum = col_factors.Sum();
    }

    private IEnumerable<(int row, Rect rect)> GetCellsInCol(float left, float right, Rect draw_rect)
    {
      float row_offset = 0f;
      for (int row = 0; row < Rows; row++)
      {
        var row_rel = row_factors[row] / row_factors_sum;
        var height_row = draw_rect.Height * row_rel;

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

      for (int col = 0; col < Columns; col++)
      {
        var col_rel = col_factors[col] / col_factors_sum;
        var width_col = draw_rect.Width * col_rel;

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

    private float[] row_factors;
    private float row_factors_sum;

    private float[] col_factors;
    private float col_factors_sum;
  }
}

