using DarkSideDiv.Common;
using Moq;
using Xunit;
using DarkSideDiv.Enums;
using DarkSideDiv.Divs;

namespace Test.Common;


public class DsDivGridTest
{

  public Mock<IDsDiv>[,] SetupIDsDivMocks(int cols, int rows, DsDivGrid dut)
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
    var grid_comp = new DsDivGrid(3, 2);
    var mocks = SetupIDsDivMocks(3, 2, grid_comp);

    // Act
    var rect = new Rect(0f, 0f, 1000f, 1000f);
    grid_comp.Draw(rect,rect);

    // Assert
    for (int col = 0; col < 3; col++)
    {
      for (int row = 0; row < 2; row++)
      {
        var mock = mocks[col, row];
        mock.Verify(call => call.Draw(It.IsAny<Rect>(),It.IsAny<Rect>()));
      }
    }
  }

  [Fact]
  public void Draw_2x2Grid_CorrectDrawCalls()
  {
    var grid_comp = new DsDivGrid(2, 2);
    var mocks = SetupIDsDivMocks(2, 2, grid_comp);

    var rect = new Rect(0f, 0f, 1000f, 1000f);
    grid_comp.Draw(rect, rect);

    // Row 0
    mocks[0, 0].Verify(call => call.Draw(new Rect(0f, 0f, 500f, 500f), It.IsAny<Rect>()));
    mocks[1, 0].Verify(call => call.Draw(new Rect(500f, 0f, 1000f, 500f),It.IsAny<Rect>()));

    // Row 1
    mocks[0, 1].Verify(call => call.Draw(new Rect(0f, 500f, 500f, 1000f), It.IsAny<Rect>()));
    mocks[1, 1].Verify(call => call.Draw(new Rect(500f, 500f, 1000f, 1000f),It.IsAny<Rect>()));
  }

  [Fact]
  public void Draw_2x2GridWithDivSpacing_CorrectDrawCalls()
  {
    var grid_comp = new DsDivGrid(2, 2);
    grid_comp.SetDivSpacing(100f);
    var mocks = SetupIDsDivMocks(2, 2, grid_comp);

    var rect = new Rect(0f, 0f, 1000f, 1000f);
    grid_comp.Draw(rect,rect);

    // Row 0
    mocks[0, 0].Verify(call => call.Draw(new Rect(0f, 0f, 450f, 450f),It.IsAny<Rect>()));
    mocks[1, 0].Verify(call => call.Draw(new Rect(550f, 0f, 1000f, 450f),It.IsAny<Rect>()));

    // Row 1
    mocks[0, 1].Verify(call => call.Draw(new Rect(0f, 550f, 450f, 1000f),It.IsAny<Rect>()));
    mocks[1, 1].Verify(call => call.Draw(new Rect(550f, 550f, 1000f, 1000f),It.IsAny<Rect>()));
  }

  [Fact]
  public void Draw_3x2GridPropFactorChangedCol1_CorrectDrawCalls()
  {
    var grid_comp = new DsDivGrid(3, 2);
    var mocks = SetupIDsDivMocks(3, 2, grid_comp);

    grid_comp.SetColPropFactor(1, 2f);


    var rect = new Rect(0f, 0f, 1000f, 1000f);
    grid_comp.Draw(rect,rect);

    // Row 0
    mocks[0, 0].Verify(call => call.Draw(new Rect(0f, 0f, 250f, 500f),It.IsAny<Rect>()));
    mocks[1, 0].Verify(call => call.Draw(new Rect(250f, 0f, 750f, 500f),It.IsAny<Rect>()));
    mocks[2, 0].Verify(call => call.Draw(new Rect(750f, 0f, 1000f, 500f),It.IsAny<Rect>()));

    // Row 1
    mocks[0, 1].Verify(call => call.Draw(new Rect(0f, 500f, 250f, 1000f),It.IsAny<Rect>()));
    mocks[1, 1].Verify(call => call.Draw(new Rect(250f, 500f, 750f, 1000f),It.IsAny<Rect>()));
    mocks[2, 1].Verify(call => call.Draw(new Rect(750f, 500f, 1000f, 1000f),It.IsAny<Rect>()));
  }

  [Fact]
  public void Draw_2x3ridPropFactorChangedRow1_CorrectDrawCalls()
  {
    var grid_comp = new DsDivGrid(2, 3);
    var mocks = SetupIDsDivMocks(2, 3, grid_comp);

    grid_comp.SetRowPropFactor(1, 2f);

    var rect = new Rect(0f, 0f, 1000f, 1000f);
    grid_comp.Draw(rect, rect);

    // Col 0
    mocks[0, 0].Verify(call => call.Draw(new Rect(0f, 0f, 500f, 250f),It.IsAny<Rect>()));
    mocks[0, 1].Verify(call => call.Draw(new Rect(0f, 250f, 500f, 750f),It.IsAny<Rect>()));
    mocks[0, 2].Verify(call => call.Draw(new Rect(0f, 750f, 500f, 1000f),It.IsAny<Rect>()));

    // Col 1
    mocks[1, 0].Verify(call => call.Draw(new Rect(500f, 0f, 1000f, 250f),It.IsAny<Rect>()));
    mocks[1, 1].Verify(call => call.Draw(new Rect(500f, 250f, 1000f, 750f),It.IsAny<Rect>()));
    mocks[1, 2].Verify(call => call.Draw(new Rect(500f, 750f, 1000f, 1000f),It.IsAny<Rect>()));
  }


  [Fact]
  public void Draw_2x3GridFixedFactorChangedRow1_CorrectDrawCalls()
  {
    var grid_comp = new DsDivGrid(2, 3);
    var mocks = SetupIDsDivMocks(2, 3, grid_comp);

    grid_comp.SetRowFixedInPixel(1, 200f);

    var rect = new Rect(0f, 0f, 1000f, 1000f);
    grid_comp.Draw(rect, rect);

    // Col 0
    mocks[0, 0].Verify(call => call.Draw(new Rect(0f, 0f, 500f, 400f),It.IsAny<Rect>()));
    mocks[0, 1].Verify(call => call.Draw(new Rect(0f, 400f, 500f, 600f),It.IsAny<Rect>()));
    mocks[0, 2].Verify(call => call.Draw(new Rect(0f, 600f, 500f, 1000f),It.IsAny<Rect>()));

    // Col 1
    mocks[1, 0].Verify(call => call.Draw(new Rect(500f, 0f, 1000f, 400f),It.IsAny<Rect>()));
    mocks[1, 1].Verify(call => call.Draw(new Rect(500f, 400f, 1000f, 600f),It.IsAny<Rect>()));
    mocks[1, 2].Verify(call => call.Draw(new Rect(500f, 600f, 1000f, 1000f),It.IsAny<Rect>()));
  }

   [Fact]
  public void Draw_3x2GridFixedInPixelCol1_CorrectDrawCalls()
  {
    var grid_comp = new DsDivGrid(3, 2);
    var mocks = SetupIDsDivMocks(3, 2, grid_comp);

    grid_comp.SetColFixedInPixel(1, 200f);


    var rect = new Rect(0f, 0f, 1000f, 1000f);
    grid_comp.Draw(rect,rect);

    // Row 0
    mocks[0, 0].Verify(call => call.Draw(new Rect(0f, 0f, 400f, 500f),It.IsAny<Rect>()));
    mocks[1, 0].Verify(call => call.Draw(new Rect(400f, 0f, 600f, 500f), It.IsAny<Rect>()));
    mocks[2, 0].Verify(call => call.Draw(new Rect(600f, 0f, 1000f, 500f),It.IsAny<Rect>()));

    // Row 1
    mocks[0, 1].Verify(call => call.Draw(new Rect(0f, 500f, 400f, 1000f),It.IsAny<Rect>()));
    mocks[1, 1].Verify(call => call.Draw(new Rect(400f, 500f, 600f, 1000f),It.IsAny<Rect>()));
    mocks[2, 1].Verify(call => call.Draw(new Rect(600f, 500f, 1000f, 1000f),It.IsAny<Rect>()));
  }
}