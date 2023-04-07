using DarkSideDiv.Common;
using DarkSideDiv.Enums;

namespace DarkSideDiv.Divs
{
  public struct DsDivAttribs
  {
    public DsDivAttribs()
    {
      border_color = new ColorString();
      content_fill_color = new ColorString();
    }

    public ColorString border_color;

    public ColorString content_fill_color;

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