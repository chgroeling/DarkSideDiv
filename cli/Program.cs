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

    var dstext_attribs1 = DsDivAlignedTextComponentAttribs.Default();
    dstext_attribs1.text="TopLeft";
    var dstext_attribs2 = DsDivAlignedTextComponentAttribs.Default();
    dstext_attribs2.text="BottomRight";
    dstext_attribs2.alignment = DsAlignment.BottomRight;


    var dstext1 = new DsDivAlignedTextComponent(dstext_attribs1);
    var dstext2 = new DsDivAlignedTextComponent(dstext_attribs2);

    var dsdiv1 = new DsDiv();
    dsdiv1.Append(dstext1);

    var dsdiv1_1 = new DsDiv();
    dsdiv1.Append(dstext2);

    var dsdiv2 = new DsDiv();

    
    var grid_attribs = DsUniformDivAttribs.Default();
    grid_attribs.rows = 4;
    grid_attribs.cols = 4;
    grid_attribs.content_fill_color = SKColor.Parse("FFCCCC");

    var ds_grid2 = new DsUniformGrid(grid_attribs);
    ds_grid2.Attach(2,2, dsdiv2);
    ds_grid2.Attach(0,0, dsdiv2);
    ds_grid2.Attach(3,3, dsdiv2);
    ds_grid2.Attach(3,0, dsdiv2);

    var ds_grid1 = new DsUniformGrid(grid_attribs);
    ds_grid1.Attach(2,2, dsdiv1);
    ds_grid1.Attach(0,0, dsdiv1);
    ds_grid1.Attach(3,3, dsdiv1);
    ds_grid1.Attach(3,0, dsdiv1);
    ds_grid1.Attach(1,1, ds_grid2);


    
    dsroot.Attach(ds_grid1);
    dsroot.Draw(canvas);
    // Save the image to disk
    SKFileWStream fs = new SKFileWStream("quickstart.jpg");
    bmp.Encode(fs, SKEncodedImageFormat.Jpeg, quality: 85);

  }
}