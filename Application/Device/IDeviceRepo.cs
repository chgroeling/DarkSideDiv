using DarkSideDiv.Divs;

namespace Application.Device;


public interface IDeviceRepo
{
  IDsDivAlignedTextDevice DivTextDevice { get; }

  IDsDivDevice DivDevice { get; }
}