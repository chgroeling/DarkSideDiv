using DarkSideDiv.Common;
using Xunit;

namespace Test.Common
{

  public class RectTest
  {
    [Fact]
    public void TestIsAlmostEqual_1div3_shouldReturnTrue()
    {
      // Arrange
      float val = 0.33333333f;
      var o1 = new Rect(0f,0f, val, val);
      var o2 = new Rect(0f,0f,1f/3f, 1f/3f);

      var equal = o1.IsAlmostEqual(o2);

      Assert.True(equal);
    }

  }
}