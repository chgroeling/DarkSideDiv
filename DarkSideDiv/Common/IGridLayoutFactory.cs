
namespace DarkSideDiv.Common;


public interface IGridLayoutFactory
{
  IGridLayout Create(int cols, int rows);
}