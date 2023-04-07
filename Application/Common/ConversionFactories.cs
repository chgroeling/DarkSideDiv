using DarkSideDiv.Common;
using SkiaSharp;

namespace Application.Common
{
  public class ConversionFactories
  {
    public static SKRect FromRect(Rect rect)
    {
      return new SKRect(rect.Left, rect.Top, rect.Right, rect.Bottom);
    }

    public static Rect ToRect(SKRect rect)
    {
      return new Rect(rect.Left, rect.Top, rect.Right, rect.Bottom);
    }

    public static SKColor FromColorString(ColorString color_string)
    {
      return SKColor.Parse(color_string.color_string);
    }


  }
}