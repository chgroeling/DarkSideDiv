using DarkSideDiv.Enums;

namespace DarkSideDiv.Common
{
  public class RectDimensions
  {
    public float GetDistanceInPixel(Quantity quantity, float width)
    {
      var re = 0f;
      if (quantity.QType == QuantityType.Percent)
      {
        re = width * quantity.Value * 0.01f;
      }
      else
      {
        re = quantity.Value;
      }
      return re;
    }
    public Rect Shrink(Rect input_rect, Rect parent_rect, RectDistance rect_dims)
    {
      var width = parent_rect.Width;
      var left = GetDistanceInPixel(rect_dims.distance_from_left, width);
      var right = GetDistanceInPixel(rect_dims.distance_from_right, width);
      var top = GetDistanceInPixel(rect_dims.distance_from_top, width);
      var bottom = GetDistanceInPixel(rect_dims.distance_from_bottom, width);


      var ret = new Rect(
        input_rect.Left + left,
        input_rect.Top + top,
        input_rect.Right - right,
        input_rect.Bottom - bottom);
      return ret;
    }


    public Rect CalculateBorderRect(Rect parent_rect, RectDistance margin)
    {
      var after_margin = Shrink(parent_rect, parent_rect, margin);
      return after_margin;
    }

    public Rect CalculatePaddingRect(Rect parent_rect, RectDistance margin, RectDistance border)
    {
      var after_margin = Shrink(parent_rect, parent_rect, margin);
      var after_border = Shrink(after_margin, parent_rect, border);
      return after_border;
    }

    public Rect CalculateContentRect(Rect parent_rect, RectDistance margin, RectDistance border, RectDistance padding)
    {
      var after_margin = Shrink(parent_rect, parent_rect, margin);
      var after_border = Shrink(after_margin, parent_rect, border);
      var after_padding = Shrink(after_border, parent_rect, padding);
      return after_padding;
    }
  }
}