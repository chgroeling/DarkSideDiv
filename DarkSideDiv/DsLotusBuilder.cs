using SkiaSharp;
using System.Collections;
using System.Collections.Generic;

namespace DarkSideDiv
{

  public class DsLotusBuilder
  {
    public SKColor GetColorByIdx(int idx)
    {
      var fill_color = SKColor.Parse("#FFFFFF");
      switch (idx)
      {
        case 0:
          fill_color = SKColor.Parse("#7f8081");
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

    public void FillCornerGrid(int idx, int col, int row, DsUniformGridComponent grid_of_grid_component, SKColor fill_color)
    {
      var attribs = new DsDivAttribs()
      {
        Border = 1f,
        Margin = 0f,
        content_fill_color = fill_color,
        border_color = SKColor.Parse("#000000"),
      };
      var ds_div = new DsDiv(attribs);
      grid_of_grid_component.Attach(col, row, ds_div);
    }


    public void FillMiddleGrid(int idx, int col, int row, DsUniformGridComponent grid_of_grid_component)
    {
      var attribs = new DsDivAttribs()
      {
        Border = 1f,
        content_fill_color = GetColorByIdx(idx),
        border_color = SKColor.Parse("#000000")
      };
      var ds_div = new DsDiv(attribs);
      grid_of_grid_component.Attach(col, row, ds_div);
    }


    public void FillGridInGrid(int base_grid_sector, int grid_sector, DsUniformGridComponent grid_component)
    {
      if (base_grid_sector == 4)
      {
        FillMiddleGrid(grid_sector, grid_sector % 3, grid_sector / 3, grid_component);
      }
      else
      {
        var fill_color = SKColor.Empty;
        if (grid_sector != 4)
        {
          fill_color = GetColorByIdx(base_grid_sector);
        }
        else
        {
          fill_color = GetColorByIdx(grid_sector);
        }
        FillCornerGrid(grid_sector, grid_sector % 3, grid_sector / 3, grid_component, fill_color);
      }
    }
    public void FillTopLevelGrid(int base_grid_sector, int base_grid_col, int base_grid_row, DsUniformGridComponent base_grid)
    {
      var grid_component = new DsUniformGridComponent(3, 3);

      for (int i = 0; i < 9; i++)
      {
        FillGridInGrid(base_grid_sector, i, grid_component);
      }

      var margin = new DsDivRectDistance();

      // BORDER of lotus diagramm
      margin.distance_from_left = base_grid_col == 0 ? Border : 0f;
      margin.distance_from_right = base_grid_col == 2 ? Border : 0f;
      margin.distance_from_top = base_grid_row == 0 ? Border : 0f;
      margin.distance_from_bottom = base_grid_row == 2 ? Border : 0f;

      // SPACING of lotus diagramm
      margin.distance_from_right =  base_grid_col < 2 ? Spacing : margin.distance_from_right;
      margin.distance_from_bottom = base_grid_row < 2 ? Spacing : margin.distance_from_bottom;

      // Attributes of the base grid
      var attribs = new DsDivAttribs()
      {
        Border = 0f,
        Margin = margin,
        content_fill_color = SKColor.Parse("#ffffff"),
        border_color = SKColor.Parse("#fffeff")
      };

      var div = new DsDiv(attribs);
      div.Append(grid_component);
      base_grid.Attach(base_grid_col, base_grid_row, div);
    }

    public DsLotusBuilder(SKRect pic_rect)
    {
      var base_grid = new DsUniformGridComponent(3, 3);

      for (int i = 0; i < 9; i++)
      {
        FillTopLevelGrid(i, i % 3, i / 3, base_grid);
      }

      var attribs = new DsDivAttribs()
      {
        Border = 1f,
        content_fill_color = SKColor.Parse("#ffffff")
      };

      var div = new DsDiv(attribs);
      div.Append(base_grid);

      _actual = new DsRoot(pic_rect);
      _actual.Attach(div);
    }

    public DsRoot Build()
    {

      return _actual;
    }

    private DsRoot _actual;

    public float Border
    {
      get;
      set;
    } = 10.0f;

    public float Spacing
    {
      get;
      set;
    } = 25.0f;

  }

}