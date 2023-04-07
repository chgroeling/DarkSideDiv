using SkiaSharp;
using System;
using System.Collections;
using System.Collections.Generic;

namespace DarkSideDiv
{


  public interface IDsDiv
  {
    void Draw(SKCanvas canvas, SKRect draw_rect);
  }

  public interface IDsDivComponent
  {
    void Draw(SKCanvas canvas, SKRect draw_rect);
  }

  public struct DsDivAttribs
  {
    public SKColor border_color;

    public SKColor content_fill_color;


    public DsDivRectDistance Border
    {
      get;
      set;    
    }

    public DsDivRectDistance Margin
    {
      get;
      set;
    }
    public DsDivRectDistance Padding
    {
      get;
      set;
    }
  }

  public class DsDiv : IDsDiv
  {

    public DsDiv() : this(new DsDivAttribs())
    {
    }

    public DsDiv(DsDivAttribs div_attribs)
    {
      _div_attribs = div_attribs;
      _components = new List<IDsDivComponent>();
    }

    public void Append(IDsDivComponent component)
    {
      _components.Add(component);
    }

    public void Draw(SKCanvas canvas, SKRect draw_rect)
    {
      // BORDER
      var border_rect = _dim_algo.CalculateBorderRect(
        draw_rect,
        _div_attribs.Margin
      );

      SKPaint paint_border = new SKPaint();
      paint_border.Color = _div_attribs.border_color;
      paint_border.IsAntialias = true;
      canvas.DrawRect(border_rect, paint_border);

      // CONTENT
      var content_rec = _dim_algo.CalculateContentRect(
        draw_rect,
        _div_attribs.Margin,
        _div_attribs.Border,
        _div_attribs.Padding
      );

      SKPaint paint_content = new SKPaint() { Color = _div_attribs.content_fill_color, IsAntialias = true };
      canvas.DrawRect(content_rec, paint_content);
      foreach (var i in _components)
      {
        i.Draw(canvas, content_rec);
      }
    }

    private DsDivAttribs _div_attribs;

    private DsRectDimensions _dim_algo = new DsRectDimensions();

    private List<IDsDivComponent> _components;
  }
}