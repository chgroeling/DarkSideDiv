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

  public class DsRectDimensions
  {
    public SKRect Shrink(SKRect rect, float value) {
        var ret = new SKRect(rect.Left + value, rect.Top + value, rect.Right - value, rect.Bottom - value);
        return ret;
    }
    public SKRect CalculateBorderRect(SKRect outer_rect, float margin)
    {
      return Shrink(outer_rect, margin);
    }
    public SKRect CalculatePaddingRect(SKRect outer_rect, float margin, float border)
    {
      return new SKRect();
    }
    public SKRect CalculateContentRect(SKRect outer_rect, float margin, float border, float padding)
    {
      var deflate_len = margin + border + padding;
      return Shrink(outer_rect, deflate_len);
    }
  }

  public struct DsDivAttribs
  {
    public float margin;
    public float border;

    public float padding;

    public SKColor border_color;

    public SKColor content_fill_color;
  }


  public class DsDiv : IDsDiv
  {

    public DsDiv()
    {
      //default attributes
      _div_attribs = new DsDivAttribs()
      {
        border = 10,
        border_color = SKColor.Parse("000000"),
        content_fill_color = SKColor.Parse("DAE8FC")
      };
      _components = new List<IDsDivComponent>();
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
      var border_rect = dim_algo.CalculateBorderRect(
        draw_rect,
        _div_attribs.margin
      );

      SKPaint paint_border = new SKPaint();
      paint_border.Color = _div_attribs.border_color;
      paint_border.IsAntialias = true;
      canvas.DrawRect(border_rect, paint_border);

      // CONTENT
      var content_rec = dim_algo.CalculateContentRect(
        draw_rect,
        _div_attribs.margin,
        _div_attribs.border,
        _div_attribs.padding
      );

      SKPaint paint_content = new SKPaint() { Color = _div_attribs.content_fill_color, IsAntialias = true };
      canvas.DrawRect(content_rec, paint_content);
      foreach (var i in _components)
      {
        i.Draw(canvas, content_rec);
      }
    }

    private DsDivAttribs _div_attribs;

    private DsRectDimensions dim_algo = new DsRectDimensions();

    private List<IDsDivComponent> _components;
  }
}