using DarkSideDiv.Common;
using DarkSideDiv.Enums;
using Xunit;


namespace Test.Common
{
  public class GridLayoutAlgorithmnTest
  {
    // Signature
    // Grid1x1
    [Fact]
    public void GetRects_Grid1x1Default_ReturnSourceRect()
    {
      // Info: All values used in the test have an accurate float representation
      // therefore direct comparision works as expected.

      // Arrange
      var grid_layout = new GridLayoutAlgorithmn();

      var inp_rect = new Rect(0f, 0f, 1000f, 1000f);

      var settings = new GridLayoutOptions
      {
        Cols = 1,
        Rows = 1
      };

      // Act
      var it = GridLayoutAlgorithmn.GetRects(settings, inp_rect);

      // Assert
      Assert.Equal(new List<(int, int, Rect)>() {
        (0, 0, new Rect(0f, 0f, 1000f, 1000f))
      }, it);
    }

    [Fact]
    public void GetRects_Grid1x1Col0FixedInPixel_ReturnSmallerRect()
    {
      // Info: All values used in the test have an accurate float representation
      // therefore direct comparision works as expected.

      // Arrange
      var grid_layout = new GridLayoutAlgorithmn();
      var inp_rect = new Rect(0f, 0f, 1000f, 1000f);
      var settings = new GridLayoutOptions
      {
        Cols = 1,
        Rows = 1,
        ColOptions = new List<Quantity>() { (QuantityType.FixedInPixel, 100f) }
      };
      // Act
      var it = GridLayoutAlgorithmn.GetRects(settings, inp_rect);

      // Assert
      Assert.Equal(new List<(int, int, Rect)>() {
        (0, 0, new Rect(0f, 0f, 100f, 1000f))
      }, it);
    }

    [Fact]
    public void GetRects_Grid1x1Col0Percent_ReturnSmallerRect()
    {
      // Info: All values used in the test have an accurate float representation
      // therefore direct comparision works as expected.

      // Arrange
      var grid_layout = new GridLayoutAlgorithmn();
      var inp_rect = new Rect(0f, 0f, 1000f, 1000f);
      var settings = new GridLayoutOptions
      {
        Cols = 1,
        Rows = 1,
        ColOptions = new List<Quantity>() { (QuantityType.Percent, 25f) }
      };
      // Act
      var it = GridLayoutAlgorithmn.GetRects(settings, inp_rect);

      // Assert
      Assert.Equal(new List<(int, int, Rect)>() {
        (0, 0, new Rect(0f, 0f, 250f, 1000f))
      }, it);
    }



    [Fact]
    public void GetRects_Grid1x1Row0FixedInPixel_ReturnSmallerRect()
    {
      // Info: All values used in the test have an accurate float representation
      // therefore direct comparision works as expected.

      // Arrange
      var grid_layout = new GridLayoutAlgorithmn();
      var inp_rect = new Rect(0f, 0f, 1000f, 1000f);

      var settings = new GridLayoutOptions
      {
        Cols = 1,
        Rows = 1,
        RowOptions = new List<Quantity>() { (QuantityType.FixedInPixel, 100f) }
      };
      // Act
      var it = GridLayoutAlgorithmn.GetRects(settings, inp_rect);

      // Assert
      Assert.Equal(new List<(int, int, Rect)>() {
        (0, 0, new Rect(0f, 0f, 1000f, 100f))
      }, it);
    }

    [Fact]
    public void GetRects_Grid1x1Row0Percent_ReturnSmallerRect()
    {
      // Info: All values used in the test have an accurate float representation
      // therefore direct comparision works as expected.

      // Arrange
      var grid_layout = new GridLayoutAlgorithmn();
      var inp_rect = new Rect(0f, 0f, 1000f, 1000f);

      var settings = new GridLayoutOptions
      {
        Cols = 1,
        Rows = 1,
        RowOptions = new List<Quantity>() { (QuantityType.Percent, 25f) }
      };
      // Act
      var it = GridLayoutAlgorithmn.GetRects(settings, inp_rect);

      // Assert
      Assert.Equal(new List<(int, int, Rect)>() {
        (0, 0, new Rect(0f, 0f, 1000f, 250f))
      }, it);
    }

    

