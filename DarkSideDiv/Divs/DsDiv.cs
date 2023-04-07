using DarkSideDiv.Common;
using DarkSideDiv.Components;
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
      _components = new List<IDsDivComponent>();
    }

    IDsDivDevice _device;

    public void Append(IDsDivComponent component)
    {
      _components.Add(component);
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
    
    public void Draw(Rect parent_rect, Rect root_rect)
    {
      _device.Setup(_div_attribs);
      //_device.SetCanvas(canvas);

      var draw_rect = parent_rect;


      if (_div_attribs.Position == Enums.PositionType.Absolute)
      {
        draw_rect = root_rect;
      }

      if (_div_attribs.Height == Enums.HeightType.Zero)
      {
        var height_offset = GetDistanceInPixel(_div_attribs.Padding.distance_from_bottom, draw_rect.Width);
        height_offset += _div_attribs.Border.distance_from_bottom.Value;
        draw_rect = new Rect(draw_rect.Left, draw_rect.Top, draw_rect.Right, draw_rect.Top + height_offset);
      }

      // BORDER
      var border_rect = _dim_algo.CalculateBorderRect(
        draw_rect,
        _div_attribs.Margin
      );

      _device.DrawBorderRect(border_rect);

      var padding_rect = _dim_algo.CalculatePaddingRect(
        draw_rect,
        _div_attribs.Margin,
        _div_attribs.Border
      );

      _device.DrawContentRect(padding_rect);

      // CONTENT
      var content_rec = _dim_algo.CalculateContentRect(
        draw_rect,
        _div_attribs.Margin,
        _div_attribs.Border,
        _div_attribs.Padding
      );

      var new_parent_rect = content_rec;
      if (_div_attribs.Position == Enums.PositionType.Absolute)
      {
        new_parent_rect = draw_rect;
      }

      var new_root_rect = root_rect;
      if (_div_attribs.Position == Enums.PositionType.Relative)
      {
        new_root_rect = padding_rect;
      }
      foreach (var i in _components)
      {
        i.Draw(new_parent_rect, new_root_rect);
      }
    }

    private DsDivAttribs _div_attribs;

    private RectDimensions _dim_algo = new RectDimensions();

    private List<IDsDivComponent> _components;
  }
}