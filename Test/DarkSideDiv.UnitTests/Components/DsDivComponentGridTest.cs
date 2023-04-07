using DarkSideDiv.Components;
using DarkSideDiv.Common;
using Moq;
using Xunit;
using DarkSideDiv.Enums;
using DarkSideDiv.Divs;

namespace Test.Common
{

  public class DsDivComponentGridTest
  {

    public Mock<IDsDiv>[,] SetupIDsDivMocks(int cols, int rows, DsDivComponentGrid dut)
    {
      Mock<IDsDiv>[,] mocks = new Mock<IDsDiv>[cols, rows];
      for (int col = 0; col < cols; col++)
      {
        for (int row = 0; row < rows; row++)
        {
          var mock_div = new Mock<IDsDiv>();
          dut.Attach(col, row, mock_div.Object);
          mocks[col, row] = mock_div;
        }
      }
      return mocks;
    }

    [Fact]
    public void Draw_3x2GridPropFactorChangedOf1x0_CorrectDimensionsInGetRectsCall()
    {
      // Arrange
      var grid_comp = new DsDivComponentGrid(3, 2);
      var mocks = SetupIDsDivMocks(3, 2, grid_comp);

      // Act
      var rect = new Rect(0f, 0f, 1000f, 1000f);
      grid_comp.Draw(rect);

      // Assert
      for (int col = 0; col < 3; col++)
      {
        for (int row = 0; row < 2; row++)
        {
          var mock = mocks[col, row];
          mock.Verify(call => call.Draw(It.IsAny<Rect>()));
        }
      }
    }

    [Fact]
    public void Draw_3x2GridPropFactorChangedCol1_CorrectDrawCalls()
    {
      var grid_comp = new DsDivComponentGrid(3, 2);
      var mocks = SetupIDsDivMocks(3, 2, grid_comp);

      grid_comp.SetColPropFactor(1, 2f);


      var rect = new Rect(0f, 0f, 1000f, 1000f);
      grid_comp.Draw(rect);

      // Row 0
      mocks[0, 0].Verify(call => call.Draw(new Rect(0f, 0f, 250f, 500f)));
      mocks[1, 0].Verify(call => call.Draw(new Rect(250f, 0f, 750f, 500f)));
      mocks[2, 0].Verify(call => call.Draw(new Rect(750f, 0f, 1000f, 500f)));

      // Row 1
      mocks[0, 1].Verify(call => call.Draw(new Rect(0f, 500f, 250f, 1000f)));
      mocks[1, 1].Verify(call => call.Draw(new Rect(250f, 500f, 750f, 1000f)));
      mocks[2, 1].Verify(call => call.Draw(new Rect(750f, 500f, 1000f, 1000f)));
    }

    [Fact]
    public void Draw_2x3ridPropFactorChangedRow1_CorrectDrawCalls()
    {
      var grid_comp = new DsDivComponentGrid(2, 3);
      var mocks = SetupIDsDivMocks(2, 3, grid_comp);

      grid_comp.SetRowPropFactor(1, 2f);

      var rect = new Rect(0f, 0f, 1000f, 1000f);
      grid_comp.Draw(rect);

      // Col 0
      mocks[0, 0].Verify(call => call.Draw(new Rect(0f, 0f, 500f, 250f)));
      mocks[0, 1].Verify(call => call.Draw(new Rect(0f, 250f, 500f, 750f)));
      mocks[0, 2].Verify(call => call.Draw(new Rect(0f, 750f, 500f, 1000f)));

      // Col 1
      mocks[1, 0].Verify(call => call.Draw(new Rect(500f, 0f, 1000f, 250f)));
      mocks[1, 1].Verify(call => call.Draw(new Rect(500f, 250f, 1000f, 750f)));
      mocks[1, 2].Verify(call => call.Draw(new Rect(500f, 750f, 1000f, 1000f)));
    }
  }
}