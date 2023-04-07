using DarkSideDiv.Common;
using SkiaSharp;

namespace DarkSideDiv.Divs
{
  public struct DsDivAttribs
  {
    public SKColor border_color;

    public SKColor content_fill_color;


    public DsDivRectDistance Border
    {
      get;
      set;
    }

    public DsDivRectDistance Margin
    {
      get;
      set;
    }
    public DsDivRectDistance Padding
    {
      get;
      set;
    }
  }
}