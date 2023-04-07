using DarkSideDiv.Components;
using DarkSideDiv.Common;
using Moq;
using Xunit;

namespace Test.Common
{

  public class DsDivComponentAlignedTextTest
  {
    [Fact]
    public void Test1()
    {
      // Arrange
      var mock = new Mock<IDsDivComponentAlignedTextDevice>();

      mock.Setup(foo => foo.MeasureText(
        It.IsAny<string>())).Returns(new Rect(0f, 0f, 100.0f, 100.0f)
      );

      var attribs = new DsDivComponentAlignedTextAttribs()
      {
        text = "HELLO WORLD",
        alignment = DarkSideDiv.Enums.DsAlignment.Center,
        text_size = 15
      };
      var dut = new DsDivComponentAlignedText(mock.Object, attribs);


      // Act
      var in_rect = new Rect(0f, 0f, 1000.0f, 1000.0f);
      dut.Draw(in_rect);

      // Assert
      mock.Verify(foo => foo.DrawText("HELLO WORLD", 450f, 500f));
    }

  }
}