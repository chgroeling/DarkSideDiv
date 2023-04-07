using SkiaSharp;

namespace DarkSideDiv
{
  public struct DsDivComponentAlignedText
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

    public DsDivAlignedTextComponent(DsDivComponentAlignedText attribs)
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
      var ret = lines.SelectMany((line) =>
      {
        var result = new List<Line>();
        SKRect textBounds = new SKRect();
        paint.MeasureText(line, ref textBounds);
        result.Add(new Line() { Value = line, TextBounds = textBounds });

        return result.ToArray();
      }).ToArray();

      return ret;
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

      var max_width_of_lines = (from i in lines select i.TextBounds.Width).Max();
      var accumulated_height_of_lines = (from i in lines select i.TextBounds.Height).Aggregate(0f, (bef, next) => { return bef + next; });

      // This returns the rectangle of the text
      var combined_rect = new SKRect(
        lines[0].TextBounds.Left,
        lines[0].TextBounds.Top,
        max_width_of_lines,
        accumulated_height_of_lines);

      // Skia coordinate system
      // ----> x
      // |
      // | y
      // V
      CalcOrigin(draw_rect, combined_rect, out x, out y);

      // take BOTTOM value here, since measureText returns a negative top value to include the ascent
      float y_offset = CalcVerticalTextOffset(combined_rect.Bottom, lines[0].TextBounds.Height);

      foreach (var l in lines)
      {
        float x_offset = CalcHorizontalElementAlignmentOffset(
          combined_rect.Left, 
          combined_rect.Right, 
          l.TextBounds.Width);

        // The text origin (0,0) is the bottom left corner of the text
        // the top coordinate is therefore negative
        canvas.DrawText(
          l.Value,
          x + x_offset,
          y + y_offset,
          textPaint);
        y_offset = y_offset + l.TextBounds.Height;
      }
      //canvas.Restore();
    }

    private float CalcVerticalTextOffset(float block_bottom, float line_height)
    {
      // The origin of every line is the bottom left corner.
      // -->The text is displayed shifted by one line.
      float y_offset;
      switch (_attribs.alignment)
      {
        case DsAlignment.TopRight:
        case DsAlignment.TopLeft:
        case DsAlignment.Top:
          y_offset = 0;
          break;

        case DsAlignment.BottomLeft:
        case DsAlignment.BottomRight:
        case DsAlignment.Bottom:
          y_offset = -(block_bottom - line_height) ;
          break;

        case DsAlignment.Left:
        case DsAlignment.Right:
        case DsAlignment.Center:
        default:
          y_offset = -(block_bottom - line_height) * 0.5f;
          break;
      }

      return y_offset;
    }

    private float CalcHorizontalElementAlignmentOffset(float left, float right, float element_width)
    {
      float x_offset;

      switch (_attribs.alignment)
      {
        case DsAlignment.TopLeft:
        case DsAlignment.Left:
        case DsAlignment.BottomLeft:
          x_offset = left;
          break;

        case DsAlignment.TopRight:
        case DsAlignment.Right:
        case DsAlignment.BottomRight:
          x_offset = right - element_width;
          break;


        case DsAlignment.Top:
        case DsAlignment.Center:
        case DsAlignment.Bottom:
        default:
          x_offset = left + (right - left) * 0.5f - element_width * 0.5f;
          break;
      }
      return x_offset;
    }

    private void CalcOrigin(SKRect draw_rect, SKRect content_rect, out float x, out float y)
    {
      // the origin of a block is always the bottom left corner of it.
      // |
      // |
      // |X <--
      // ------
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

    private DsDivComponentAlignedText _attribs;
  }
}