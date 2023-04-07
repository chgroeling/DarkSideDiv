using DarkSideDiv.Common;
using DarkSideDiv.Enums;

namespace DarkSideDiv.Divs
{
  public struct DsDivAttribs
  {
    public DsDivAttribs()
    {
    }

    public ColorString? border_color = null;

    public ColorString? content_fill_color = null;

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

    public PositionType Position
    {
      get;
      set;
    }

    public HeightType Height
    {
      get;
      set;
    }
  }
}