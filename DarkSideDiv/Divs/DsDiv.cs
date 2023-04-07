using DarkSideDiv.Common;
using DarkSideDiv.Enums;

namespace DarkSideDiv.Divs
{
  public class DsDiv : IDsDiv
  {

    public DsDiv(IDsDivDevice device) : this(device, new DsDivAttribs())
    {
    }

    public DsDiv(IDsDivDevice device, DsDivAttribs div_attribs)
    {
      _device = device;
      _div_attribs = div_attribs;
      _div_stack = new List<IDsDiv>();
    }

    IDsDivDevice _device;

    public void Append(IDsDiv div)
    {
      _div_stack.Add(div);
    }

    public float GetDistanceInPixel(Quantity quantity, float width)
    {
      var re = 0f;
      if (quantity.QType == QuantityType.Percent)
      {
        re = width * quantity.Value * 0.01f;
      }
      else
      {
        re = quantity.Value;
      }
      return re;
    }

    public void Draw(Rect parent_content, Rect nearest_positioned_ancestor)
    {
      _device.Setup(_div_attribs);
      //_device.SetCanvas(canvas);

      var margin = parent_content;


      if (_div_attribs.Position == Enums.PositionType.Absolute)
      {
        margin = nearest_positioned_ancestor;
      }

      if (_div_attribs.Height == Enums.HeightType.Zero)
      {
        var height_offset = GetDistanceInPixel(_div_attribs.Padding.distance_from_bottom, margin.Width);
        height_offset += _div_attribs.Border.distance_from_bottom.Value;
        margin = new Rect(margin.Left, margin.Top, margin.Right, margin.Top + height_offset);
      }

      if (_div_attribs.BorderColor != null)
      {
        // BORDER
        var border = _dim_algo.CalculateBorderRect(
          margin,
          _div_attribs.Margin
        );

        _device.DrawBorderRect(border);
      }

      var padding = _dim_algo.CalculatePaddingRect(
        margin,
        _div_attribs.Margin,
        _div_attribs.Border
      );

      if (_div_attribs.ContentFillColor != null)
      {
        _device.DrawContentRect(padding);
      }

      // CONTENT
      var content = _dim_algo.CalculateContentRect(
        margin,
        _div_attribs.Margin,
        _div_attribs.Border,
        _div_attribs.Padding
      );

      var new_parent_content = content;
      if (_div_attribs.Position == Enums.PositionType.Absolute)
      {
        new_parent_content = margin;
      }

      var new_nearest_positioned_ancestor = nearest_positioned_ancestor;
      if (_div_attribs.Position == Enums.PositionType.Relative)
      {
        new_nearest_positioned_ancestor = padding; // absolute positioning starts from padding rect
      }

      
      foreach (var element in _div_stack)
      {
        element.Draw(new_parent_content, new_nearest_positioned_ancestor);
      }
    }

    private DsDivAttribs _div_attribs;

    private RectDimensions _dim_algo = new RectDimensions();

    private List<IDsDiv> _div_stack;
  }
}