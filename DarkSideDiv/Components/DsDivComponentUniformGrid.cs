using DarkSideDiv.Divs;
using SkiaSharp;

namespace DarkSideDiv.Components
{

  public class DsDivComponentUniformGrid : IDsDivComponent
  {

    public DsDivComponentUniformGrid() : this(1, 1)
    {
    }

    public DsDivComponentUniformGrid(int cols, int rows)
    {
      _grid = new IDsDiv[cols, rows];
      _cols = cols;
      _rows = rows;
    }

    public void Attach(int col, int row, IDsDiv div)
    {
      _grid[col, row] = div;
    }

    public void Draw(SKCanvas canvas, SKRect draw_rect)
    {
      var width_col = draw_rect.Width / _cols;
      var height_row = draw_rect.Height / _rows;

      for (int col = 0; col < _cols; col++)
      {
        for (int row = 0; row < _rows; row++)
        {
          if (_grid[col, row] is null)
          {
            continue;
          }
          var left = draw_rect.Left + col * width_col;
          var right = draw_rect.Left + (col + 1) * width_col;

          var top = draw_rect.Top + row * height_row;
          var bottom = draw_rect.Top + (row + 1) * height_row;

          SKRect rect = new SKRect(left, top, right, bottom);
          _grid[col, row]?.Draw(canvas, rect);
        }
      }
    }

    IDsDiv[,] _grid;

    int _rows;

    int _cols;

  }
}

