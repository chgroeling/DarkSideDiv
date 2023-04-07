using DarkSideDiv.Components;
using DarkSideDiv.Common;
using Moq;
using Xunit;
using DarkSideDiv.Enums;

namespace Test.Common
{

  public class DsDivComponentAlignedTextTest
  {
    static Mock<IAbsoluteLayout> HelperIAbsLayoutAnyArgumentsReturnsRect(Rect ret)
    {
      var mock = new Mock<IAbsoluteLayout>();
      mock.Setup(f => f.GetAbsRect(
       It.IsAny<Rect>(),
       It.IsAny<Rect>(),
       It.IsAny<DsAlignment>(),
       It.IsAny<float>(),
       It.IsAny<float>()
     )).Returns(ret);

      return mock;
    }


    static DsDivComponentAlignedTextAttribs CreateDefaultArguments(string text)
    {
      var attribs = new DsDivComponentAlignedTextAttribs()
      {
        text = text,
        alignment = DarkSideDiv.Enums.DsAlignment.Center,
        text_size = 15
      };

      return attribs;
    }

    [Fact]
    public void Draw_SingleLine_GetRectCalledWithCorrectRowOptions()
    {
      // Arrange
      var test_text = "Line1";
      var stub_device = new Mock<IDsDivComponentAlignedTextDevice>();
      var mock_grid_layout = new Mock<IGridLayout>();

      stub_device.Setup(foo => foo.MeasureText(
        It.IsAny<string>())).Returns(new Rect(0f, 0f, 20.0f, 20.0f)
      );

      var attribs = CreateDefaultArguments(test_text);
      var dut = new DsDivComponentAlignedText(stub_device.Object, attribs);
      dut.GridLayoutAlgorithmn = mock_grid_layout.Object;

      // Act
      var in_rect = new Rect(0f, 0f, 1000.0f, 1000.0f);
      dut.Draw(in_rect);

      // Assert
      var expected_RowOptions = new List<Quantity>() {
          (QuantityType.FixedInPixel, 20f),
      };

      mock_grid_layout.Verify(gl => gl.GetRects(
        It.Is<GridLayoutOptions>(i => Enumerable.SequenceEqual(i.RowOptions, expected_RowOptions)),
        It.IsAny<Rect>()
      ));
    }

    [Fact]
    public void Draw_MultiLine_GetRectCalledWithCorrectRowOptions()
    {
      // Arrange
      var test_text = "Line1\nLine2";
      var stub_device = new Mock<IDsDivComponentAlignedTextDevice>();
      var mock_grid_layout = new Mock<IGridLayout>();
     
      stub_device.Setup(foo => foo.MeasureText(
        It.IsAny<string>())).Returns(new Rect(0f, 0f, 20.0f, 20.0f)
      );

      var attribs = CreateDefaultArguments(test_text);
      var dut = new DsDivComponentAlignedText(stub_device.Object, attribs);
      dut.GridLayoutAlgorithmn = mock_grid_layout.Object;

      // Act
      var in_rect = new Rect(0f, 0f, 1000.0f, 1000.0f);
      dut.Draw(in_rect);

      // Assert
      var expected_RowOptions = new List<Quantity>() {
          (QuantityType.FixedInPixel, 20f),
          (QuantityType.FixedInPixel, 20f)
      };

      mock_grid_layout.Verify(gl => gl.GetRects(
        It.Is<GridLayoutOptions>(i => Enumerable.SequenceEqual(i.RowOptions, expected_RowOptions)),
        It.IsAny<Rect>()
      ));
    }

