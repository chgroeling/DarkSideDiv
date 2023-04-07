using SkiaSharp;
using DarkSideDiv.Common;
using DarkSideDiv.Components;
using Application.Common;

namespace Application.Device;

public class DsDivComponentAlignedTextSkia : IDsDivComponentAlignedTextDevice
{
  public DsDivComponentAlignedTextSkia()
  {

  }

  public void SetCanvas(SKCanvas canvas)
  {
    _canvas = canvas;
  }

  public FontMetrics Setup(DsDivComponentAlignedTextAttribs attribs)
  {
    var sk_font_weight = SKFontStyleWeight.Normal;

    switch (attribs.FontWeight)
    {
      case FontWeight.Normal:
        sk_font_weight = SKFontStyleWeight.Normal;
        break;

      case FontWeight.Bold:
        sk_font_weight = SKFontStyleWeight.Bold;
        break;

      default:
        break;
    }
    _text_paint = new SKPaint() // with object initializer
    {
      IsAntialias = true,
      TextAlign = SKTextAlign.Left,
      Color = SKColors.Black,
      TextSize = attribs.TextSize,
      Typeface = SKTypeface.FromFamilyName("Roboto",
            sk_font_weight,
            SKFontStyleWidth.Normal,
            SKFontStyleSlant.Upright)
    };

    var fm = _text_paint.FontMetrics;
    return new FontMetrics()
    {
      Leading = fm.Leading,
      Ascent = fm.Ascent,
      Descent = fm.Descent
    };
  }

  public Rect MeasureText(string str)
  {
    if (_text_paint == null)
    {
      throw new Exception("Setup was not called");
    }

    var textBounds = new SKRect();
    _text_paint.MeasureText(str, ref textBounds);
    var fm = _text_paint.FontMetrics;
    return ConversionFactories.ToRect(textBounds);
  }

  public void DrawText(string text, float x, float y)
  {
    if (_text_paint == null)
    {
      throw new Exception("Setup was not called");
    }
    if (_canvas == null)
    {
      throw new Exception("Canvas was not set");
    }
    _canvas.DrawText(
        text,
        x,
        y,
        _text_paint);
  }

  SKPaint? _text_paint = null;
  SKCanvas? _canvas = null;

}