using DarkSideDiv.Common;
using DarkSideDiv.Enums;

namespace DarkSideDiv.Components
{
  public struct DsDivComponentAlignedTextAttribs
  {
    public string Text {
      get;
      set;
    }

    public DsAlignment Alignment {
      get;
      set;
    }
    public float TextSize {
      get;
      set;
    }

    public FontWeight FontWeight {
      get;
      set;
    }
  };
}