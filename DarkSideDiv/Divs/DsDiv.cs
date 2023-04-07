using DarkSideDiv.Common;
using DarkSideDiv.Components;

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

    public void Draw(Rect draw_rect)
    {
      _device.Setup(_div_attribs);
      //_device.SetCanvas(canvas);

      // BORDER
      var border_rect = _dim_algo.CalculateBorderRect(
        draw_rect,
        _div_attribs.Margin
      );

      _device.DrawBorderRect(border_rect);

      var inner_rect = _dim_algo.CalculatePaddingRect(
        draw_rect,
        _div_attribs.Margin,
        _div_attribs.Border
      );

    _device.DrawContentRect(inner_rect);

      // CONTENT
      var content_rec = _dim_algo.CalculateContentRect(
        draw_rect,
        _div_attribs.Margin,
        _div_attribs.Border,
        _div_attribs.Padding
      );

  

      foreach (var i in _components)
      {
        i.Draw(content_rec);
      }
    }

    private DsDivAttribs _div_attribs;

    private RectDimensions _dim_algo = new RectDimensions();

    private List<IDsDivComponent> _components;
  }
}