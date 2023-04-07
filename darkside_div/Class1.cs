using SkiaSharp;

namespace darkside_div;

public class Class1
{
  public void Draw(SKCanvas canvas, int width, int height)
  {
    Random rand = new(0);
    SKPaint paint = new() { Color = SKColors.White.WithAlpha(100), IsAntialias = true };
    for (int i = 0; i < 100; i++)
    {
      SKPoint pt1 = new(rand.Next(width), rand.Next(height));
      SKPoint pt2 = new(rand.Next(width), rand.Next(height));
      paint.StrokeWidth = rand.Next(1, 10);
      canvas.DrawLine(pt1, pt2, paint);
    }

  }
}
