namespace DarkSideDiv.Common
{
  public struct DsDivRectDistance
  {
    public static implicit operator DsDivRectDistance(float value)
    {
      var ret = new DsDivRectDistance()
      {
        distance_from_left = value,
        distance_from_right = value,
        distance_from_bottom = value,
        distance_from_top = value
      };
      return ret;
    }

    public static implicit operator DsDivRectDistance((float left, float top, float right, float bottom) value)
    {
      var ret = new DsDivRectDistance()
      {
        distance_from_left = value.left,
        distance_from_right = value.right,
        distance_from_bottom = value.bottom,
        distance_from_top = value.top
      };
      return ret;
    }
    public float distance_from_left;
    public float distance_from_top;
    public float distance_from_right;
    public float distance_from_bottom;

    public static DsDivRectDistance operator +(DsDivRectDistance a, DsDivRectDistance b)
    {
      var ret = new DsDivRectDistance()
      {
        distance_from_left = a.distance_from_left + b.distance_from_left,
        distance_from_top = a.distance_from_top + b.distance_from_top,
        distance_from_right = a.distance_from_right + b.distance_from_right,
        distance_from_bottom = a.distance_from_bottom + b.distance_from_bottom
      };
      return ret;
    }
  };
}