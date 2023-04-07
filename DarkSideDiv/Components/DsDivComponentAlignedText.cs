using DarkSideDiv.Enums;
using DarkSideDiv.Common;

namespace DarkSideDiv.Components
{

  public class DsDivComponentAlignedText : IDsDivComponent
  {

    public IAbsoluteLayout AbsoluteLayoutAlgorithmn
    {
      get;
      set;
    } = new AbsoluteLayout();

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
      var lines = SplitLines(text, _device);

      var max_width_of_lines = (from i in lines select i.TextBounds.Width).Max();
      var accumulated_height_of_lines = (
        from i in lines select i.TextBounds.Height).Aggregate(0f, (bef, next) => { return bef + next; }
      );

      // This returns the rectangle of the text
      var combined_rect = new Rect(
        lines[0].TextBounds.Left,
        lines[0].TextBounds.Top,
        max_width_of_lines,
        lines[0].TextBounds.Top + accumulated_height_of_lines);

      // Skia coordinate system
      // ----> x
      // |
      // | y
      // V
      //var (x, y) = AbsoluteLayoutAlgorithmn.GetOffset(draw_rect, combined_rect, _attribs.alignment);
      var rect = AbsoluteLayoutAlgorithmn.GetAbsRect(draw_rect, combined_rect, _attribs.alignment, 0f, 0f);
      
      var abs_left = rect.Left;
      var abs_top = rect.Top;

      float top_offset = lines[0].TextBounds.Height; // first line offset;

      foreach (var l in lines)
      {
        float left_offset = CalcHorizontalElementAlignmentOffset(
          combined_rect.Left,
          combined_rect.Right,
          l.TextBounds.Width);

        // The text origin (0,0) is the bottom left corner of the text
        // the top coordinate is therefore negative
        _device.DrawText(
          l.Value,
          abs_left + left_offset,
          abs_top + top_offset);

        top_offset = top_offset + l.TextBounds.Height;
      }
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

    private DsDivComponentAlignedTextAttribs _attribs;
  }
}