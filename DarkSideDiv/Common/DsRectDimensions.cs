using SkiaSharp;

namespace DarkSideDiv.Common
{
  public class DsRectDimensions
  {
    public SKRect Shrink(SKRect rect, float value)
    {
      return Shrink(rect, value, value, value, value);
    }

    public SKRect Shrink(SKRect rect, float left, float top, float right, float bottom)
    {
      var ret = new SKRect(rect.Left + left, rect.Top + top, rect.Right - right, rect.Bottom - right);
      return ret;
    }

    public SKRect Shrink(SKRect rect, DsDivRectDistance rect_dims)
    {
      var ret = new SKRect(rect.Left + rect_dims.distance_from_left,
                           rect.Top + rect_dims.distance_from_top,
                           rect.Right - rect_dims.distance_from_right,
                           rect.Bottom - rect_dims.distance_from_bottom);
      return ret;
    }


    public SKRect CalculateBorderRect(SKRect outer_rect, DsDivRectDistance margin)
    {
      return Shrink(outer_rect, margin);
    }

    public SKRect CalculatePaddingRect(SKRect outer_rect, DsDivRectDistance margin, DsDivRectDistance border)
    {
      return new SKRect();
    }

    public SKRect CalculateContentRect(SKRect outer_rect, DsDivRectDistance margin, DsDivRectDistance border, DsDivRectDistance padding)
    {
      var res = margin + border + padding;
      return Shrink(outer_rect, res);
    }
  }
}