using Application.Common;
using DarkSideDiv.Common;
using DarkSideDiv.Divs;
using SkiaSharp;

namespace Application.Device
{
  public class DsDivSkia : IDsDivDevice
  {
    public DsDivSkia()
    {
    }

    public void Setup(DsDivAttribs attribs)
    {
      _attribs = attribs;
    }

    SKCanvas? _canvas = null;

    public void SetCanvas(SKCanvas canvas)
    {
      _canvas = canvas;
    }

    public void DrawBorderRect(Rect border_rect)
    {
      if (_canvas == null)
      {
        throw new Exception("Canvas not initialized");
      }

      var paint_border = new SKPaint();
      paint_border.Color = ConversionFactories.FromColorString(_attribs.border_color);
      paint_border.IsAntialias = true;

      _canvas.DrawRect(ConversionFactories.FromRect(border_rect), paint_border);

    }
    public void DrawContentRect(Rect content_rect)
    {
      if (_canvas == null)
      {
        throw new Exception("Canvas not initialized");
      }

      var paint_content = new SKPaint()
      {
        Color = ConversionFactories.FromColorString(_attribs.content_fill_color),
        IsAntialias = true
      };
      _canvas.DrawRect(ConversionFactories.FromRect(content_rect), paint_content);
    }

    private DsDivAttribs _attribs;

  }
}