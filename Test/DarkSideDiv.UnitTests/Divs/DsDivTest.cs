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
    void Draw_NonZeroPadding_CorrectDrawCallToFillContentArea() {
        // Arrange
        var div_attribs = new DsDivAttribs() {
          Padding = 100f
        };
        var mock_device = new Mock<IDsDivDevice>();
        var dut = new DsDiv(mock_device.Object, div_attribs);

        // Act
        dut.Draw(new Rect(0f,0f,1000f,1000f));

        // Assert
        mock_device.Verify(call => call.DrawContentRect(new Rect(0f, 0f, 1000f, 1000f)));
    }
  }
}