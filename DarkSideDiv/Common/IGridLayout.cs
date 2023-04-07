namespace DarkSideDiv.Common;

public interface IGridLayout
{
  IEnumerable<(int col, int row, Rect rect)> GetRects(GridLayoutSettings settings, Rect draw_rect);

};
