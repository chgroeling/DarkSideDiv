using SkiaSharp;
using System;
namespace DarkSideDiv {

public class Class1
{
  public int DoSomething() {
    return 1;
  }
  
  public void Draw(SKCanvas canvas, int width, int height)
  {
    var rand = new Random(0);
    var paint = new SKPaint() { Color = SKColors.White.WithAlpha(100), IsAntialias = true };
    for (int i = 0; i < 100; i++)
    {
       var pt1 = new SKPoint(rand.Next(width), rand.Next(height));
       var pt2 = new SKPoint(rand.Next(width), rand.Next(height));
      paint.StrokeWidth = rand.Next(1, 10);
      canvas.DrawLine(pt1, pt2, paint);
    }

  }
}
}