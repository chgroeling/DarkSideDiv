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

    List<Line> SplitString(string str, float rect_width)
    {
      var new_lines = new List<Line>();
      var list = str.Split(' ');

      Line? last_item = null;
      string accu = "";

      for (int j = 0; j < list.Count(); j++)
      {
        var element = list[j];
        accu += element;

        var textBounds = _device.MeasureText(accu);
        var item = new Line()
        {
          Value = accu,
          TextBounds = textBounds
        };

        if (textBounds.Width > rect_width)
        {
          if (last_item == null)
          {
            last_item = item;
            break;
          }
          new_lines.Add(last_item);
          accu = element;

          textBounds = _device.MeasureText(element);

          last_item = new Line()
          {
            Value = accu,
            TextBounds = textBounds
          };
          accu += " ";
        }
        else
        {
          last_item = item;
          accu += " ";
        }
      }

      if (last_item == null)
      {
        throw new Exception("Last item was null");
      }
      new_lines.Add(last_item);

      return new_lines;
    }

    public void Draw(Rect parent_content, Rect nearest_positioned_ancestor)
    {
      var font_metrics = _device.Setup(_attribs);
      var line_height = -font_metrics.Ascent + font_metrics.Descent + font_metrics.Leading;

      var text = _attribs.Text;

      var lines = SplitLines(text, _device);

      var new_lines = new List<Line>();
      for (int i = 0; i < lines.Count(); i++)
      {
        var line_width = lines[i].TextBounds.Width;
        var rect_width = parent_content.Width;
        if (line_width > rect_width)
        {
          var splitted_lines = SplitString(lines[i].Value, rect_width);
          new_lines.AddRange(splitted_lines);
        }
        else
        {
          new_lines.Add(lines[i]);
        }
      }
      lines = new_lines.ToArray();

      var max_width_of_lines = (from i in lines select i.TextBounds.Width).Max();
      var accumulated_height_of_lines = (
        from i in lines select i.TextBounds.Height).Aggregate(0f, (bef, next) => { return bef + next; }
      );

      // This returns the rectangle of the text
      var combined_rect = new Rect(
        lines[0].TextBounds.Left,
        font_metrics.Ascent,
        max_width_of_lines,
        font_metrics.Ascent + line_height * lines.Count());

      // Skia coordinate system
      // ----> x
      // |
      // | y
      // V
      var abs_rect = AbsoluteLayoutAlgorithmn.GetAbsRect(parent_content, combined_rect, _attribs.Alignment, 0f, 0f);


      var row_options = new List<Quantity>();
      for (int row = 0; row < lines.Count(); row++)
      {
        var actual_line = lines[row];
        row_options.Add((QuantityType.FixedInPixel, line_height));
      }

      var options = new GridLayoutOptions
      {
        Cols = 1,
        Rows = lines.Count(),
        RowOptions = row_options
      };

      var rects_enum = GridLayoutAlgorithmn.GetRects(options, abs_rect);
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
        float top_offset = r.rect.Top + line_height - font_metrics.Descent - font_metrics.Leading;
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

      switch (_attribs.Alignment)
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