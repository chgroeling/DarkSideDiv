namespace DarkSideDiv.Common
{
  class GridLayoutFactory : IGridLayoutFactory
  {
    public IGridLayout Create(int cols, int rows)
    {
      return new GridLayout(cols, rows);
    }
  };
};