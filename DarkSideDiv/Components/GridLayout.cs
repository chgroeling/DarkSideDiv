using SkiaSharp;

namespace DarkSideDiv.Components
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

    public void SetRowFactor(int row, float factor)
    {
      row_factors[row] = factor;
      row_factors_sum = row_factors.Sum();
    }

    public void SetColFactor(int col, float factor)
    {
      col_factors[col] = factor;
      col_factors_sum = col_factors.Sum();
    }

    public IEnumerable<(int col, int row, SKRect rect)> GetRects(SKRect draw_rect)
    {
      float row_offset = 0f;
      float col_offset = 0f;

      for (int col = 0; col < Columns; col++)
      {
        row_offset = 0f;
        var col_rel = col_factors[col] / col_factors_sum;
        var width_col = draw_rect.Width * col_rel;

        for (int row = 0; row < Rows; row++)
        {
          var row_rel = row_factors[row] / row_factors_sum;


          var height_row = draw_rect.Height * row_rel;


          var left = draw_rect.Left + col_offset;
          var right = draw_rect.Left + col_offset +  width_col;

          var top = draw_rect.Top + row_offset;
          var bottom = draw_rect.Top + row_offset + height_row;

          SKRect rect = new SKRect(left, top, right, bottom);
          yield return (col, row, rect);
          row_offset += height_row;

        }
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

