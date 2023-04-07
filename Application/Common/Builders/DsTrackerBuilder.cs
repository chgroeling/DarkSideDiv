using SkiaSharp;

using DarkSideDiv.Enums;
using DarkSideDiv.Components;
using DarkSideDiv.Common;
using DarkSideDiv.Divs;
using Application.Device;
using Application.Common;

namespace Application.Builders
{
  public class DsTrackerBuilder
  {

    public DsTrackerBuilder(IDeviceRepo device_repo, SKRect pic_rect)
    {
      _device_repo = device_repo;
      _pic_rect = pic_rect;
    }

    public DsDiv CreateTrackerCell() {
        var attribs = new DsDivAttribs()
      {
        Border = 0f, // outer border
        content_fill_color = new ColorString("#ffffff"),
      };

       var div = new DsDiv(_device_repo.DivDevice, attribs);
       return div;

    }
    public DsRoot Build()
    {
      var base_grid = new DsDivComponentGrid(30, 8);
      base_grid.SetDivSpacing(2f);

      for (int i = 0; i < 30*8; i++)
      {
        var cell = CreateTrackerCell();
        base_grid.Attach(i % 30, i / 30, cell);
      }

      var attribs = new DsDivAttribs()
      {
        Border = 2f, // outer border
        border_color = new ColorString("#ffff00"),
        content_fill_color = new ColorString("#ff0000"),
      };

      var div = new DsDiv(_device_repo.DivDevice, attribs);
      div.Append(base_grid);

      var root_div = new DsRoot(ConversionFactories.ToRect(_pic_rect));
      root_div.Attach(div);
      return root_div;
    }

 
    DsPalette _palette_algo = new DsPalette();

    IDeviceRepo _device_repo;

     private SKRect _pic_rect;

  }

}