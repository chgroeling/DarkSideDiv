using DarkSideDiv.Components;
using Xunit;
using SkiaSharp;
using System.Collections;

namespace Test.Common
{


  public class GridLayoutTest
  {
    [Fact]
    public void TestGridLayout_GetRects1c1r_Return1Rect()
    {
      // Arrange
      var grid_layout = new GridLayout(1,1);
 
      var inp_rect = new SKRect(0f, 0f, 1000f, 1000f);


      // Act
      var it = grid_layout.GetRects(inp_rect);

      // Assert
      Assert.Equal(new List<(int, int, SKRect)>() {
        (0, 0, new SKRect(0f, 0f, 1000f, 1000f))
      }, it);
    }

    [Fact]
    public void TestGridLayout_GetRects1c2r_Return2Rects()
    {
      // Arrange
      var grid_layout = new GridLayout(1,2);

      var inp_rect = new SKRect(0f, 0f, 1000f, 1000f);
      

      // Act
      var it = grid_layout.GetRects(inp_rect);

      // Assert
      Assert.Equal(new List<(int, int, SKRect)>() {
        (0, 0, new SKRect(0f, 0f, 1000f, 500f)),      // top 
        (0, 1, new SKRect(0f, 500f, 1000f, 1000f)),   // bottom 
      }, it);
    }

    [Fact]
    public void TestGridLayout_GetRects1c2rRowPropFactorChanged_Return2Rects()
    {
      // Arrange
      var grid_layout = new GridLayout(1,2);

      var inp_rect = new SKRect(0f, 0f, 1000f, 1000f);
      grid_layout.SetRowPropFactor(0, 3f);

      // Act
      var it = grid_layout.GetRects(inp_rect);

      // Assert
      Assert.Equal(new List<(int, int, SKRect)>() {
        (0, 0, new SKRect(0f, 0f, 1000f, 750f)),      // top 
        (0, 1, new SKRect(0f, 750f, 1000f, 1000f)),   // bottom 
      }, it);
    }

    [Fact]
    public void TestGridLayout_GetRects2c1r_Return2Rects()
    {
      // Arrange
      var grid_layout = new GridLayout(2,1);

      var inp_rect = new SKRect(0f, 0f, 1000f, 1000f);
      

      // Act
      var it = grid_layout.GetRects(inp_rect);

      // Assert
      Assert.Equal(new List<(int, int, SKRect)>() {
        (0, 0, new SKRect(0f, 0f, 500f, 1000f)),      // left 
        (1, 0, new SKRect(500f, 0f, 1000f, 1000f)),   // right
      }, it);
    }

    [Fact]
    public void TestGridLayout_GetRects2c1rColPropFactorChanged_Return2Rects()
    {
      // Arrange
      var grid_layout = new GridLayout(2,1);
      grid_layout.SetColPropFactor(0, 3f);

      var inp_rect = new SKRect(0f, 0f, 1000f, 1000f);
      

      // Act
      var it = grid_layout.GetRects(inp_rect);

      // Assert
      Assert.Equal(new List<(int, int, SKRect)>() {
        (0, 0, new SKRect(0f, 0f, 750f, 1000f)),      // left 
        (1, 0, new SKRect(750f, 0f, 1000f, 1000f)),   // right
      }, it);
    }


    [Fact]
    public void TestGridLayout_GetRects2c2r_Return4Rects()
    {
      // Arrange
      var grid_layout = new GridLayout(2,2);

      var inp_rect = new SKRect(0f, 0f, 1000f, 1000f);
      

      // Act
      var it = grid_layout.GetRects(inp_rect);

      // Assert
      Assert.Equal(new List<(int, int, SKRect)>() {
        (0, 0, new SKRect(0f, 0f, 500f, 500f)),      // top left
        (0, 1, new SKRect(0f, 500f, 500f, 1000f)),   // bottom left
        (1, 0, new SKRect(500f, 0f, 1000f, 500f)),   // top right
        (1, 1, new SKRect(500f, 500f, 1000f, 1000f)) // bottom right
      }, it);

    }

    [Fact]
    public void TestGridLayout_GetRects2c2rBottomRightPropFactorsChanged_Return4Rects()
    {
      // Arrange
      var grid_layout = new GridLayout(2,2);

      var inp_rect = new SKRect(0f, 0f, 1000f, 1000f);
      grid_layout.SetColPropFactor(1,3f);      
      grid_layout.SetRowPropFactor(1,3f);      

      // Act
      var it = grid_layout.GetRects(inp_rect);

      // Assert
      Assert.Equal(new List<(int, int, SKRect)>() {
        (0, 0, new SKRect(0f, 0f, 250f, 250f)),      // top left
        (0, 1, new SKRect(0f, 250f, 250f, 1000f)),   // bottom left
        (1, 0, new SKRect(250f, 0f, 1000f, 250f)),   // top right
        (1, 1, new SKRect(250f, 250f, 1000f, 1000f)) // bottom right
      }, it);

    }
  }
}
