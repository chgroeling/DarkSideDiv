using DarkSideDiv.Common;
using DarkSideDiv.Enums;

namespace DarkSideDiv.Divs
{
  public struct DsDivAttribs
  {
    public DsDivAttribs()
    {
    }

    public ColorString? BorderColor {
      get;
      set;
    } = null;

    public ColorString? ContentFillColor {
      get;
      set;
    } = null;

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