namespace DarkSideDiv.Common;

public interface IGridLayout
{
  void SetRowFixed(int row, float value);

  IEnumerable<(int col, int row, Rect rect)> GetRects(Rect draw_rect);

};