    [Fact]
    public void GetRects_Grid1x2Row0FixedInPixel_Return2Rects()
    {
      // Info: All values used in the test have an accurate float representation
      // therefore direct comparision works as expected.

      // Arrange
      var grid_layout = new GridLayoutAlgorithmn();
      var inp_rect = new Rect(0f, 0f, 1000f, 1000f);
      var settings = new GridLayoutOptions
      {
        Cols = 1,
        Rows = 2,
        RowOptions = new List<Quantity>() { (QuantityType.FixedInPixel, 100f) }
      };


      // Act
      var it = GridLayoutAlgorithmn.GetRects(settings, inp_rect);

      // Assert
      Assert.Equal(new List<(int, int, Rect)>() {
        (0, 0, new Rect(0f, 0f, 1000f, 100f)),
        (0, 1, new Rect(0f, 100f, 1000f, 1000f))
      }, it);
    }

    [Fact]
    public void GetRects_Grid1x2Row0FixedInPixelRow1FixedInPixel_Returns2Rects()
    {
      // Info: All values used in the test have an accurate float representation
      // therefore direct comparision works as expected.

      // Arrange
      var grid_layout = new GridLayoutAlgorithmn();
      var inp_rect = new Rect(0f, 0f, 1000f, 1000f);

      var settings = new GridLayoutOptions
      {
        Cols = 1,
        Rows = 2,
        RowOptions = new List<Quantity>() {
          ( QuantityType.FixedInPixel, 100f),
          ( QuantityType.FixedInPixel, 250f)
        }
      };


      // Act
      var it = GridLayoutAlgorithmn.GetRects(settings, inp_rect);

      // Assert
      Assert.Equal(new List<(int, int, Rect)>() {
        (0, 0, new Rect(0f, 0f, 1000f, 100f)),
        (0, 1, new Rect(0f, 100f, 1000f, 350f))
      }, it);
    }

   

    [Fact]
    public void GetRects_Grid2x1Col0FixedInPixel_Returns2Rects()
    {
      // Info: All values used in the test have an accurate float representation
      // therefore direct comparision works as expected.

      // Arrange
      var grid_layout = new GridLayoutAlgorithmn();
      var inp_rect = new Rect(0f, 0f, 1000f, 1000f);

      var settings = new GridLayoutOptions
      {
        Cols = 2,
        Rows = 1,
        ColOptions = new List<Quantity>() { (QuantityType.FixedInPixel, 100f) }
      };

      // Act
      var it = GridLayoutAlgorithmn.GetRects(settings, inp_rect);

      // Assert
      Assert.Equal(new List<(int, int, Rect)>() {
        (0, 0, new Rect(0f, 0f, 100f, 1000f)),      // left
        (1, 0, new Rect(100f, 0f, 1000f, 1000f)),   // right
      }, it);
    }

    [Fact]
    public void GetRects_Grid3x1Col0FixedInPixel_Returns3Rects()
    {
      // Info: All values used in the test have an accurate float representation
      // therefore direct comparision works as expected.

      // Arrange
      var grid_layout = new GridLayoutAlgorithmn();
      var inp_rect = new Rect(0f, 0f, 1000f, 1000f);
      var settings = new GridLayoutOptions
      {
        Cols = 3,
        Rows = 1,
        ColOptions = new List<Quantity>() { (QuantityType.FixedInPixel, 100f) }
      };

      // Act
      var it = GridLayoutAlgorithmn.GetRects(settings, inp_rect);

      // Assert
      Assert.Equal(new List<(int, int, Rect)>() {
        (0, 0, new Rect(0f, 0f, 100f, 1000f)),      // left
        (1, 0, new Rect(100f, 0f, 550f, 1000f)),   // middle
        (2, 0, new Rect(550f, 0f, 1000f, 1000f)),   // right
      }, it);
    }


    [Fact]
    public void GetRects_Grid1x2Default_Return2EventSplitRects()
    {
      // Info: All values used in the test have an accurate float representation
      // therefore direct comparision works as expected.

      // Arrange
      var grid_layout = new GridLayoutAlgorithmn();

      var inp_rect = new Rect(0f, 0f, 1000f, 1000f);

      var settings = new GridLayoutOptions
      {
        Cols = 1,
        Rows = 2
      };


      // Act
      var it = GridLayoutAlgorithmn.GetRects(settings, inp_rect);

      // Assert
      Assert.Equal(new List<(int, int, Rect)>() {
        (0, 0, new Rect(0f, 0f, 1000f, 500f)),      // top 
        (0, 1, new Rect(0f, 500f, 1000f, 1000f)),   // bottom 
      }, it);
    }



