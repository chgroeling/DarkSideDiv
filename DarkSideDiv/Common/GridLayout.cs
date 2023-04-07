namespace DarkSideDiv.Common
{
  internal class GridLayout : IGridLayout
  {
    enum QuantityType
    {
      FixedInPixel,
      Weight,
    };

    struct Quantity
    {
      public QuantityType QType {get; set;}
      public float Value {get; set;}
    }

    public GridLayout(int cols, int rows)
    {
      Columns = cols;
      Rows = rows;

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

    public void SetColFixed(int col, float value)
    {
      col_attribs[col].Value = value;
      col_attribs[col].QType = QuantityType.FixedInPixel;
      col_factors_sum = col_attribs.Sum(i => i.QType == QuantityType.Weight ? i.Value : 0f);
    }

    public void SetRowFixed(int row, float value)
    {
      row_attribs[row].Value = value;
      row_attribs[row].QType = QuantityType.FixedInPixel;
      row_factors_sum = row_attribs.Sum(i => i.QType == QuantityType.Weight ? i.Value : 0f);
    }

    public void SetRowPropFactor(int row, float prop_factor)
    {
      row_attribs[row].Value = prop_factor;
      row_factors_sum = row_attribs.Sum(i => i.Value);
    }

    public void SetColPropFactor(int col, float prop_factor)
    {
      col_attribs[col].Value = prop_factor;
      col_factors_sum = col_attribs.Sum(i => i.Value);
    }

    private IEnumerable<(int row, Rect rect)> GetCellsInCol(float left, float right, Rect draw_rect)
    {
      float row_offset = 0f;
      float reduced_height = draw_rect.Height;

      // Calculate reduced height due to fixed rows
      for (int row = 0; row < Rows; row++)
      {
        if (row_attribs[row].QType == QuantityType.FixedInPixel)
        {
          reduced_height -= row_attribs[row].Value;
        }
      }

      for (int row = 0; row < Rows; row++)
      {
        var height_row = 0f;

        if (row_attribs[row].QType == QuantityType.FixedInPixel)
        {
          height_row = row_attribs[row].Value;
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
        if (col_attribs[col].QType == QuantityType.FixedInPixel)
        {
          reduced_width -= col_attribs[col].Value;
        }
      }

      for (int col = 0; col < Columns; col++)
      {
        var width_col = 0f;
        if (col_attribs[col].QType == QuantityType.FixedInPixel)
        {
          width_col = col_attribs[col].Value;
        }
        else
        {
          var col_rel = col_attribs[col].Value / col_factors_sum;
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

    private Quantity[] row_attribs;
    private float row_factors_sum;

    private Quantity[] col_attribs;
    private float col_factors_sum;
  }
}
