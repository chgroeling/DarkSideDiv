using DarkSideDiv;
using SkiaSharp;
using Xunit;

namespace tests {

public class DsRectDimensionsTest
{
    [Fact]
    public void TestCalculateBorderRect()
    {
      var dut = new DsRectDimensions();
      var rect = new SKRect(0.0f, 100.0f,200.0f, 300.0f);
      var result = dut.CalculateBorderRect(rect, 10.0f);
    
      Assert.Equal(10.0f, result.Left);
      Assert.Equal(290.0f, result.Bottom);
    }

    [Fact]
    public void TestCalculateContentRect()
    {
      var dut = new DsRectDimensions();
      var rect = new SKRect(0.0f, 100.0f,200.0f, 300.0f);
      var result = dut.CalculateContentRect(rect, 10.0f, 1.0f, 2.0f);
    
      Assert.Equal(13.0f, result.Left);
      Assert.Equal(287.0f, result.Bottom);
    }
}
}