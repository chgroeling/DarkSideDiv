using SkiaSharp;
using DarkSideDiv.Common;

namespace DarkSideDiv.Divs
{
  public interface IDsDiv
  {
    void Draw(SKCanvas canvas, Rect draw_rect);
  }
}