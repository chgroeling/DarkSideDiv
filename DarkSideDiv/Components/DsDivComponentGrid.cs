using DarkSideDiv.Divs;
using DarkSideDiv.Common;
using DarkSideDiv.Enums;

namespace DarkSideDiv.Components
{
  public class DsDivComponentGrid : IDsDivComponent
  {

    public DsDivComponentGrid() : this(1, 1)
    {
    }

    public void SetRowPropFactor(int row, float factor)
    {
      _row_options[row] = (QuantityType.Weight, factor);
    }

    public void SetColPropFactor(int col, float factor)
    {
      _col_options[col] = (QuantityType.Weight, factor);
    }

    public void SetDivSpacing(float spacing) {
      _div_spacing = spacing;
    }

    public DsDivComponentGrid(int cols, int rows)
    {
      _grid = new IDsDiv[cols, rows];
      _cols = cols;
      _rows = rows;
      _div_spacing = 0f;
      _row_options = new List<Quantity>();
      for (int i = 0; i < _rows; i++)
      {
        _row_options.Add((QuantityType.Weight, 1.0f));
      }
      _col_options = new List<Quantity>();
      for (int i = 0; i < _cols; i++)
      {
        _col_options.Add((QuantityType.Weight, 1.0f));
      }

    }

    public void Attach(int col, int row, IDsDiv div)
    {
      _grid[col, row] = div;
    }
    public void Draw(Rect draw_rect)
    {
      var options = new GridLayoutOptions
      {
        Cols = _cols,
        Rows = _rows,
        ColOptions = _col_options,
        RowOptions = _row_options,
        DivSpacing = _div_spacing,
      };

      foreach (var tuple in GridLayoutAlgorithmn.GetRects(options, draw_rect))
      {
        (int col, int row, Rect rect) = tuple;
        if (_grid[col, row] is null)
        {
          continue;
        }

        _grid[col, row]?.Draw(rect);
      }
    }

    IDsDiv[,] _grid;

    int _rows;

    int _cols;

    List<Quantity> _row_options;
    List<Quantity> _col_options;

    float _div_spacing;

  }
}

