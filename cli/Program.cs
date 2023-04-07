﻿using SkiaSharp;
using DarkSideDiv.Divs;
using CommandLine;
using MarkDigFacade;

internal class Program
{
  public class Options
  {
    // Required option: Set a input filename
    // E.g.:
    // -f filename
    // --filename filename
    [Option('f', "filename", Required = true, HelpText = "Input filename.")]
    // C# wants that the property of an immutable type is initialized.
    // Therefore I set the default value via the equal sign.
    public string InputFilename { get; set; } = string.Empty;


    // Used to enable verbose messages. Usually associated with logging.
    [Option('v', "verbose", Required = false, HelpText = "Set output to verbose messages.")]
    public bool Verbose { get; set; }
  }

  private static void Run(Options opts)
  {
    
    // Checking the existence of the specified
    if (!File.Exists(opts.InputFilename))
    {
     throw new FileNotFoundException($"File {opts.InputFilename} does not exists");
    }

    var text = File.ReadAllText(opts.InputFilename);
    Console.WriteLine(text);


    MarkDigExample.Test(text, "");

    // Create an image and fill it blue
    SKBitmap bmp = new SKBitmap(1024, 800);
    using SKCanvas canvas = new SKCanvas(bmp);
    canvas.Clear(SKColor.Parse("#003366"));

    var graph_rect = new SKRect(0.0f, 0.0f, bmp.Width, bmp.Height);
    var lotus_builder = new DsLotusBuilder(graph_rect);
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

  private static void HandleParseError(IEnumerable<Error> errors)
  {
    // handle errors
  }

  private static void Main(string[] args)
  {
    Parser.Default.ParseArguments<Options>(args)
          .WithParsed<Options>(Run)
          .WithNotParsed(HandleParseError); // errors is a sequence of type IEnumerable<Error>

  }

}