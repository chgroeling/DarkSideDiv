using SkiaSharp;
using System;
using System.Collections;
using System.Collections.Generic;

namespace DarkSideDiv {

public class DsRectDimensions
{
  public SKRect CalculateBorderRect(SKRect outer_rect, float margin)
  {
    outer_rect.Inflate(-margin, -margin);
    return outer_rect;
  }
  public SKRect CalculatePaddingRect(SKRect outer_rect, float margin, float border)
  {
    return new SKRect();
  }
  public SKRect CalculateContentRect(SKRect outer_rect, float margin, float border, float padding)
  {
    var deflate_len = margin + border + padding;
    outer_rect.Inflate(-deflate_len, -deflate_len);
    return outer_rect;
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

public interface IDsDiv {
  void Draw(SKCanvas canvas, SKRect draw_rect);
}

public interface IDsDivComponent {
  void Draw(SKCanvas canvas, SKRect draw_rect);
}

public enum DsAlignment
{
    Left,
    Right,
    Top,
    Bottom,
    TopLeft,
    BottomLeft,
    TopRight,
    BottomRight
}

public struct DsDivAlignedTextComponentAttribs {

  public static DsDivAlignedTextComponentAttribs Default() {
    var attribs = new DsDivAlignedTextComponentAttribs() {
      alignment = DsAlignment.TopLeft,
      text = "",
      text_size = 20
    };
    return attribs;
  }
  public string text;

  public DsAlignment alignment;
  public float text_size;
};

public class DsDivAlignedTextComponent : IDsDivComponent {
  
  public DsDivAlignedTextComponent() : this(DsDivAlignedTextComponentAttribs.Default()) {
  }

  public DsDivAlignedTextComponent(DsDivAlignedTextComponentAttribs attribs) {
    _attribs = attribs;
  }

  public void Draw(SKCanvas canvas, SKRect draw_rect) {
 
    var textPaint = new SKPaint() // with object initializer
      {
        IsAntialias = true,
        TextAlign = SKTextAlign.Left,
        Color = SKColors.Black,
        TextSize = _attribs.text_size,
        //Typeface = SKTypeface.FromFamilyName("monospace")
      };
    var text = _attribs.text;

    // Get the bounds of the text
    SKRect textBounds = new SKRect();
    textPaint.MeasureText(text, ref textBounds);

    var x = draw_rect.Left;
    var y = draw_rect.Bottom;

    switch(_attribs.alignment) {
      case DsAlignment.Left:
        x = draw_rect.Left;
        y = draw_rect.Bottom + (draw_rect.Top- draw_rect.Bottom)*0.5f-textBounds.Top*0.5f;
      break;

      case DsAlignment.TopLeft:
        x = draw_rect.Left;
        y = draw_rect.Top - textBounds.Top;
      break;

      case DsAlignment.Right:
        x = draw_rect.Right - textBounds.Width;
        y =draw_rect.Bottom + (draw_rect.Top- draw_rect.Bottom)*0.5f -textBounds.Top*0.5f;
      break;

      case DsAlignment.TopRight:
        x = draw_rect.Right - textBounds.Width;
        y = draw_rect.Top - textBounds.Top;
      break;

      case DsAlignment.BottomRight:
        x = draw_rect.Right - textBounds.Width;
        y = draw_rect.Bottom;
      break;

      case DsAlignment.Bottom:
        x = draw_rect.Left + (draw_rect.Right - draw_rect.Left)*0.5f - textBounds.Width * 0.5f;
        y = draw_rect.Bottom;
      break;

      case DsAlignment.Top:
        x = draw_rect.Left + (draw_rect.Right - draw_rect.Left)*0.5f - textBounds.Width * 0.5f;
        y = draw_rect.Top - textBounds.Top;
      break;

      case DsAlignment.BottomLeft:
      default:
      // do nothing
      break;
    }

    //var meas = -textBounds.Top;
    //SKPaint paint_border = new SKPaint();
    //canvas.DrawRect(x,y-meas, 100f, meas, paint_border);

    //canvas.Save();
    // canvas.Translate(draw_rect.Left,
    //  draw_rect.Bottom);
    //canvas.Scale(0.5f,0.5f);
    canvas.DrawText(text,
      x,
      y,
      textPaint
    );
    //canvas.Restore();
  }

  private DsDivAlignedTextComponentAttribs _attribs;
}

public class DsDiv : IDsDiv
{

  public DsDiv()
  {
    //default attributes
    _div_attribs = new DsDivAttribs(){
      border = 10,
      border_color = SKColor.Parse("000000"),
      content_fill_color = SKColor.Parse("DAE8FC")
    };
    _components = new List<IDsDivComponent>();
  }

  public DsDiv(DsDivAttribs div_attribs) {
    _div_attribs = div_attribs;
    _components = new List<IDsDivComponent>();
  }

  public void Append(IDsDivComponent component) {
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
    foreach(var i in _components) {
      i.Draw(canvas, content_rec);
    }
  }

  private DsDivAttribs _div_attribs;

  private DsRectDimensions dim_algo =  new DsRectDimensions();

  private List<IDsDivComponent> _components;


}
}