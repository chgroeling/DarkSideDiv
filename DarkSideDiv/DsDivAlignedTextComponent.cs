using SkiaSharp;

namespace DarkSideDiv
{
  public struct DsDivAlignedTextComponentAttribs
  {

    public string text;

    public DsAlignment alignment;
    public float text_size;
  };

  public class DsDivAlignedTextComponent : IDsDivComponent
  {

    public DsDivAlignedTextComponent() 
    {
    }

    public DsDivAlignedTextComponent(DsDivAlignedTextComponentAttribs attribs)
    {
      _attribs = attribs;
    }

    public void Draw(SKCanvas canvas, SKRect draw_rect)
    {

      var textPaint = new SKPaint() // with object initializer
      {
        IsAntialias = true,
        TextAlign = SKTextAlign.Left,
        Color = SKColors.Black,
        TextSize = _attribs.text_size,
        Typeface = SKTypeface.FromFamilyName("Arial", SKFontStyleWeight.Bold,
              SKFontStyleWidth.Normal,
              SKFontStyleSlant.Upright)
      };
      var text = _attribs.text;

      // Get the bounds of the text
      SKRect textBounds = new SKRect();
      textPaint.MeasureText(text, ref textBounds);

      var x = draw_rect.Left;
      var y = draw_rect.Bottom;

      switch (_attribs.alignment)
      {
        case DsAlignment.Left:
          x = draw_rect.Left;
          y = draw_rect.Bottom + (draw_rect.Top - draw_rect.Bottom) * 0.5f - textBounds.Top * 0.5f;
          break;

        case DsAlignment.TopLeft:
          x = draw_rect.Left;
          y = draw_rect.Top - textBounds.Top;
          break;

        case DsAlignment.Right:
          x = draw_rect.Right - textBounds.Width;
          y = draw_rect.Bottom + (draw_rect.Top - draw_rect.Bottom) * 0.5f - textBounds.Top * 0.5f;
          break;

        case DsAlignment.TopRight:
          x = draw_rect.Right - textBounds.Width;
          y = draw_rect.Top - textBounds.Top;
          break;

        case DsAlignment.BottomRight:
          x = draw_rect.Right - textBounds.Width;
          y = draw_rect.Bottom;
          break;

        case DsAlignment.Bottom:
          x = draw_rect.Left + (draw_rect.Right - draw_rect.Left) * 0.5f - textBounds.Width * 0.5f;
          y = draw_rect.Bottom;
          break;

        case DsAlignment.Top:
          x = draw_rect.Left + (draw_rect.Right - draw_rect.Left) * 0.5f - textBounds.Width * 0.5f;
          y = draw_rect.Top - textBounds.Top;
          break;

        case DsAlignment.Center:
          x = draw_rect.Left + (draw_rect.Right - draw_rect.Left) * 0.5f - textBounds.Width * 0.5f;
          y = draw_rect.Bottom + (draw_rect.Top - draw_rect.Bottom) * 0.5f - textBounds.Top * 0.5f;
          break;

        case DsAlignment.BottomLeft:
        default:
          // do nothing
          break;
      }

      //var meas = -textBounds.Top;
      //SKPaint paint_border = new SKPaint();
      //canvas.DrawRect(x,y-meas, 100f, meas, paint_border);

      //canvas.Save();
      // canvas.Translate(draw_rect.Left,
      //  draw_rect.Bottom);
      //canvas.Scale(0.5f,0.5f);
      canvas.DrawText(text,
        x,
        y,
        textPaint
      );
      //canvas.Restore();
    }

    private DsDivAlignedTextComponentAttribs _attribs;
  }
}