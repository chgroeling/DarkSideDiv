using DarkSideDiv.Common;

namespace DarkSideDiv.Divs;


public interface IDsDivAlignedTextDevice
{
  FontMetrics Setup(DsDivAlignedTextAttribs attribs);

  Rect MeasureText(string str);

  void DrawText(string text, float x, float y);
}