using DarkSideDiv.Divs;
using DarkSideDiv.Common;

namespace DarkSideDiv.Components
{
  public class DsDivComponentGrid : IDsDivComponent
  {

    GridLayout _grid_layout;

    public DsDivComponentGrid() : this(1, 1)
    {
    }

    public void SetRowPropFactor(int row, float factor)
    {
      _grid_layout.SetRowPropFactor(row, factor);
    }

    public void SetColPropFactor(int col, float factor)
    {
      _grid_layout.SetColPropFactor(col, factor);
    }

    public DsDivComponentGrid(int cols, int rows)
    {
      _grid = new IDsDiv[cols, rows];
      _cols = cols;
      _rows = rows;
      _grid_layout = new GridLayout(cols, rows);
    }

    public void Attach(int col, int row, IDsDiv div)
    {
      _grid[col, row] = div;
    }

    public void Draw(Rect draw_rect)
    {
      foreach (var tuple in _grid_layout.GetRects(draw_rect))
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

  }
}