    [Fact]
    public void Draw_MultiLine_CallGetRects()
    {
      // Arrange
      var test_text = "Line1\nLine2";
      var stub_device = new Mock<IDsDivComponentAlignedTextDevice>();
      var mock_grid_layout = new Mock<IGridLayout>();

      stub_device.Setup(foo => foo.MeasureText(
        It.IsAny<string>())).Returns(new Rect(0f, 0f, 20.0f, 20.0f)
      );

      var attribs = CreateDefaultArguments(test_text);
      var dut = new DsDivComponentAlignedText(stub_device.Object, attribs);
      dut.GridLayoutAlgorithmn = mock_grid_layout.Object;

      // Act
      var in_rect = new Rect(0f, 0f, 1000.0f, 1000.0f);
      dut.Draw(in_rect);

      // Assert
      mock_grid_layout.Verify(gl => gl.GetRects(It.IsAny<GridLayoutOptions>(), new Rect(490f, 480f, 510f, 520f)));
    }


    [Fact]
    public void Draw_CenterSinglelineText_PlacedCorrectly()
    {
      // Arrange
      var test_text = "Line1";
      var mock = new Mock<IDsDivComponentAlignedTextDevice>();
      
      mock.Setup(foo => foo.MeasureText(
        It.IsAny<string>())).Returns(new Rect(0f, 0f, 20.0f, 20.0f)
      );


      var attribs = CreateDefaultArguments(test_text);
      var dut = new DsDivComponentAlignedText(mock.Object, attribs);
  
      // Act
      var in_rect = new Rect(0f, 0f, 1000.0f, 1000.0f);
      dut.Draw(in_rect);

      // Assert
      mock.Verify(foo => foo.DrawText("Line1", 490.0f, 510f));
    }

    [Fact]
    public void Draw_CenterMultilineText_PlacedCorrectly()
    {
      // Arrange
      var test_text = "Line1\nLine2";
      var mock = new Mock<IDsDivComponentAlignedTextDevice>();
 
      mock.Setup(foo => foo.MeasureText(
        It.IsAny<string>())).Returns(new Rect(0f, 0f, 20.0f, 20.0f)
      );

      var attribs = CreateDefaultArguments(test_text);
      var dut = new DsDivComponentAlignedText(mock.Object, attribs);

      // Act
      var in_rect = new Rect(0f, 0f, 1000.0f, 1000.0f);
      dut.Draw(in_rect);

      // Assert
      mock.Verify(foo => foo.DrawText("Line1", 490.0f, 500f));
      mock.Verify(foo => foo.DrawText("Line2", 490.0f, 520f));
    }

     [Fact]
    public void Draw_CenterTooLongTextAutowrapEnabledLinesAreEqualLong_PlacedCorrectly()
    {
      // Arrange
      var test_text = "Line1 Line2";
      var mock = new Mock<IDsDivComponentAlignedTextDevice>();

      mock.Setup(foo => foo.MeasureText("Line1 Line2")).Returns(new Rect(0f, 0f, 200.0f, 20.0f));
      mock.Setup(foo => foo.MeasureText("Line1")).Returns(new Rect(0f, 0f, 100.0f, 20.0f));
      mock.Setup(foo => foo.MeasureText("Line2")).Returns(new Rect(0f, 0f, 100.0f, 20.0f));


      var attribs = CreateDefaultArguments(test_text);
      var dut = new DsDivComponentAlignedText(mock.Object, attribs);
    
      // Act
      var in_rect = new Rect(0f, 0f, 150.0f, 1000.0f);
      dut.Draw(in_rect);

      // Assert
      mock.Verify(foo => foo.DrawText("Line1", 25.0f, 500f));
      mock.Verify(foo => foo.DrawText("Line2", 25.0f, 520f));
    }


