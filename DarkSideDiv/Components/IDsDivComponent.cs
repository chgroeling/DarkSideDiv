using DarkSideDiv.Common;

namespace DarkSideDiv.Components
{
  public interface IDsDivComponent
  {
    void Draw(Rect parent_content, Rect nearest_positioned_ancestor);
  }
}