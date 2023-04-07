using DarkSideDiv.Enums;

namespace DarkSideDiv.Components
{
  public enum FontWeight {
    Normal = 0, // default

    Bold
  };
  public struct DsDivComponentAlignedTextAttribs
  {
    public string text;
    public DsAlignment alignment;
    public float text_size;

    public FontWeight font_weight;
  };
}