using DarkSideDiv.Common;

namespace DarkSideDiv.Divs
{
  public interface IDsDiv
  {
    void Draw(Rect parent_content, Rect nearest_positioned_ancestor);
  }
}