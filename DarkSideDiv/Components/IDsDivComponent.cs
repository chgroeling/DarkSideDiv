using SkiaSharp;

namespace DarkSideDiv.Components
{
  public interface IDsDivComponent
  {
    void Draw(SKCanvas canvas, SKRect draw_rect);
  }
}