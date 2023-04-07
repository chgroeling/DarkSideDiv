namespace DarkSideDiv.Common
{
  public interface IGridLayoutAlgorithmn
  {
    IEnumerable<(int col, int row, Rect rect)> GetRects(GridLayoutOptions options, Rect draw_rect);
  }
}