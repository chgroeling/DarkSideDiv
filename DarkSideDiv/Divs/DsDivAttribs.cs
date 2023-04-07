using DarkSideDiv.Common;
using SkiaSharp;

namespace DarkSideDiv.Divs
{
  public struct DsDivAttribs
  {
    public SKColor border_color;

    public SKColor content_fill_color;


    public RectDistance Border
    {
      get;
      set;
    }

    public RectDistance Margin
    {
      get;
      set;
    }
    public RectDistance Padding
    {
      get;
      set;
    }
  }
}