using DarkSideDiv.Common;

namespace DarkSideDiv.Components
{
  
  public interface IDsDivComponentAlignedTextDevice
  {
    FontMetrics Setup(DsDivComponentAlignedTextAttribs attribs);

    Rect MeasureText(string str);

    void DrawText(string text, float x, float y);
  }
}