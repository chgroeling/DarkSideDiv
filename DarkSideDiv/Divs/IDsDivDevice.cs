using DarkSideDiv.Common;

namespace DarkSideDiv.Divs
{
  public interface IDsDivDevice
  {
    void Setup(DsDivAttribs attribs);
    void DrawBorderRect(Rect border_rect);
    void DrawContentRect(Rect content_rect);
  }
}