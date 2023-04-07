using DarkSideDiv.Divs;
using SkiaSharp;

namespace DarkSideDiv.Components
{
  public class DsDivComponentUniformGrid : IDsDivComponent
  {

    GridLayout _grid_layout;

    public DsDivComponentUniformGrid() : this(1, 1)
    {
    }

    public void SetRowFactor(int row, float factor) {
      _grid_layout.SetRowFactor(row, factor);
    }

    public void SetColFactor(int col, float factor) {
      _grid_layout.SetColFactor(col, factor);
    }

    public DsDivComponentUniformGrid(int cols, int rows)
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

    public void Draw(SKCanvas canvas, SKRect draw_rect)
    {
      foreach (var tuple in _grid_layout.GetRects(draw_rect))
      {
        (int col, int row, SKRect rect) = tuple;
        if (_grid[col, row] is null)
        {
          continue;
        }

        _grid[col, row]?.Draw(canvas, rect);
      }
    }

    IDsDiv[,] _grid;

    int _rows;

    int _cols;

  }
}

