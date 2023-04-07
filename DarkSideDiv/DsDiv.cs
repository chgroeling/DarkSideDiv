using SkiaSharp;

namespace DarkSideDiv;

public class DsRectDimensions
{

  public SKRect CalculateBorderRect(SKRect outer_rect, float margin)
  {
    outer_rect.Inflate(-margin, -margin);
    return outer_rect;
  }
  public SKRect CalculatePaddingRect(SKRect outer_rect, float margin, float border)
  {
    return new SKRect();
  }
  public SKRect CalculateContentRect(SKRect outer_rect, float margin, float border, float padding)
  {
    var deflate_len = margin + border + padding;
    outer_rect.Inflate(-deflate_len, -deflate_len);
    return outer_rect;
  }

}

public struct DsDivAttribs
{
  public DsDivAttribs()
  {

  }

  public float margin;
  public float border;

  public float padding;

  public SKColor border_color;

  public SKColor content_fill_color;
}


public class DsDiv
{

  public DsDiv()
  {
    _div_attribs = new DsDivAttribs() {
      border = 10,
      border_color = SKColor.Parse("000000"),
      content_fill_color = SKColor.Parse("DAE8FC")
    };
  }

  public DsDiv(DsDivAttribs div_attribs) {
    _div_attribs = div_attribs;
  }


  public void Draw(SKCanvas canvas, SKRect draw_rect)
  {
    // BORDER
    var border_rect = dim_algo.CalculateBorderRect(
      draw_rect, 
      _div_attribs.margin
    );

    SKPaint paint_border = new() { Color = _div_attribs.border_color, IsAntialias = true };
    canvas.DrawRect(border_rect, paint_border);

    // CONTENT
    var content_rec = dim_algo.CalculateContentRect(
      draw_rect, 
      _div_attribs.margin, 
      _div_attribs.border, 
      _div_attribs.padding
    );

    SKPaint paint_content = new() { Color = _div_attribs.content_fill_color, IsAntialias = true };
    canvas.DrawRect(content_rec, paint_content);
  }

  private DsDivAttribs _div_attribs;

  private DsRectDimensions dim_algo = new();


}
