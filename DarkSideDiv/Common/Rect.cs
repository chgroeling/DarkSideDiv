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

    public override string ToString()
    {
      return $"({Left}, {Top}, {Right}, {Bottom})";
    }
  }
}