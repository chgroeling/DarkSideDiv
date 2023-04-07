using DarkSideDiv.Divs;
using DarkSideDiv.Common;
using DarkSideDiv.Enums;

namespace DarkSideDiv.Components
{
  public class DsDivComponentGrid : IDsDivComponent
  {

    public IGridLayout GridLayout { get; set; }

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

    public DsDivComponentGrid(int cols, int rows)
    {
      GridLayout = new GridLayout();
      _grid = new IDsDiv[cols, rows];
      _cols = cols;
      _rows = rows;
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
      GridLayoutSettings settings = new GridLayoutSettings
      {
        Cols = _cols,
        Rows = _rows,
        ColOptions = _col_options,
        RowOptions = _row_options
      };

      foreach (var tuple in GridLayout.GetRects(settings, draw_rect))
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

  }
}

