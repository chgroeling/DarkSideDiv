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
    public void Draw_SingleLine_CorrectRowOptionsInGetRectsCall()
    {
      // Arrange
      var test_text = "Line1";
      var stub_device = new Mock<IDsDivComponentAlignedTextDevice>();
      var stub_abs_layout = HelperIAbsLayoutAnyArgumentsReturnsRect(new Rect(490f, 490f, 510f, 510f));
      var mock_grid_layout = new Mock<IGridLayout>();

      stub_device.Setup(foo => foo.MeasureText(
        It.IsAny<string>())).Returns(new Rect(0f, 0f, 20.0f, 20.0f)
      );

      var attribs = CreateDefaultArguments(test_text);
      var dut = new DsDivComponentAlignedText(stub_device.Object, attribs);
      dut.AbsoluteLayoutAlgorithmn = stub_abs_layout.Object;
      dut.GridLayout = mock_grid_layout.Object;

      // Act
      var in_rect = new Rect(0f, 0f, 1000.0f, 1000.0f);
      dut.Draw(in_rect);

      // Assert
      var expected_RowOptions = new List<Quantity>() {
          (QuantityType.FixedInPixel, 20f),
      };

      mock_grid_layout.Verify(gl => gl.GetRects(
        It.Is<GridLayoutSettings>(i => Enumerable.SequenceEqual(i.RowOptions, expected_RowOptions)),
        It.IsAny<Rect>()
      ));
    }

    [Fact]
    public void Draw_MultiLine_GetRectCalledWithCorrectSettings()
    {
      // Arrange
      var test_text = "Line1\nLine2";
      var stub_device = new Mock<IDsDivComponentAlignedTextDevice>();
      var stub_abs_layout = HelperIAbsLayoutAnyArgumentsReturnsRect(new Rect(490f, 480f, 510f, 520f));
      var mock_grid_layout = new Mock<IGridLayout>();
     
      stub_device.Setup(foo => foo.MeasureText(
        It.IsAny<string>())).Returns(new Rect(0f, 0f, 20.0f, 20.0f)
      );

      var attribs = CreateDefaultArguments(test_text);
      var dut = new DsDivComponentAlignedText(stub_device.Object, attribs);
      dut.AbsoluteLayoutAlgorithmn = stub_abs_layout.Object;
      dut.GridLayout = mock_grid_layout.Object;

      // Act
      var in_rect = new Rect(0f, 0f, 1000.0f, 1000.0f);
      dut.Draw(in_rect);

      // Assert
      var expected_RowOptions = new List<Quantity>() {
          (QuantityType.FixedInPixel, 20f),
          (QuantityType.FixedInPixel, 20f)
      };

      mock_grid_layout.Verify(gl => gl.GetRects(
        It.Is<GridLayoutSettings>(i => Enumerable.SequenceEqual(i.RowOptions, expected_RowOptions)),
        It.IsAny<Rect>()
      ));
    }

    [Fact]
    public void Draw_MultiLine_CallGetRects()
    {
      // Arrange
      var test_text = "Line1\nLine2";
      var stub_device = new Mock<IDsDivComponentAlignedTextDevice>();
      var stub_abs_layout = HelperIAbsLayoutAnyArgumentsReturnsRect(new Rect(490f, 480f, 510f, 520f));
      var mock_grid_layout = new Mock<IGridLayout>();

      stub_device.Setup(foo => foo.MeasureText(
        It.IsAny<string>())).Returns(new Rect(0f, 0f, 20.0f, 20.0f)
      );

      var attribs = CreateDefaultArguments(test_text);
      var dut = new DsDivComponentAlignedText(stub_device.Object, attribs);
      dut.AbsoluteLayoutAlgorithmn = stub_abs_layout.Object;
      dut.GridLayout = mock_grid_layout.Object;

      // Act
      var in_rect = new Rect(0f, 0f, 1000.0f, 1000.0f);
      dut.Draw(in_rect);

      // Assert
      mock_grid_layout.Verify(gl => gl.GetRects(It.IsAny<GridLayoutSettings>(), new Rect(490f, 480f, 510f, 520f)));
    }


    [Fact]
    public void Draw_SingleLineAbsAlgo_CalledWithCorrectArguments()
    {
      // Arrange
      var test_text = "Test Text";
      var stub = new Mock<IDsDivComponentAlignedTextDevice>();
      var mock = new Mock<IAbsoluteLayout>();

      stub.Setup(foo => foo.MeasureText(
        It.IsAny<string>())).Returns(new Rect(0f, 0f, 100.0f, 100.0f)
      );

      var attribs = CreateDefaultArguments(test_text);
      var dut = new DsDivComponentAlignedText(stub.Object, attribs);
      dut.AbsoluteLayoutAlgorithmn = mock.Object;

      // Act
      var in_rect = new Rect(0f, 0f, 1000.0f, 1000.0f);
      dut.Draw(in_rect);

      // Assert
      mock.Verify(foo => foo.GetAbsRect(
        new Rect(0f, 0f, 1000f, 1000f),
        new Rect(0f, 0f, 100f, 100f),
        DarkSideDiv.Enums.DsAlignment.Center,
        0f,
        0f
      ));
    }

    [Fact]
    public void Draw_MultiLineAbsAlgo_CalledWithCorrectArguments()
    {
      // Arrange
      var test_text = "Line1\nLine2";
      var stub = new Mock<IDsDivComponentAlignedTextDevice>();
      var mock = new Mock<IAbsoluteLayout>();

      stub.Setup(foo => foo.MeasureText(
        It.IsAny<string>())).Returns(new Rect(0f, 0f, 100.0f, 100.0f)
      );

      var attribs = CreateDefaultArguments(test_text);
      var dut = new DsDivComponentAlignedText(stub.Object, attribs);
      dut.AbsoluteLayoutAlgorithmn = mock.Object;

      // Act
      var in_rect = new Rect(0f, 0f, 1000.0f, 1000.0f);
      dut.Draw(in_rect);

      // Assert
      mock.Verify(foo => foo.GetAbsRect(
        new Rect(0f, 0f, 1000f, 1000f),
        new Rect(0f, 0f, 100f, 200f), // 100 * 2
        DarkSideDiv.Enums.DsAlignment.Center,
        0f,
        0f
      ));
    }

    [Fact]
    public void Draw_CenterSinglelineText_PlacedCorrectly()
    {
      // Arrange
      var test_text = "Line1";
      var mock = new Mock<IDsDivComponentAlignedTextDevice>();
      var stub_abs_layout = HelperIAbsLayoutAnyArgumentsReturnsRect(new Rect(490f, 490f, 510f, 510f));

      mock.Setup(foo => foo.MeasureText(
        It.IsAny<string>())).Returns(new Rect(0f, 0f, 20.0f, 20.0f)
      );


      var attribs = CreateDefaultArguments(test_text);
      var dut = new DsDivComponentAlignedText(mock.Object, attribs);
      dut.AbsoluteLayoutAlgorithmn = stub_abs_layout.Object;

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
      var stub_abs_layout = HelperIAbsLayoutAnyArgumentsReturnsRect(new Rect(490f, 480f, 510f, 520f));

      mock.Setup(foo => foo.MeasureText(
        It.IsAny<string>())).Returns(new Rect(0f, 0f, 20.0f, 20.0f)
      );

      var attribs = CreateDefaultArguments(test_text);
      var dut = new DsDivComponentAlignedText(mock.Object, attribs);
      dut.AbsoluteLayoutAlgorithmn = stub_abs_layout.Object;

      // Act
      var in_rect = new Rect(0f, 0f, 1000.0f, 1000.0f);
      dut.Draw(in_rect);

      // Assert
      mock.Verify(foo => foo.DrawText("Line1", 490.0f, 500f));
      mock.Verify(foo => foo.DrawText("Line2", 490.0f, 520f));
    }


  }
}