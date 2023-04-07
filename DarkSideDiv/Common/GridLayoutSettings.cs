namespace DarkSideDiv.Common;

public struct GridLayoutOptions
{
  public int Cols { get; set; }
  
  public int Rows { get; set; }
  
  public List<Quantity> ColOptions { get; set; }

  public List<Quantity> RowOptions { get; set; }

  public float CellSpacing {get; set;}

};
