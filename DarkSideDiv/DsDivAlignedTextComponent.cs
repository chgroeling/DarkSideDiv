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

    public class Line
    {
      public string Value { get; set; }

      public SKRect TextBounds { get; set; }
    }


    private Line[] SplitLines(string text, SKPaint paint)
    {
      var lines = text.Split('\n');

      return lines.SelectMany((line) =>
      {
        var result = new List<Line>();
        SKRect textBounds = new SKRect();
        paint.MeasureText(line, ref textBounds);
        result.Add(new Line() { Value = line, TextBounds = textBounds });

        return result.ToArray();
      }).ToArray();
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

      float x, y;

      var lines = SplitLines(text, textPaint);

      var max_width = (from i in lines select i.TextBounds.Width).Max();
      var accu_height = (from i in lines select i.TextBounds.Height).Aggregate(0f, (bef, next) => { return bef + next; });

      var new_rect = new SKRect(
        lines[0].TextBounds.Left,
        lines[0].TextBounds.Top,
        max_width,
        accu_height);

      CalcOrigin(draw_rect, new_rect, out x, out y);


      //var meas = -textBounds.Top;
      //SKPaint paint_border = new SKPaint();
      //canvas.DrawRect(x,y-meas, 100f, meas, paint_border);

      //canvas.Save();
      // canvas.Translate(draw_rect.Left,
      //  draw_rect.Bottom);
      //canvas.Scale(0.5f,0.5f);

      float y_offset;
      switch (_attribs.alignment)
      {
        case DsAlignment.TopRight:
        case DsAlignment.TopLeft:
        case DsAlignment.Top:
          y_offset =  0f;
          break;

        case DsAlignment.BottomLeft:
        case DsAlignment.BottomRight:
        case DsAlignment.Bottom:
          y_offset = -(accu_height - lines[0].TextBounds.Height) * 1.0f;
          break;

        case DsAlignment.Left:
        case DsAlignment.Right:
        default:
          y_offset = -(accu_height - lines[0].TextBounds.Height) * 0.5f;
          break;
      }
      foreach (var l in lines)
      {
        canvas.DrawText(l.Value,
          x,
          y + y_offset,
          textPaint
        );
        y_offset = y_offset + l.TextBounds.Height;
      }
      //canvas.Restore();
    }

    private void CalcOrigin(SKRect draw_rect, SKRect content_rect, out float x, out float y)
    {
      x = draw_rect.Left;
      y = draw_rect.Bottom;
      switch (_attribs.alignment)
      {
        case DsAlignment.Left:
          x = draw_rect.Left;
          y = draw_rect.Bottom + (draw_rect.Top - draw_rect.Bottom) * 0.5f - content_rect.Top * 0.5f;
          break;

        case DsAlignment.TopLeft:
          x = draw_rect.Left;
          y = draw_rect.Top - content_rect.Top;
          break;

        case DsAlignment.Right:
          x = draw_rect.Right - content_rect.Width;
          y = draw_rect.Bottom + (draw_rect.Top - draw_rect.Bottom) * 0.5f - content_rect.Top * 0.5f;
          break;

        case DsAlignment.TopRight:
          x = draw_rect.Right - content_rect.Width;
          y = draw_rect.Top - content_rect.Top;
          break;

        case DsAlignment.BottomRight:
          x = draw_rect.Right - content_rect.Width;
          y = draw_rect.Bottom;
          break;

        case DsAlignment.Bottom:
          x = draw_rect.Left + (draw_rect.Right - draw_rect.Left) * 0.5f - content_rect.Width * 0.5f;
          y = draw_rect.Bottom;
          break;

        case DsAlignment.Top:
          x = draw_rect.Left + (draw_rect.Right - draw_rect.Left) * 0.5f - content_rect.Width * 0.5f;
          y = draw_rect.Top - content_rect.Top;
          break;

        case DsAlignment.Center:
          x = draw_rect.Left + (draw_rect.Right - draw_rect.Left) * 0.5f - content_rect.Width * 0.5f;
          y = draw_rect.Bottom + (draw_rect.Top - draw_rect.Bottom) * 0.5f - content_rect.Top * 0.5f;
          break;

        case DsAlignment.BottomLeft:
        default:
          // do nothing
          break;
      }
    }

    private DsDivAlignedTextComponentAttribs _attribs;
  }
}