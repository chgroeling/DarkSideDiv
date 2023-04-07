using DarkSideDiv.Divs;
using DarkSideDiv.Components;

namespace Application.Device;


public interface IDeviceRepo
{
  IDsDivComponentAlignedTextDevice DivTextDevice { get; }

  IDsDivDevice DivDevice { get; }
}