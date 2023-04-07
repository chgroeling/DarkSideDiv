using DarkSideDiv.Components;
using DarkSideDiv.Common;
using Moq;
using Xunit;
using DarkSideDiv.Enums;
using DarkSideDiv.Divs;

namespace Test.Common
{

  public class DsDivTest
  {
    [Fact]
    void Draw_NonZeroPadding_CorrectDrawCallToFillContentArea()
    {
      // Arrange
      var div_attribs = new DsDivAttribs()
      {
        Padding = 100f,
        ContentFillColor = new ColorString("#000000")
      };
      var mock_device = new Mock<IDsDivDevice>();
      var dut = new DsDiv(mock_device.Object, div_attribs);
      var rect = new Rect(0f, 0f, 1000f, 1000f);
      // Act
      dut.Draw(rect, rect);

      // Assert
      mock_device.Verify(call => call.DrawContentRect(new Rect(0f, 0f, 1000f, 1000f)));
    }

    [Fact]
    void Draw_StaticPosition_CorrectDrawCallToFillContentArea()
    {
      // Arrange
      var div_attribs = new DsDivAttribs()
      {
        Position = PositionType.Static,
        ContentFillColor = new ColorString("#000000")
      };
      var mock_device = new Mock<IDsDivDevice>();
      var dut = new DsDiv(mock_device.Object, div_attribs);
      var parent_content = new Rect(0f, 0f, 300f, 300f);
      var nearest_positioned_ancestor = new Rect(0f, 0f, 1000f, 1000f);
      // Act
      dut.Draw(parent_content, nearest_positioned_ancestor);

      // Assert
      mock_device.Verify(call => call.DrawContentRect(new Rect(0f, 0f, 300f, 300f)));
    }

    [Fact]
    void Draw_AbsolutePosition_CorrectDrawCallToFillContentArea()
    {
      // Arrange
      var div_attribs = new DsDivAttribs()
      {
        Position = PositionType.Absolute,
        ContentFillColor = new ColorString("#000000")
      };
      var mock_device = new Mock<IDsDivDevice>();
      var dut = new DsDiv(mock_device.Object, div_attribs);
      var parent_content = new Rect(0f, 0f, 300f, 300f);
      var nearest_positioned_ancestor = new Rect(0f, 0f, 1000f, 1000f);
      // Act
      dut.Draw(parent_content, nearest_positioned_ancestor);

      // Assert
      mock_device.Verify(call => call.DrawContentRect(new Rect(0f, 0f, 1000f, 1000f)));
    }

    [Fact]
    void Draw_BorderColorIsNull_DoNotCallDrawBorderRect()
    {
      // Arrange
      var div_attribs = new DsDivAttribs()
      {
        BorderColor = null
      };
      var mock_device = new Mock<IDsDivDevice>();
      var dut = new DsDiv(mock_device.Object, div_attribs);
      var parent_content = new Rect(0f, 0f, 300f, 300f);
      var nearest_positioned_ancestor = new Rect(0f, 0f, 1000f, 1000f);
      // Act
      dut.Draw(parent_content, nearest_positioned_ancestor);

      // Assert
      mock_device.Verify(call => call.DrawBorderRect(It.IsAny<Rect>()), Times.Never());
    }

    [Fact]
    void Draw_ContentFillColorIsNull_DoNotCallDrawBorderRect()
    {
      // Arrange
      var div_attribs = new DsDivAttribs()
      {
        ContentFillColor = null
      };
      var mock_device = new Mock<IDsDivDevice>();
      var dut = new DsDiv(mock_device.Object, div_attribs);
      var parent_content = new Rect(0f, 0f, 300f, 300f);
      var nearest_positioned_ancestor = new Rect(0f, 0f, 1000f, 1000f);
      // Act 
      dut.Draw(parent_content, nearest_positioned_ancestor);

      // Assert
      mock_device.Verify(call => call.DrawContentRect(It.IsAny<Rect>()), Times.Never());
    }

    [Fact]
    void Draw_PercentPadding_CorrectDrawCallToFillContentArea()
    {

      // Arrange
      var div_attribs = new DsDivAttribs()
      {
        Padding = new RectDistance((QuantityType.Percent, 25.0f)),
      };
      var mock_device = new Mock<IDsDivDevice>();
      var mock_component = new Mock<IDsDivComponent>();
      var dut = new DsDiv(mock_device.Object, div_attribs);
      dut.Append(mock_component.Object);
      var rect = new Rect(0f, 0f, 1000f, 1000f);

      // Act
      dut.Draw(rect, rect);

      // Assert
      mock_component.Verify(call => call.Draw(new Rect(250f, 250f, 750f, 750f), It.IsAny<Rect>()));
    }

