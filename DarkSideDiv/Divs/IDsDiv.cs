using SkiaSharp;

namespace DarkSideDiv.Divs
{
  public interface IDsDiv
  {
    void Draw(SKCanvas canvas, SKRect draw_rect);
  }
}