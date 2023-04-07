using SkiaSharp;
using DarkSideDiv;

internal class Program
{
  private static void Main(string[] args)
  {
    Console.WriteLine("Hello, World!");


    // Create an image and fill it blue
    SKBitmap bmp = new(640, 480);
    using SKCanvas canvas = new(bmp);
    canvas.Clear(SKColor.Parse("#003366"));

    // Draw lines with random positions and thicknesses
    Random rand = new(0);
    var classobj = new Class1();
  
    classobj.Draw(canvas, bmp.Width, bmp.Height);

    var dsroot = new DsRoot(new SKRect(0.0f, 0.0f, bmp.Width, bmp.Height));
    var dsrootdiv = new DsDiv();
    dsroot.Attach(dsrootdiv);
    dsroot.Draw(canvas);
    // Save the image to disk
    SKFileWStream fs = new("quickstart.jpg");
    bmp.Encode(fs, SKEncodedImageFormat.Jpeg, quality: 85);

  }
}