     [Fact]
    public void Draw_CenterTooLongWord_PlacedCorrectly()
    {
      // Arrange
      var test_text = "Line1AAAAAAA";
      var mock = new Mock<IDsDivComponentAlignedTextDevice>();

      mock.Setup(foo => foo.MeasureText("Line1AAAAAAA")).Returns(new Rect(0f, 0f, 300.0f, 20.0f));

      var stub_abs_layout = HelperIAbsLayoutAnyArgumentsReturnsRect(new Rect(-25f, 490f, 275f, 510f));
      var attribs = CreateDefaultArguments(test_text);
      var dut = new DsDivComponentAlignedText(mock.Object, attribs);
      // Wie geht man mit Algorithmen bzw. Helper Methoden um
      ///dut.AbsoluteLayoutAlgorithmn = stub_abs_layout.Object;

      // Act
      var in_rect = new Rect(0f, 0f, 250.0f, 1000.0f);
      dut.Draw(in_rect);

      // Assert
      mock.Verify(foo => foo.DrawText("Line1AAAAAAA", -25.0f, 510f));
    }

     [Fact]
    public void Draw_CenterTooLongTextAutowrapEnabled_PlacedCorrectly()
    {
      // Arrange
      var test_text = "Line1A Line1B Line2A";
      var mock = new Mock<IDsDivComponentAlignedTextDevice>();

      mock.Setup(foo => foo.MeasureText("Line1A Line1B Line2A")).Returns(new Rect(0f, 0f, 300.0f, 20.0f));
      mock.Setup(foo => foo.MeasureText("Line1A Line1B")).Returns(new Rect(0f, 0f, 200.0f, 20.0f));
      mock.Setup(foo => foo.MeasureText("Line1A")).Returns(new Rect(0f, 0f, 100.0f, 20.0f));
      mock.Setup(foo => foo.MeasureText("Line1B")).Returns(new Rect(0f, 0f, 100.0f, 20.0f));
      mock.Setup(foo => foo.MeasureText("Line2A")).Returns(new Rect(0f, 0f, 100.0f, 20.0f));

      var attribs = CreateDefaultArguments(test_text);
      var dut = new DsDivComponentAlignedText(mock.Object, attribs);
    
      // Act
      var in_rect = new Rect(0f, 0f, 250.0f, 1000.0f);
      dut.Draw(in_rect);

      // Assert
      mock.Verify(foo => foo.DrawText("Line1A Line1B", 25.0f, 500f));
      mock.Verify(foo => foo.DrawText("Line2A", 75.0f, 520f));
    }

     [Fact]
    public void Draw_CenterTooLongTextAutowrapEnabledSpaceInLine_PlacedCorrectly()
    {
      // Arrange
      var test_text = "Line1A Line1B Line A";
      var mock = new Mock<IDsDivComponentAlignedTextDevice>();

      mock.Setup(foo => foo.MeasureText("Line1A Line1B Line A")).Returns(new Rect(0f, 0f, 300.0f, 20.0f));
      mock.Setup(foo => foo.MeasureText("Line1A Line1B Line")).Returns(new Rect(0f, 0f, 275.0f, 20.0f));
      mock.Setup(foo => foo.MeasureText("Line1A Line1B")).Returns(new Rect(0f, 0f, 200.0f, 20.0f));
      mock.Setup(foo => foo.MeasureText("Line1A")).Returns(new Rect(0f, 0f, 100.0f, 20.0f));
      mock.Setup(foo => foo.MeasureText("Line1B")).Returns(new Rect(0f, 0f, 100.0f, 20.0f));
      mock.Setup(foo => foo.MeasureText("Line")).Returns(new Rect(0f, 0f, 75.0f, 20.0f));
      mock.Setup(foo => foo.MeasureText("Line A")).Returns(new Rect(0f, 0f, 100.0f, 20.0f));

      var attribs = CreateDefaultArguments(test_text);
      var dut = new DsDivComponentAlignedText(mock.Object, attribs);
    
      // Act
      var in_rect = new Rect(0f, 0f, 250.0f, 1000.0f);
      dut.Draw(in_rect);

      // Assert
      mock.Verify(foo => foo.DrawText("Line1A Line1B", 25.0f, 500f));
      mock.Verify(foo => foo.DrawText("Line A", 75.0f, 520f));
    }




  }
}