namespace DarkSideDiv.Common
{
  public struct ColorString
  {
    public ColorString()
    {
      color_string = "#000000";
    }

    public ColorString(string color_string)
    {
      this.color_string = color_string;
    }
    
    public string color_string;
  }
}