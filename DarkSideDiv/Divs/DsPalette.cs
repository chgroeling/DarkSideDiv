using SkiaSharp;

namespace DarkSideDiv.Divs
{
  public class DsPalette
  {
    public SKColor GetColorByIdx(int idx)
    {
      var color = SKColor.Parse("#FFFFFF");
      switch (idx)
      {
        case 0:
          color = SKColor.Parse("#dbd4d0");
          break;
        case 1:
          color = SKColor.Parse("#ded9c1");
          break;
        case 2:
          color = SKColor.Parse("#c3d9f4");
          break;
        case 3:
          color = SKColor.Parse("#dae5f2");
          break;
        case 4:
          color = SKColor.Parse("#fffeff");
          break;
        case 5:
          color = SKColor.Parse("#f4dbd9");
          break;
        case 6:
          color = SKColor.Parse("#eaf1dd");
          break;
        case 7:
          color = SKColor.Parse("#e5dfed");
          break;
        case 8:
          color = SKColor.Parse("#d7eef3");
          break;


        default:
          color = SKColor.Parse("#FFFFFF");
          break;
      }
      return color;
    }

    
  }

}