    [Fact]
    void Draw_PercentPaddingTop_CorrectDrawCallToFillContentArea()
    {
      // Arrange
      var div_attribs = new DsDivAttribs()
      {
        Padding = (
          (QuantityType.Percent, 0.0f),  // left
          (QuantityType.Percent, 25.0f), // top
          (QuantityType.Percent, 0.0f),  // right
          (QuantityType.Percent, 0.0f)   // bottom
        ),
      };
      var mock_device = new Mock<IDsDivDevice>();
      var mock_component = new Mock<IDsDivComponent>();
      var dut = new DsDiv(mock_device.Object, div_attribs);
      dut.Append(mock_component.Object);
      var rect = new Rect(0f, 0f, 1000f, 300f);
      // Act
      dut.Draw(rect, rect);

      // Assert
      mock_component.Verify(call => call.Draw(new Rect(0f, 250f, 1000f, 300f), It.IsAny<Rect>()));
    }

    [Fact]
    void Draw_RelativePosition_SetsRootRect()
    {
      // Arrange
      var div_attribs = new DsDivAttribs()
      {
        Position = PositionType.Relative,
      };
      var mock_device = new Mock<IDsDivDevice>();
      var mock_component = new Mock<IDsDivComponent>();
      var dut = new DsDiv(mock_device.Object, div_attribs);
      dut.Append(mock_component.Object);
      var parent_content = new Rect(0f, 0f, 300f, 300f);
      var nearest_positioned_ancestor = new Rect(0f, 0f, 1000f, 1000f);
      // Act
      dut.Draw(parent_content, nearest_positioned_ancestor);

      // Assert
      mock_component.Verify(call => call.Draw(It.IsAny<Rect>(), new Rect(0f, 0f, 300f, 300f)));
    }


    [Fact]
    void Draw_HeightTypeZero_CorrectDrawCalls()
    {
      // Arrange
      var div_attribs = new DsDivAttribs()
      {
        Height = HeightType.Zero
      };
      var mock_device = new Mock<IDsDivDevice>();
      var mock_component = new Mock<IDsDivComponent>();
      var dut = new DsDiv(mock_device.Object, div_attribs);
      dut.Append(mock_component.Object);
      var parent_content = new Rect(0f, 0f, 300f, 300f);
      var nearest_positioned_ancestor = new Rect(0f, 0f, 1000f, 1000f);
      // Act
      dut.Draw(parent_content, nearest_positioned_ancestor);

      // Assert
      mock_component.Verify(call => call.Draw(new Rect(0f, 0f, 300f, 0f), It.IsAny<Rect>()));
    }

    [Fact]
    void Draw_HeightTypeZeroAndAbsolutePosition_CorrectDrawCalls()
    {
      // Arrange
      var div_attribs = new DsDivAttribs()
      {
        Height = HeightType.Zero,
        Position = PositionType.Absolute
      };
      var mock_device = new Mock<IDsDivDevice>();
      var mock_component = new Mock<IDsDivComponent>();
      var dut = new DsDiv(mock_device.Object, div_attribs);
      dut.Append(mock_component.Object);
      var parent_content = new Rect(0f, 0f, 300f, 300f);
      var nearest_positioned_ancestor = new Rect(0f, 0f, 1000f, 1000f);
      // Act
      dut.Draw(parent_content, nearest_positioned_ancestor);

      // Assert
      mock_component.Verify(call => call.Draw(new Rect(0f, 0f, 1000f, 0f), It.IsAny<Rect>()));
    }

    [Fact]
    void Draw_HeightTypeZeroAndAbsolutePositionWithBorder_CorrectDrawCalls()
    {
      // Arrange
      var div_attribs = new DsDivAttribs()
      {
        Border = 10f,
        Position = PositionType.Absolute
      };
      var mock_device = new Mock<IDsDivDevice>();
      var mock_component = new Mock<IDsDivComponent>();
      var dut = new DsDiv(mock_device.Object, div_attribs);
      dut.Append(mock_component.Object);
      var parent_content = new Rect(0f, 0f, 300f, 300f);
      var nearest_positioned_ancestor = new Rect(0f, 0f, 1000f, 1000f);
      // Act
      dut.Draw(parent_content, nearest_positioned_ancestor);

      // Assert
      // anchor for abs position is nearest_positioned_ancestor ..
      mock_component.Verify(call => call.Draw(new Rect(0f, 0f, 1000f, 1000f), It.IsAny<Rect>()));
    }


