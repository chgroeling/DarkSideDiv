using DarkSideDiv.Enums;

namespace DarkSideDiv.Common
{
  public struct RectDistance
  {
    public static implicit operator RectDistance(float value)
    {
      var ret = new RectDistance()
      {
        distance_from_left = (QuantityType.FixedInPixel, value),
        distance_from_right = (QuantityType.FixedInPixel, value),
        distance_from_bottom = (QuantityType.FixedInPixel, value),
        distance_from_top = (QuantityType.FixedInPixel, value)
      };
      return ret;
    }

    public RectDistance(Quantity value)
    {
      distance_from_left = value;
      distance_from_right = value;
      distance_from_bottom = value;
      distance_from_top = value;
    }


    public static implicit operator RectDistance((float left, float top, float right, float bottom) value)
    {
      var ret = new RectDistance()
      {
        distance_from_left = (QuantityType.FixedInPixel, value.left),
        distance_from_right = (QuantityType.FixedInPixel, value.right),
        distance_from_bottom = (QuantityType.FixedInPixel, value.bottom),
        distance_from_top = (QuantityType.FixedInPixel, value.top)
      };
      return ret;
    }

    public static implicit operator RectDistance((Quantity left, Quantity top, Quantity right, Quantity bottom) value)
    {
      var ret = new RectDistance()
      {
        distance_from_left =value.left,
        distance_from_right =value.right,
        distance_from_bottom = value.bottom,
        distance_from_top =value.top
      };
      return ret;
    }

    public Quantity distance_from_left;
    public Quantity distance_from_top;
    public Quantity distance_from_right;
    public Quantity distance_from_bottom;

    public static RectDistance operator +(RectDistance a, RectDistance b)
    {
      var ret = new RectDistance()
      {
        distance_from_left = (QuantityType.FixedInPixel, a.distance_from_left.Value + b.distance_from_left.Value),
        distance_from_top = (QuantityType.FixedInPixel, a.distance_from_top.Value + b.distance_from_top.Value),
        distance_from_right = (QuantityType.FixedInPixel, a.distance_from_right.Value + b.distance_from_right.Value),
        distance_from_bottom = (QuantityType.FixedInPixel, a.distance_from_bottom.Value + b.distance_from_bottom.Value)
      };
      return ret;
    }
  };
}