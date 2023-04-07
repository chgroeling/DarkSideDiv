namespace DarkSideDiv.Common
{
  public class RectDimensions
  {
    public Rect Shrink(Rect rect, float value)
    {
      return Shrink(rect, value, value, value, value);
    }

    public Rect Shrink(Rect rect, float left, float top, float right, float bottom)
    {
      var ret = new Rect(rect.Left + left, rect.Top + top, rect.Right - right, rect.Bottom - right);
      return ret;
    }

    public Rect Shrink(Rect rect, RectDistance rect_dims)
    {
      var ret = new Rect(rect.Left + rect_dims.distance_from_left,
                           rect.Top + rect_dims.distance_from_top,
                           rect.Right - rect_dims.distance_from_right,
                           rect.Bottom - rect_dims.distance_from_bottom);
      return ret;
    }


    public Rect CalculateBorderRect(Rect outer_rect, RectDistance margin)
    {
      return Shrink(outer_rect, margin);
    }

    public Rect CalculatePaddingRect(Rect outer_rect, RectDistance margin, RectDistance border)
    {
      return new Rect();
    }

    public Rect CalculateContentRect(Rect outer_rect, RectDistance margin, RectDistance border, RectDistance padding)
    {
      var res = margin + border + padding;
      return Shrink(outer_rect, res);
    }
  }
}