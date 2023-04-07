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
        Text = text,
        Alignment = DarkSideDiv.Enums.DsAlignment.Center,
        TextSize = 15
      };

      return attribs;
    }

    [Fact]
    public void Draw_ArbitaryLine_SetupIsCalled()
    {
      // Arrange
      var test_text = "Line1";
      var mock = new Mock<IDsDivComponentAlignedTextDevice>();

      var attribs = CreateDefaultArguments(test_text);
      var dut = new DsDivComponentAlignedText(mock.Object, attribs);

      // Act
      var in_rect = new Rect(0f, 0f, 1000.0f, 1000.0f);
      dut.Draw(in_rect, in_rect);

      // Assert
      mock.Verify(call => call.Setup(attribs));
    }


    [Fact]
    public void Draw_CenterSinglelineText_PlacedCorrectly()
    {
      // Arrange
      var test_text = "Line1";
      var mock = new Mock<IDsDivComponentAlignedTextDevice>();
      var attribs = CreateDefaultArguments(test_text);

      mock.Setup(call => call.Setup(attribs)).Returns(new FontMetrics()
      {
        Leading = 0f,
        Ascent = -15f,
        Descent = 5f,
      });

      mock.Setup(foo => foo.MeasureText(
        It.IsAny<string>())).Returns(new Rect(0f, -15f, 20.0f, 2.0f)
      );

      var dut = new DsDivComponentAlignedText(mock.Object, attribs);

      // Act
      var in_rect = new Rect(0f, 0f, 1000.0f, 1000.0f);
      dut.Draw(in_rect, in_rect);

      // Assert
      // Center Rect is 490f .. 510f ... Ascent is 15 means Draw Text should be called with 505
      mock.Verify(foo => foo.DrawText("Line1", 490.0f, 505f));
    }

    [Fact]
    public void Draw_CenterSinglelineTextWithLongWord_PlacedCorrectly()
    {
      // Arrange
      var test_text = "Line1";
      var mock = new Mock<IDsDivComponentAlignedTextDevice>();
      var attribs = CreateDefaultArguments(test_text);

      mock.Setup(call => call.Setup(attribs)).Returns(new FontMetrics()
      {
        Leading = 0f,
        Ascent = -15f,
        Descent = 5f,
      });

      mock.Setup(foo => foo.MeasureText(
        It.IsAny<string>())).Returns(new Rect(0f, -15f, 120.0f, 2.0f)
      );

      var dut = new DsDivComponentAlignedText(mock.Object, attribs);

      // Act
      var in_rect = new Rect(0f, 0f, 100.0f, 1000.0f);
      dut.Draw(in_rect, in_rect);

      // Assert
      mock.Verify(foo => foo.DrawText("Line1", -10.0f, 505f));
    }


    [Fact]
    public void Draw_CenterSinglelineTextLeadingNotZero_PlacedCorrectly()
    {
      // Arrange
      var test_text = "Line1";
      var mock = new Mock<IDsDivComponentAlignedTextDevice>();
      var attribs = CreateDefaultArguments(test_text);

      mock.Setup(call => call.Setup(attribs)).Returns(new FontMetrics()
      {
        Leading = 2f,
        Ascent = -15f,
        Descent = 3f,
      });

      mock.Setup(foo => foo.MeasureText(
        It.IsAny<string>())).Returns(new Rect(0f, -15f, 20.0f, 2.0f)
      );

      var dut = new DsDivComponentAlignedText(mock.Object, attribs);

      // Act
      var in_rect = new Rect(0f, 0f, 1000.0f, 1000.0f);
      dut.Draw(in_rect, in_rect);

      // Assert
      // Center Rect is 490f .. 510f ... Ascent is 15 means Draw Text should be called with 505
      mock.Verify(foo => foo.DrawText("Line1", 490.0f, 505f));
    }

    [Fact]
    public void Draw_CenterMultilineText_PlacedCorrectly()
    {
      // Arrange
      var test_text = "Line1\nLine2";
      var mock = new Mock<IDsDivComponentAlignedTextDevice>();

      mock.Setup(foo => foo.MeasureText(
        It.IsAny<string>())).Returns(new Rect(0f, -15f, 20.0f, 3.0f)
      );

      var attribs = CreateDefaultArguments(test_text);
      mock.Setup(call => call.Setup(attribs)).Returns(new FontMetrics()
      {
        Leading = 0f,
        Ascent = -15f,
        Descent = 5f,
      });
      var dut = new DsDivComponentAlignedText(mock.Object, attribs);

      // Act
      var in_rect = new Rect(0f, 0f, 1000.0f, 1000.0f);
      dut.Draw(in_rect, in_rect);

      // Assert
      mock.Verify(foo => foo.DrawText("Line1", 490.0f, 495f));
      mock.Verify(foo => foo.DrawText("Line2", 490.0f, 515f));
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
      mock.Setup(call => call.Setup(attribs)).Returns(new FontMetrics()
      {
        Leading = 0f,
        Ascent = -15f,
        Descent = 5f,
      });

      var dut = new DsDivComponentAlignedText(mock.Object, attribs);

      // Act
      var in_rect = new Rect(0f, 0f, 150.0f, 1000.0f);
      dut.Draw(in_rect, in_rect);

      // Assert
      mock.Verify(foo => foo.DrawText("Line1", 25.0f, 495f));
      mock.Verify(foo => foo.DrawText("Line2", 25.0f, 515f));
    }


    [Fact]
    public void Draw_CenterTooLongWord_PlacedCorrectly()
    {
      // Arrange
      var test_text = "Line1AAAAAAA";
      var mock = new Mock<IDsDivComponentAlignedTextDevice>();

      mock.Setup(foo => foo.MeasureText("Line1AAAAAAA")).Returns(new Rect(0f, -14f, 300.0f, 2.0f));

      var attribs = CreateDefaultArguments(test_text);
      mock.Setup(call => call.Setup(attribs)).Returns(new FontMetrics()
      {
        Leading = 0f,
        Ascent = -15f,
        Descent = 5f,
      });
      var dut = new DsDivComponentAlignedText(mock.Object, attribs);
      // Wie geht man mit Algorithmen bzw. Helper Methoden um
      ///dut.AbsoluteLayoutAlgorithmn = stub_abs_layout.Object;

      // Act
      var in_rect = new Rect(0f, 0f, 250.0f, 1000.0f);
      dut.Draw(in_rect, in_rect);

      // Assert
      mock.Verify(foo => foo.DrawText("Line1AAAAAAA", -25.0f, 505f));
    }

    [Fact]
    public void Draw_CenterTooLongTextAutowrapEnabled_PlacedCorrectly()
    {
      // Arrange
      var test_text = "Line1A Line1B Line2A";
      var mock = new Mock<IDsDivComponentAlignedTextDevice>();

      mock.Setup(foo => foo.MeasureText("Line1A Line1B Line2A")).Returns(new Rect(0f, -13f, 300.0f, 2f));
      mock.Setup(foo => foo.MeasureText("Line1A Line1B")).Returns(new Rect(0f, 0f, 200.0f, 20.0f));
      mock.Setup(foo => foo.MeasureText("Line1A")).Returns(new Rect(0f, 0f, 100.0f, 20.0f));
      mock.Setup(foo => foo.MeasureText("Line1B")).Returns(new Rect(0f, 0f, 100.0f, 20.0f));
      mock.Setup(foo => foo.MeasureText("Line2A")).Returns(new Rect(0f, 0f, 100.0f, 20.0f));

      var attribs = CreateDefaultArguments(test_text);
      mock.Setup(call => call.Setup(attribs)).Returns(new FontMetrics()
      {
        Leading = 0f,
        Ascent = -15f,
        Descent = 5f,
      });
      var dut = new DsDivComponentAlignedText(mock.Object, attribs);

      // Act
      var in_rect = new Rect(0f, 0f, 250.0f, 1000.0f);
      dut.Draw(in_rect, in_rect);

      // Assert
      mock.Verify(foo => foo.DrawText("Line1A Line1B", 25.0f, 495f));
      mock.Verify(foo => foo.DrawText("Line2A", 75.0f, 515f));
    }

    [Fact]
    public void Draw_CenterTooLongTextAutowrapEnabledSpaceIn2ndLine_PlacedCorrectly()
    {
      // Arrange
      var test_text = "Line1A Line1B Line A";
      var mock = new Mock<IDsDivComponentAlignedTextDevice>();

      mock.Setup(foo => foo.MeasureText("Line1A Line1B Line A")).Returns(new Rect(0f, -14f, 300.0f, 2.0f));
      mock.Setup(foo => foo.MeasureText("Line1A Line1B Line")).Returns(new Rect(0f, -14f, 275.0f, 2.0f));
      mock.Setup(foo => foo.MeasureText("Line1A Line1B")).Returns(new Rect(0f, -15f, 200.0f, 2.0f));
      mock.Setup(foo => foo.MeasureText("Line1A")).Returns(new Rect(0f, -13f, 100.0f, 2.0f));
      mock.Setup(foo => foo.MeasureText("Line1B")).Returns(new Rect(0f, -13f, 100.0f, 2.0f));
      mock.Setup(foo => foo.MeasureText("Line")).Returns(new Rect(0f, -14f, 75.0f, 2.0f));
      mock.Setup(foo => foo.MeasureText("Line A")).Returns(new Rect(0f, -15f, 100.0f, 2.0f));

      var attribs = CreateDefaultArguments(test_text);
      mock.Setup(call => call.Setup(attribs)).Returns(new FontMetrics()
      {
        Leading = 0f,
        Ascent = -15f,
        Descent = 5f,
      });
      var dut = new DsDivComponentAlignedText(mock.Object, attribs);

      // Act
      var in_rect = new Rect(0f, 0f, 250.0f, 1000.0f);
      dut.Draw(in_rect, in_rect);

      // Assert
      mock.Verify(foo => foo.DrawText("Line1A Line1B", 25.0f, 495f));
      mock.Verify(foo => foo.DrawText("Line A", 75.0f, 515f));
    }

  }
}