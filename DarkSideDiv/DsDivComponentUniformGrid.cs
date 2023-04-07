using SkiaSharp;

namespace DarkSideDiv
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
      var width_col = draw_rect.Width / (float)_cols;
      var height_row = draw_rect.Height / (float)_rows;

      for (int col = 0; col < _cols; col++)
      {
        for (int row = 0; row < _rows; row++)
        {
          if (_grid[col, row] is null)
          {
            continue;
          }
          var left = draw_rect.Left + ((float)col * width_col);
          var right = draw_rect.Left + ((float)(col + 1) * width_col);

          var top = draw_rect.Top + ((float)row * height_row);
          var bottom = draw_rect.Top + ((float)(row + 1) * height_row);

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

