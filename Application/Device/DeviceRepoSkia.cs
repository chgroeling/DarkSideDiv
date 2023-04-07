using SkiaSharp;
using DarkSideDiv.Divs;
using DarkSideDiv.Components;

namespace Application.Device;


public class DeviceRepoSkia : IDeviceRepo
{

  public IDsDivComponentAlignedTextDevice DivTextDevice
  {
    get
    {
      if (TextDeviceSkia == null)
      {
        throw new Exception("Text Device not initialized");
      }
      return TextDeviceSkia;
    }
  }

  public IDsDivDevice DivDevice
  {
    get
    {
      if (DivDeviceSkia == null)
      {
        throw new Exception("Div Device not initialized");
      }
      return DivDeviceSkia;
    }
  }

  public DsDivComponentAlignedTextSkia? TextDeviceSkia { get; set; }

  public DsDivSkia? DivDeviceSkia { get; set; }

  public void SetCanvas(SKCanvas canvas)
  {
    if (TextDeviceSkia != null)
    {
      TextDeviceSkia.SetCanvas(canvas);
    }

    if (DivDeviceSkia != null)
    {
      DivDeviceSkia.SetCanvas(canvas);
    }
  }

}