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

    public DsDiv CreateTrackerCell()
    {
      var color = _palette_algo.GetColorByIdx(0);
      var attribs = new DsDivAttribs()
      {
        Border = 0f, // outer border
        Margin = 5f,
       // Padding = 5f,
        content_fill_color = color,
      };

      var div = new DsDiv(_device_repo.DivDevice, attribs);
      return div;

    }


    public DsDiv CreateContentDiv()
    {
      var base_grid = new DsDivComponentGrid((int)WEEKS, (int)DAYS);
      base_grid.SetDivSpacing(2f);

      for (int i = 0; i < WEEKS*DAYS; i++)
      {
        var cell = CreateTrackerCell();
        base_grid.Attach(i % ((int)WEEKS), i / (int)WEEKS, cell);
      }

      var margin = 50f;
      var attribs = new DsDivAttribs()
      {
        Margin = (
          (QuantityType.FixedInPixel, margin* WEEKS/DAYS),
          (QuantityType.FixedInPixel, margin),
          (QuantityType.FixedInPixel, margin* WEEKS/DAYS),
          (QuantityType.FixedInPixel, margin)
        ),
       // Border = 2f, 
        border_color = new ColorString("#ffffff"),
        content_fill_color = new ColorString("#ffffff"),
      };

      var div = new DsDiv(_device_repo.DivDevice, attribs);
      div.Append(base_grid);
      return div;
    }

    public DsDiv CreateAspectDiv()
    {
      var base_grid = new DsDivComponentGrid(1, 1);
      //base_grid.SetDivSpacing(2f);

      var attribs = new DsDivAttribs()
      {
        Border = 0f,
        Position = PositionType.Absolute, // place it absolute to new rect
        Height = HeightType.Zero,
        content_fill_color = new ColorString("#ffffff"),
        border_color = new ColorString("#ffffff"),
        Padding = (
          (QuantityType.FixedInPixel, 0.0f),  // left
          (QuantityType.FixedInPixel, 0.0f), // top
          (QuantityType.FixedInPixel, 0.0f),  // right
          (QuantityType.Percent, 100f / (WEEKS / DAYS)))   // bottom
      };

      var div = new DsDiv(_device_repo.DivDevice, attribs);
      var content_div = CreateContentDiv();
      base_grid.Attach(0, 0, content_div);
      div.Append(base_grid);

      return div;
    }


    public DsRoot Build()
    {
      var base_grid = new DsDivComponentGrid(1, 2);
      //base_grid.SetDivSpacing(2f);
      base_grid.SetRowFixedInPixel(0, 200f);
      var attribs = new DsDivAttribs()
      {
        Border = 2f, // outer border
        Position = PositionType.Relative,
        border_color = new ColorString("#00ff00"),
        content_fill_color = new ColorString("#ffffff"),
      };

      var div = new DsDiv(_device_repo.DivDevice, attribs);
      var content_div = CreateAspectDiv();
      base_grid.Attach(0, 1, content_div);
      div.Append(base_grid);

      var root_div = new DsRoot(ConversionFactories.ToRect(_pic_rect));
      root_div.Attach(div);
      return root_div;
    }


    DsPalette _palette_algo = new DsPalette();

    IDeviceRepo _device_repo;

    private SKRect _pic_rect;

    const float WEEKS = 53f;
    const float DAYS = 7f;

  }

}