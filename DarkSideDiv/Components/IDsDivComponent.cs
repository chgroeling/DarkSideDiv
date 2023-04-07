using DarkSideDiv.Common;
using SkiaSharp;

namespace DarkSideDiv.Components
{
  public interface IDsDivComponent
  {
    void Draw(SKCanvas canvas, Rect draw_rect);
  }
}