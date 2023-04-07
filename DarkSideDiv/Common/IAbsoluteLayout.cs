using DarkSideDiv.Enums;

namespace DarkSideDiv.Common;

public interface IAbsoluteLayout {
  (float, float) GetOffset(Rect input_rect, Rect content_rect, DsAlignment alignment);
}