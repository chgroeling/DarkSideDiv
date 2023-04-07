using DarkSideDiv.Enums;
using DarkSideDiv.Common;

namespace DarkSideDiv.Components
{

  public class DsDivComponentAlignedText : IDsDivComponent
  {

    public DsDivComponentAlignedText(IDsDivComponentAlignedTextDevice device) : this(device, new DsDivComponentAlignedTextAttribs())
    {
    }

    public DsDivComponentAlignedText(IDsDivComponentAlignedTextDevice device, DsDivComponentAlignedTextAttribs attribs)
    {
      _device = device;
      _attribs = attribs;
    }

    IDsDivComponentAlignedTextDevice _device;

    private class Line
    {
      public string Value { get; set; } = string.Empty;

      public Rect TextBounds { get; set; }
    }


    private Line[] SplitLines(string text, IDsDivComponentAlignedTextDevice device)
    {
      var lines = text.Split('\n');
      var ret = lines.SelectMany((line) =>
      {
        var result = new List<Line>();
        var textBounds = device.MeasureText(line);

        var item = new Line()
        {
          Value = line,
          TextBounds = textBounds
        };
        result.Add(item);

        return result.ToArray();
      }).ToArray();

      return ret;
    }

    public void Draw(Rect draw_rect)
    {
      // _device.SetCanvas(canvas);
      _device.Setup(_attribs);

      var text = _attribs.text;
      float x, y;

      var lines = SplitLines(text, _device);

      var max_width_of_lines = (from i in lines select i.TextBounds.Width).Max();
      var accumulated_height_of_lines = (from i in lines select i.TextBounds.Height).Aggregate(0f, (bef, next) => { return bef + next; });

      // This returns the rectangle of the text
      var combined_rect = new Rect(
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
        _device.DrawText(
          l.Value,
          x + x_offset,
          y + y_offset);
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
          y_offset = -(block_bottom - line_height);
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

    private void CalcOrigin(Rect draw_rect, Rect content_rect, out float x, out float y)
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

    private DsDivComponentAlignedTextAttribs _attribs;
  }
}