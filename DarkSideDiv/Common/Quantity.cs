using DarkSideDiv.Enums;

namespace DarkSideDiv.Common;

public struct Quantity
{
  public QuantityType QType { get; set; }
  public float Value { get; set; }

  public static implicit operator Quantity((QuantityType qtype, float value) args)
  {
    var ret = new Quantity()
    {
      QType = args.qtype,
      Value = args.value
    };
    return ret;
  }
}
