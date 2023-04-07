﻿using SkiaSharp; // TODO: Move to Infrastructure

namespace Application;

public class UseCases
{
  public static void CreateLotusDiagram()
  {
    // Create an image and fill it blue
    SKBitmap bmp = new SKBitmap(1024, 800);
    using SKCanvas canvas = new SKCanvas(bmp);
    canvas.Clear(SKColor.Parse("#003366"));

    var graph_rect = new SKRect(0.0f, 0.0f, bmp.Width, bmp.Height);
    var lotus_builder = new Builders.DsLotusBuilder(graph_rect);
    lotus_builder.AddLevel1("I\nMultiline");
    lotus_builder.AddLevel2("A");
    lotus_builder.AddLevel3("A1\nA1.1");
    lotus_builder.AddLevel3("A2");
    lotus_builder.AddLevel3("A3");
    lotus_builder.AddLevel3("A4");
    lotus_builder.AddLevel3("A5");
    lotus_builder.AddLevel3("A6");
    lotus_builder.AddLevel3("A7");
    lotus_builder.AddLevel3("A8");
    lotus_builder.AddLevel2("B");
    lotus_builder.AddLevel2("C");
    lotus_builder.AddLevel2("D");
    lotus_builder.AddLevel2("E");
    lotus_builder.AddLevel2("F");
    lotus_builder.AddLevel2("G");
    lotus_builder.AddLevel3("I");
    lotus_builder.AddLevel3("II");
    lotus_builder.AddLevel3("III\nABCD\nDEF2");

    lotus_builder.AddLevel2("H");

    var ds_root = lotus_builder.Build();

    // Draw Lotus
    ds_root.Draw(canvas);

    // Save the image to disk
    SKFileWStream fs = new SKFileWStream("quickstart.jpg");
    bmp.Encode(fs, SKEncodedImageFormat.Jpeg, quality: 85);
  }

}