    [Fact]
    public void GetRects_Grid1x2Row0Weight_Return2Rects()
    {
      // Info: All values used in the test have an accurate float representation
      // therefore direct comparision works as expected.

      // Arrange
      var grid_layout = new GridLayoutAlgorithmn();
      var inp_rect = new Rect(0f, 0f, 1000f, 1000f);
      var settings = new GridLayoutOptions
      {
        Cols = 1,
        Rows = 2,
        RowOptions = new List<Quantity>() { (QuantityType.Weight, 3f) }
      };


      // Act
      var it = GridLayoutAlgorithmn.GetRects(settings, inp_rect);

      // Assert
      Assert.Equal(new List<(int, int, Rect)>() {
        (0, 0, new Rect(0f, 0f, 1000f, 750f)),      // top 
        (0, 1, new Rect(0f, 750f, 1000f, 1000f)),   // bottom 
      }, it);
    }

    [Fact]
    public void GetRects_Grid2x1Default_Return2EventSplitRects()
    {
      // Info: All values used in the test have an accurate float representation
      // therefore direct comparision works as expected.

      // Arrange
      var grid_layout = new GridLayoutAlgorithmn();

      var inp_rect = new Rect(0f, 0f, 1000f, 1000f);

      var settings = new GridLayoutOptions
      {
        Cols = 2,
        Rows = 1
      };


      // Act
      var it = GridLayoutAlgorithmn.GetRects(settings, inp_rect);

      // Assert
      Assert.Equal(new List<(int, int, Rect)>() {
        (0, 0, new Rect(0f, 0f, 500f, 1000f)),      // left 
        (1, 0, new Rect(500f, 0f, 1000f, 1000f)),   // right
      }, it);
    }

    [Fact]
    public void GetRects_Grid2x1Col0Weight_Return2Rects()
    {
      // Info: All values used in the test have an accurate float representation
      // therefore direct comparision works as expected.

      // Arrange
      var grid_layout = new GridLayoutAlgorithmn();
      var inp_rect = new Rect(0f, 0f, 1000f, 1000f);
      var settings = new GridLayoutOptions
      {
        Cols = 2,
        Rows = 1,
        ColOptions = new List<Quantity>() { (QuantityType.Weight, 3f) }
      };

      // Act
      var it = GridLayoutAlgorithmn.GetRects(settings, inp_rect);

      // Assert
      Assert.Equal(new List<(int, int, Rect)>() {
        (0, 0, new Rect(0f, 0f, 750f, 1000f)),      // left 
        (1, 0, new Rect(750f, 0f, 1000f, 1000f)),   // right
      }, it);
    }


    [Fact]
    public void GetRects_Grid2x2Default_Return4EvenlySpacedRects()
    {
      // Arrange
      var grid_layout = new GridLayoutAlgorithmn();

      var inp_rect = new Rect(0f, 0f, 1000f, 1000f);

      var settings = new GridLayoutOptions
      {
        Cols = 2,
        Rows = 2
      };

      // Act
      var it = GridLayoutAlgorithmn.GetRects(settings, inp_rect);

      // Assert
      Assert.Equal(new List<(int, int, Rect)>() {
        (0, 0, new Rect(0f, 0f, 500f, 500f)),      // top left
        (0, 1, new Rect(0f, 500f, 500f, 1000f)),   // bottom left
        (1, 0, new Rect(500f, 0f, 1000f, 500f)),   // top right
        (1, 1, new Rect(500f, 500f, 1000f, 1000f)) // bottom right
      }, it);

    }

    [Fact]
    public void GetRects_Grid2x2CellSpacing100_Return4EvenlySpacedRects()
    {
      // Arrange
      var grid_layout = new GridLayoutAlgorithmn();

      var inp_rect = new Rect(0f, 0f, 1000f, 1000f);

      var settings = new GridLayoutOptions
      {
        Cols = 2,
        Rows = 2,
        CellSpacing = 100f
      };

      // Act
      var it = GridLayoutAlgorithmn.GetRects(settings, inp_rect);

      // Assert
      Assert.Equal(new List<(int, int, Rect)>() {
        (0, 0, new Rect(0f, 0f, 450f, 450f)),      // top left
        (0, 1, new Rect(0f, 550f, 450f, 1000f)),   // bottom left
        (1, 0, new Rect(550f, 0f, 1000f, 450f)),   // top right
        (1, 1, new Rect(550f, 550f, 1000f, 1000f)) // bottom right
      }, it);

    }

    

