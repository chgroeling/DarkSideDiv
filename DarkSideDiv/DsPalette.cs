using SkiaSharp;

namespace DarkSideDiv
{
  public class DsPalette {
    public SKColor GetColorByIdx(int idx)
    {
      var fill_color = SKColor.Parse("#FFFFFF");
      switch (idx)
      {
        case 0:
          fill_color = SKColor.Parse("#dbd4d0");
          break;
        case 1:
          fill_color = SKColor.Parse("#ded9c1");
          break;
        case 2:
          fill_color = SKColor.Parse("#c3d9f4");
          break;
        case 3:
          fill_color = SKColor.Parse("#dae5f2");
          break;
        case 4:
          fill_color = SKColor.Parse("#fffeff");
          break;
        case 5:
          fill_color = SKColor.Parse("#f4dbd9");
          break;
        case 6:
          fill_color = SKColor.Parse("#eaf1dd");
          break;
        case 7:
          fill_color = SKColor.Parse("#e5dfed");
          break;
        case 8:
          fill_color = SKColor.Parse("#d7eef3");
          break;


        default:
          fill_color = SKColor.Parse("#FFFFFF");
          break;
      }
      return fill_color;
    }
  }

}