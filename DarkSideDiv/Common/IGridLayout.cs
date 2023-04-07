namespace DarkSideDiv.Common;

public interface IGridLayout
{
  IEnumerable<(int col, int row, Rect rect)> GetRects(GridLayoutOptions options, Rect draw_rect);

};