    [Fact]
    void Draw_HeightTypeZeroAndAbsolutePositionWithBottomPadding_CorrectDrawCalls()
    {
      // Arrange
      var div_attribs = new DsDivAttribs()
      {
        Height = HeightType.Zero,
        Position = PositionType.Absolute,
        Padding = (
          (QuantityType.FixedInPixel, 0.0f),  // left
          (QuantityType.FixedInPixel, 0.0f), // top
          (QuantityType.FixedInPixel, 0.0f),  // right
          (QuantityType.FixedInPixel, 100.0f)   // bottom
        ),
      };
      var mock_device = new Mock<IDsDivDevice>();
      var mock_component = new Mock<IDsDivComponent>();
      var dut = new DsDiv(mock_device.Object, div_attribs);
      dut.Append(mock_component.Object);
      var parent_content = new Rect(0f, 0f, 300f, 300f);
      var nearest_positioned_ancestor = new Rect(0f, 0f, 1000f, 1000f);
      // Act
      dut.Draw(parent_content, nearest_positioned_ancestor);

      // Assert
      mock_component.Verify(call => call.Draw(new Rect(0f, 0f, 1000f, 100f), It.IsAny<Rect>()));
    }

    [Fact]
    void Draw_HeightTypeZeroAndAbsolutePositionWithBottomPaddingInPercent_CorrectDrawCalls()
    {
      // Arrange
      var div_attribs = new DsDivAttribs()
      {
        Height = HeightType.Zero,
        Position = PositionType.Absolute,
        Padding = (
          (QuantityType.FixedInPixel, 0.0f),  // left
          (QuantityType.FixedInPixel, 0.0f), // top
          (QuantityType.FixedInPixel, 0.0f),  // right
          (QuantityType.Percent, 25.0f)   // bottom
        ),
      };
      var mock_device = new Mock<IDsDivDevice>();
      var mock_component = new Mock<IDsDivComponent>();
      var dut = new DsDiv(mock_device.Object, div_attribs);
      dut.Append(mock_component.Object);
      var parent_content = new Rect(0f, 0f, 300f, 300f);
      var nearest_positioned_ancestor = new Rect(0f, 0f, 1000f, 1000f);
      // Act
      dut.Draw(parent_content, nearest_positioned_ancestor);

      // Assert
      mock_component.Verify(call => call.Draw(new Rect(0f, 0f, 1000f, 250f), It.IsAny<Rect>()));
    }

    [Fact]
    void Draw_HeightTypeZeroAndAbsolutePositionWithBottomPaddingAndBorder_CorrectDrawCalls()
    {
      // Arrange
      var div_attribs = new DsDivAttribs()
      {
        Height = HeightType.Zero,
        Position = PositionType.Absolute,
        Border = (
          (QuantityType.FixedInPixel, 0.0f),  // left
          (QuantityType.FixedInPixel, 0.0f), // top
          (QuantityType.FixedInPixel, 0.0f),  // right
          (QuantityType.FixedInPixel, 2.0f)   // bottom
        ),
        Padding = (
          (QuantityType.FixedInPixel, 0.0f),  // left
          (QuantityType.FixedInPixel, 0.0f), // top
          (QuantityType.FixedInPixel, 0.0f),  // right
          (QuantityType.FixedInPixel, 100.0f)   // bottom
        ),
      };
      var mock_device = new Mock<IDsDivDevice>();
      var mock_component = new Mock<IDsDivComponent>();
      var dut = new DsDiv(mock_device.Object, div_attribs);
      dut.Append(mock_component.Object);
      var parent_content = new Rect(0f, 0f, 300f, 300f);
      var nearest_positioned_ancestor = new Rect(0f, 0f, 1000f, 1000f);
      // Act
      dut.Draw(parent_content, nearest_positioned_ancestor);

      // Assert
      mock_component.Verify(call => call.Draw(new Rect(0f, 0f, 1000f, 102f), It.IsAny<Rect>()));
    }
  }
}