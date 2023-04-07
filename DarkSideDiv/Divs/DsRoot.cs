using SkiaSharp;
using System;
using DarkSideDiv.Common;

namespace DarkSideDiv.Divs
{
  public class DsRoot
  {
    public DsRoot(Rect root_rect)
    {
      _root_rect = root_rect;
    }

    public void Attach(IDsDiv dsdiv)
    {
      _root_div = dsdiv;
    }

    public void Draw(SKCanvas canvas)
    {
      if (_root_div is null)
      {
        throw new ArgumentNullException(nameof(_root_div));
      }

      Draw(canvas, _root_div);
    }
    private void Draw(SKCanvas canvas, IDsDiv parent)
    {
      parent.Draw(canvas, _root_rect);
    }

    private Rect _root_rect;
    private IDsDiv? _root_div;
  }
}