using DarkSideDiv.Enums;

namespace DarkSideDiv.Common;

public interface IAbsoluteLayout {
  Rect GetAbsRect(Rect input_rect, Rect content_rect, DsAlignment alignment, float x_offs, float y_offs);
}