using SkiaSharp; // TODO: Move to Infrastructure
using Application.Common.Interfaces;

namespace Application;

public class UseCaseCreateLotusDiagram
{
  private IReadTextFile _read_text_file;
  
  private IDocumentConverter _document_converter;

  public UseCaseCreateLotusDiagram(IDocumentConverter document_converter, IReadTextFile read_text_file) {
    _read_text_file = read_text_file;
    _document_converter = document_converter;
  }

  public void Execute( string input_filename, string output_filename)
  {
    // Create an image and fill it blue
    SKBitmap bmp = new SKBitmap(1600, 900); // 5::3
    using SKCanvas canvas = new SKCanvas(bmp);
    canvas.Clear(SKColor.Parse("#003366"));

    var graph_rect = new SKRect(0.0f, 0.0f, bmp.Width, bmp.Height);
    var lotus_builder = new Builders.DsLotusBuilder(graph_rect);

    var text = _read_text_file.Read(input_filename);
    var topic_list = _document_converter.GetTableOfContents(text);

    foreach(var i in topic_list) {
      lotus_builder.AddTopic(i.level,i.label??"undefined");
    }

    var ds_root = lotus_builder.Build();

    // Draw Lotus
    ds_root.Draw(canvas);

    // Save the image to disk
    SKFileWStream fs = new SKFileWStream(output_filename);
    bmp.Encode(fs, SKEncodedImageFormat.Jpeg, quality: 90);
  }

}
