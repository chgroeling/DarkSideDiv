using DarkSideDiv.Common;

namespace DarkSideDiv.Divs
{
  public class DsPalette
  {
    public ColorString GetColorByIdx(int idx)
    {
      var color = new ColorString("#FFFFFF");
      switch (idx)
      {
        case 0:
          color = new ColorString("#dbd4d0");
          break;
        case 1:
          color = new ColorString("#ded9c1");
          break;
        case 2:
          color = new ColorString("#c3d9f4");
          break;
        case 3:
          color = new ColorString("#dae5f2");
          break;
        case 4:
          color = new ColorString("#fffeff");
          break;
        case 5:
          color = new ColorString("#f4dbd9");
          break;
        case 6:
          color = new ColorString("#eaf1dd");
          break;
        case 7:
          color = new ColorString("#e5dfed");
          break;
        case 8:
          color = new ColorString("#d7eef3");
          break;


        default:
          color = new ColorString("#FFFFFF");
          break;
      }
      return color;
    }


  }

}