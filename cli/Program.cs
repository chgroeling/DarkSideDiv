using SkiaSharp;
using DarkSideDiv;
using System;

internal class Program
{
  private static void Main(string[] args)
  {
    Console.WriteLine("Hello, World!");


    // Create an image and fill it blue
    SKBitmap bmp = new SKBitmap(640, 480);
    using SKCanvas canvas = new SKCanvas(bmp);
    canvas.Clear(SKColor.Parse("#003366"));

    // Draw lines with random positions and thicknesses
    Random rand = new Random(0);
    var classobj = new Class1();
  
    classobj.Draw(canvas, bmp.Width, bmp.Height);

    var dsroot = new DsRoot(new SKRect(0.0f, 0.0f, bmp.Width, bmp.Height));
    var dsdiv1 = new DsDiv();
    
    var grid_attribs = DsUniformDivAttribs.Default();
    grid_attribs.rows = 4;
    grid_attribs.cols = 4;
    grid_attribs.content_fill_color = SKColor.Parse("FFCCCC");
    var dsrootdiv = new DsUniformGrid(grid_attribs);

    dsrootdiv.Attach(2,2, dsdiv1);
    dsrootdiv.Attach(0,0, dsdiv1);
    dsrootdiv.Attach(3,3, dsdiv1);
    dsrootdiv.Attach(3,0, dsdiv1);

    dsroot.Attach(dsrootdiv);
    dsroot.Draw(canvas);
    // Save the image to disk
    SKFileWStream fs = new SKFileWStream("quickstart.jpg");
    bmp.Encode(fs, SKEncodedImageFormat.Jpeg, quality: 85);

  }
}