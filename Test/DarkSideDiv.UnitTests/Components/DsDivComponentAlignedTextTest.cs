using DarkSideDiv.Components;
using DarkSideDiv.Common;
using Moq;
using Xunit;
using DarkSideDiv.Enums;

namespace Test.Common
{

  public class DsDivComponentAlignedTextTest
  {
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
      var stub = new Mock<IAbsoluteLayout>();

      mock.Setup(foo => foo.MeasureText(
        It.IsAny<string>())).Returns(new Rect(0f, 0f, 20.0f, 20.0f)
      );

      stub.Setup(f => f.GetAbsRect(
        It.IsAny<Rect>(),
        It.IsAny<Rect>(),
        It.IsAny<DsAlignment>(),
         It.IsAny<float>(),
        It.IsAny<float>()
      )).Returns(new Rect(490f, 490f, 510f, 510f));

      var attribs = CreateDefaultArguments(test_text);
      var dut = new DsDivComponentAlignedText(mock.Object, attribs);
      dut.AbsoluteLayoutAlgorithmn = stub.Object;

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
      var stub = new Mock<IAbsoluteLayout>();

      mock.Setup(foo => foo.MeasureText(
        It.IsAny<string>())).Returns(new Rect(0f, 0f, 20.0f, 20.0f)
      );

      stub.Setup(f => f.GetAbsRect(
        It.IsAny<Rect>(),
        It.IsAny<Rect>(),
        It.IsAny<DsAlignment>(),
        It.IsAny<float>(),
        It.IsAny<float>()
      )).Returns(new Rect(490f, 480f, 510f, 520f)); // 520 ... two lines with height = 20.0f dived by 2

      var attribs = CreateDefaultArguments(test_text);
      var dut = new DsDivComponentAlignedText(mock.Object, attribs);
      dut.AbsoluteLayoutAlgorithmn = stub.Object;

      // Act
      var in_rect = new Rect(0f, 0f, 1000.0f, 1000.0f);
      dut.Draw(in_rect);

      // Assert
      mock.Verify(foo => foo.DrawText("Line1", 490.0f, 500f));
      mock.Verify(foo => foo.DrawText("Line2", 490.0f, 520f));
    }


  }
}