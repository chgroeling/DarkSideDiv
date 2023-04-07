using DarkSideDiv.Common;
using DarkSideDiv.Enums;
using Xunit;


namespace Test.Common
{
  public class AbsoluteLayoutTest
  {

    [Theory]
    [InlineData(DsAlignment.Center, 450f, 525f)]
    [InlineData(DsAlignment.Bottom, 450f, 1000f)]
    [InlineData(DsAlignment.BottomLeft, 0f, 1000f)]
    [InlineData(DsAlignment.BottomRight, 900f, 1000f)]
    [InlineData(DsAlignment.Left, 0f, 525f)]
    [InlineData(DsAlignment.Right, 900f, 525f)]
    [InlineData(DsAlignment.Top, 450f, 50f)]
    [InlineData(DsAlignment.TopLeft, 0f, 50f)]
    [InlineData(DsAlignment.TopRight, 900f, 50f)]
    public void TestGetOffset_AlignmentChange_CorrectCoordinates(DsAlignment alignment, float x_expected, float y_expected)
    {
      // Arrange
      var abs_layout = new AbsoluteLayout();

      var inp_rect = new Rect(0f, 0f, 1000f, 1000f);
      var content_rect = new Rect(0f, 0f, 100f, 50f);

      // Act
      var (x, y) = abs_layout.GetOffset(inp_rect, content_rect, alignment);

      // Assert
      Assert.Equal(x_expected, x);
      Assert.Equal(y_expected, y);
    }




  }
}
