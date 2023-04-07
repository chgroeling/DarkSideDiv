using DarkSideDiv.Components;
using DarkSideDiv.Common;
using Moq;
using Xunit;
using DarkSideDiv.Enums;

namespace Test.Common
{

  public class DsDivComponentGridTest
  {
    [Fact]
    public void Draw_3x2Grid_CorrectCallToGetRects()
    {
      var grid_comp = new DsDivComponentGrid(3, 2);
      var mock = new Mock<IGridLayout>();

      grid_comp.GridLayout = mock.Object;

      var rect = new Rect(0f, 0f, 1000f, 1000f);
      grid_comp.Draw(rect);

      mock.Verify(call => call.GetRects(It.IsAny<GridLayoutSettings>(), rect));
    }

    [Fact]
    public void Draw_3x2GridPropFactorChangedOf1x0_CorrectDimensionsInGetRectsCall()
    {
      var grid_comp = new DsDivComponentGrid(3, 2);
      var mock = new Mock<IGridLayout>();

      grid_comp.SetColPropFactor(1, 100f);
      grid_comp.SetRowPropFactor(0, 100f);
      grid_comp.GridLayout = mock.Object;

      var rect = new Rect(0f, 0f, 1000f, 1000f);
      grid_comp.Draw(rect);

      mock.Verify(call => call.GetRects(It.Is<GridLayoutSettings>(
        i => (i.Cols == 3) && 
             (i.Rows == 2) 
      ), It.IsAny<Rect>()));
    }

        [Fact]
    public void Draw_3x2GridPropFactorChangedOf1x0_CorrectColAndRowOptionsInGetRectsCall()
    {
      var grid_comp = new DsDivComponentGrid(3, 2);
      var mock = new Mock<IGridLayout>();

      grid_comp.SetColPropFactor(1, 100f);
      grid_comp.SetRowPropFactor(0, 100f);
      grid_comp.GridLayout = mock.Object;

      var rect = new Rect(0f, 0f, 1000f, 1000f);
      grid_comp.Draw(rect);

      var expected_ColOptions = new List<Quantity>() {
          (QuantityType.Weight, 1f),
          (QuantityType.Weight, 100f),
          (QuantityType.Weight, 1f)
        };

      var expected_RowOptions = new List<Quantity>() {
          (QuantityType.Weight, 100f),
          (QuantityType.Weight, 1f),
        };

      mock.Verify(call => call.GetRects(It.Is<GridLayoutSettings>(
        i => (Enumerable.SequenceEqual(expected_ColOptions, i.ColOptions)) && 
             (Enumerable.SequenceEqual(expected_RowOptions, i.RowOptions)) 
      ), It.IsAny<Rect>()));
    }
  }
}