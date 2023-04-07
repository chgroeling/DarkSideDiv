using SkiaSharp;

namespace DarkSideDiv {

public struct DsUniformDivAttribs
{
  public static DsUniformDivAttribs Default()
  {
    DsUniformDivAttribs attribs = new DsUniformDivAttribs()
    {
      border = 10,
      border_color = SKColor.Parse("000000"),
      content_fill_color = SKColor.Parse("DAE8FC"),
      rows = 10,
      cols = 10
    };
    return attribs;
  }
  
  public float margin;
  public float border;

  public float padding;

  public SKColor border_color;

  public SKColor content_fill_color;

  public int rows;

  public int cols;
}


public class DsUniformGrid : IDsDiv
{

  public DsUniformGrid() : this(DsUniformDivAttribs.Default())
  {
  }

  public DsUniformGrid(DsUniformDivAttribs attribs)
  {
    _attribs = attribs;
    _base_div = new DsDiv (DeriveDsAttribs(attribs));
    _grid = new IDsDiv?[_attribs.cols, _attribs.rows];
  }

  private static DsDivAttribs DeriveDsAttribs(DsUniformDivAttribs attribs)
  {
    DsDivAttribs div = new DsDivAttribs()
    {
      border = attribs.border,
      margin = attribs.margin,
      padding = attribs.padding,
      content_fill_color = attribs.content_fill_color,
      border_color = attribs.border_color,
    };
    return div;
  }

  public void Attach(int col, int row, IDsDiv div)
  {
    _grid[col, row] = div;
  }

  public void Draw(SKCanvas canvas, SKRect draw_rect)
  {
    _base_div.Draw(canvas, draw_rect);

    // CONTENT
    var content_rec = dim_algo.CalculateContentRect(
      draw_rect,
      _attribs.margin,
      _attribs.border,
      _attribs.padding
    );

    var width_col = content_rec.Width / (float)_attribs.cols;
    var height_row = content_rec.Height / (float)_attribs.rows;

    for (int col = 0; col < _attribs.cols; col++)
    {
      for (int row = 0; row < _attribs.rows; row++)
      {
        if (_grid[col, row] is null)
        {
          continue;
        }
        var left = content_rec.Left + ((float)col * width_col);
        var right = content_rec.Left + ((float)(col + 1) * width_col);

        var top = content_rec.Top + ((float)row * height_row);
        var bottom = content_rec.Top + ((float)(row + 1) * height_row);

        SKRect rect = new SKRect(left, top, right, bottom);
        _grid[col, row]?.Draw(canvas, rect);
      }
    }
  }

  DsDiv _base_div;

  IDsDiv?[,] _grid;

  DsUniformDivAttribs _attribs;

  private DsRectDimensions dim_algo = new DsRectDimensions();


}
}