     [Fact]
    public void GetRects_Grid2x2CellSpacing100Row01WeightChangedCol01WeightChanged_Return4EvenlySpacedRects()
    {
      // Arrange
      var grid_layout = new GridLayoutAlgorithmn();

      var inp_rect = new Rect(0f, 0f, 1000f, 1000f);

      var settings = new GridLayoutOptions
      {
        Cols = 2,
        Rows = 2,
        CellSpacing = 100f,
        RowOptions = new List<Quantity>() {
          (QuantityType.Weight, 1f),
          (QuantityType.Weight, 3f)
        },
        ColOptions = new List<Quantity>() {
          (QuantityType.Weight, 1f),
          (QuantityType.Weight, 3f)
        }
      };

      // Act
      var it = GridLayoutAlgorithmn.GetRects(settings, inp_rect);

      // Assert
      Assert.Equal(new List<(int, int, Rect)>() {
        (0, 0, new Rect(0f, 0f, 225f, 225f)),      // top left
        (0, 1, new Rect(0f, 325f, 225f, 1000f)),   // bottom left
        (1, 0, new Rect(325f, 0f, 1000f, 225f)),   // top right
        (1, 1, new Rect(325f, 325f, 1000f, 1000f)) // bottom right
      }, it);

    }


    [Fact]
    public void GetRects_Grid2x2Row01WeightChangedCol01WeightChanged_Return4Rects()
    {
      // Info: All values used in the test have an accurate float representation
      // therefore direct comparision works as expected.

      // Arrange
      var grid_layout = new GridLayoutAlgorithmn();

      var inp_rect = new Rect(0f, 0f, 1000f, 1000f);
      var settings = new GridLayoutOptions
      {
        Cols = 2,
        Rows = 2,
        RowOptions = new List<Quantity>() {
          (QuantityType.Weight, 1f),
          (QuantityType.Weight, 3f)
        },
        ColOptions = new List<Quantity>() {
          (QuantityType.Weight, 1f),
          (QuantityType.Weight, 3f)
        }
      };

      // Act
      var it = GridLayoutAlgorithmn.GetRects(settings, inp_rect);

      // Assert
      Assert.Equal(new List<(int, int, Rect)>() {
        (0, 0, new Rect(0f, 0f, 250f, 250f)),      // top left
        (0, 1, new Rect(0f, 250f, 250f, 1000f)),   // bottom left
        (1, 0, new Rect(250f, 0f, 1000f, 250f)),   // top right
        (1, 1, new Rect(250f, 250f, 1000f, 1000f)) // bottom right
      }, it);

    }

    [Fact]
    public void GetRects_Grid2x2Row0PercentRow1WeightCol0PercentCol1Weight_Return4Rects()
    {
      // Info: All values used in the test have an accurate float representation
      // therefore direct comparision works as expected.

      // Arrange
      var grid_layout = new GridLayoutAlgorithmn();

      var inp_rect = new Rect(0f, 0f, 1000f, 1000f);
      var settings = new GridLayoutOptions
      {
        Cols = 2,
        Rows = 2,
        RowOptions = new List<Quantity>() {
          (QuantityType.Percent, 25f),
          (QuantityType.Weight, 1f)
        },
        ColOptions = new List<Quantity>() {
          (QuantityType.Percent, 25f),
          (QuantityType.Weight, 1f)
        }
      };

      // Act
      var it = GridLayoutAlgorithmn.GetRects(settings, inp_rect);

      // Assert
      Assert.Equal(new List<(int, int, Rect)>() {
        (0, 0, new Rect(0f, 0f, 250f, 250f)),      // top left
        (0, 1, new Rect(0f, 250f, 250f, 1000f)),   // bottom left
        (1, 0, new Rect(250f, 0f, 1000f, 250f)),   // top right
        (1, 1, new Rect(250f, 250f, 1000f, 1000f)) // bottom right
      }, it);

    }

  }
}
