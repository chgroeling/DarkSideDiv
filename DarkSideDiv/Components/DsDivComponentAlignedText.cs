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
    } = new AbsoluteLayoutAlgorithmn();

    public IGridLayout GridLayoutAlgorithmn
    {
      get;
      set;
    } = new GridLayoutAlgorithmn();

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
      var abs_rect = AbsoluteLayoutAlgorithmn.GetAbsRect(draw_rect, combined_rect, _attribs.alignment, 0f, 0f);

      var grid_layout = GridLayoutAlgorithmn;

      var row_options = new List<Quantity>();
      for (int row = 0; row < lines.Count(); row++)
      {
        var actual_line = lines[row];
        row_options.Add((QuantityType.FixedInPixel, actual_line.TextBounds.Height));
      }

      var options = new GridLayoutOptions
      {
        Cols = 1,
        Rows = lines.Count(),
        RowOptions = row_options
      };

      var rects_enum = grid_layout.GetRects(options, abs_rect);
      var rects = rects_enum.ToArray();

      if (rects.Count() != lines.Count())
      {
        return; // discrepancy detected ... do nothing for now
      }

      for (var i = 0; i < lines.Count(); i++)
      {
        var l = lines[i];
        var r = rects[i]; // get a rect for each row/line

        // align the text within each row
        float left_offset = CalcHorizontalElementAlignmentOffset(
          r.rect.Left,
          r.rect.Right,
          l.TextBounds.Width
        );


        // The origin (0,0) is the bottom left corner of the text
        float top_offset = r.rect.Top + l.TextBounds.Height;
        _device.DrawText(
          l.Value,
          left_offset,
          top_offset
        );
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