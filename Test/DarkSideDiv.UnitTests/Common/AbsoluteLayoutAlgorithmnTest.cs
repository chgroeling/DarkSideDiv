using DarkSideDiv.Common;
using DarkSideDiv.Enums;
using Xunit;


namespace Test.Common
{
  public class AbsoluteLayoutAlgorithmnTest
  {
    [Theory]
    [InlineData(DsAlignment.Center, 450f, 475f)]
    [InlineData(DsAlignment.Bottom, 450f, 950f)]
    [InlineData(DsAlignment.BottomLeft, 0f, 950f)]
    [InlineData(DsAlignment.BottomRight, 900f, 950f)]
    [InlineData(DsAlignment.Left, 0f, 475f)]
    [InlineData(DsAlignment.Right, 900f, 475f)]
    [InlineData(DsAlignment.Top, 450f, 0f)]
    [InlineData(DsAlignment.TopLeft, 0f, 0f)]
    [InlineData(DsAlignment.TopRight, 900f, 0f)]
    public void GetAbsRect_AlignmentChange_CorrectCoordinates(DsAlignment alignment, float left, float top)
    {
      // Arrange
      var abs_layout = new AbsoluteLayoutAlgorithmn();

      var inp_rect = new Rect(0f, 0f, 1000f, 1000f);
      var content_rect = new Rect(0f, 0f, 100f, 50f);

      // Act
      var rect = abs_layout.GetAbsRect(inp_rect, content_rect, alignment);

      // Assert
      Assert.Equal(left, rect.Left);
      Assert.Equal(left + 100f, rect.Right);

      Assert.Equal(top, rect.Top);
      Assert.Equal(top + 50f, rect.Bottom);
    }

    [Theory]
    [InlineData(DsAlignment.Center, 450f, 475f)]
    [InlineData(DsAlignment.Bottom, 450f, 950f)]
    [InlineData(DsAlignment.BottomLeft, 0f, 950f)]
    [InlineData(DsAlignment.BottomRight, 900f, 950f)]
    [InlineData(DsAlignment.Left, 0f, 475f)]
    [InlineData(DsAlignment.Right, 900f, 475f)]
    [InlineData(DsAlignment.Top, 450f, 0f)]
    [InlineData(DsAlignment.TopLeft, 0f, 0f)]
    [InlineData(DsAlignment.TopRight, 900f, 0f)]
    public void GetAbsRect_AlignmentChangeWithOffset_CorrectCoordinates(DsAlignment alignment, float left, float top)
    {
      // Arrange
      var abs_layout = new AbsoluteLayoutAlgorithmn();

      var inp_rect = new Rect(0f, 0f, 1000f, 1000f);
      var content_rect = new Rect(0f, 0f, 100f, 50f);

      float x_offset = 25f;
      float y_offset = 50f;
      // Act
      var rect = abs_layout.GetAbsRect(inp_rect, content_rect, alignment, x_offset, y_offset);

      // Assert
      Assert.Equal(left + x_offset, rect.Left);
      Assert.Equal(left + 100f + x_offset, rect.Right);

      Assert.Equal(top + y_offset, rect.Top);
      Assert.Equal(top + 50f + y_offset, rect.Bottom);
    }



  }
}
