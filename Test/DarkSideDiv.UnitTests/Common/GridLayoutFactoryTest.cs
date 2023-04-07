using DarkSideDiv.Common;
using Xunit;


namespace Test.Common
{
  public class GridLayoutFactoryTest
  {
    [Fact]
    public void CreateGridLayout_3Cols3Rows_CorrectObject()
    {
        var factory = new GridLayoutFactory();
        var obj = factory.Create(2,2);

        Assert.IsType<GridLayout>(obj);
        Assert.IsAssignableFrom<IGridLayout>(obj);
    }
  }
}
