namespace DarkSideDiv.Common
{
  public struct Rect
  {
    public Rect(float left, float top, float right, float bottom)
    {
      Left = left;
      Top = top;
      Right = right;
      Bottom = bottom;
    }

    public float Left { get; }   //!< smaller x-axis bounds
    public float Top { get; }   //!< smaller y-axis bounds
    public float Right { get; }  //!< larger x-axis bounds
    public float Bottom { get; } //!< larger y-axis bounds

    public float Height { get { return Bottom - Top; } }
    public float Width { get { return Right - Left; } }

    private bool AlmostEqual(float val1, float val2, float difference)
    {
      return (Math.Abs(val1 - val2) <= difference);
    }

    private const float NEAR_ZERO = 1e-12f;
    public bool IsAlmostEqual(Rect rect)
    {
      if (!AlmostEqual(Left, rect.Left, NEAR_ZERO)) return false;
      if (!AlmostEqual(Right, rect.Right, NEAR_ZERO)) return false;
      if (!AlmostEqual(Top, rect.Top, NEAR_ZERO)) return false;
      if (!AlmostEqual(Bottom, rect.Bottom, NEAR_ZERO)) return false;

      return true;
    }

  }
}