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

    public void FillGridOfGrid(int idx, int col, int row, DsUniformGrid ds_grid, SKColor fill_color)
    {
      var attr = new DsDivAttribs()
      {
        border = 1,
        margin = 10,
        content_fill_color = fill_color,
        border_color = SKColor.Parse("#000000")
      };

      var ds_div = new DsDiv(attr);
      ds_grid.Attach(col, row, ds_div);
    }


    public void FillMiddleGrid(int idx, int col, int row, DsUniformGrid ds_grid)
    {
      var attr = new DsDivAttribs()
      {
        border = 1,
        content_fill_color = GetColorByIdx(idx),
        border_color = SKColor.Parse("#000000")
      };


      var ds_div = new DsDiv(attr);
      ds_grid.Attach(col, row, ds_div);
    }


    public void FillTopLevelGrid(int idx, int col, int row, DsUniformGrid ds_grid)
    {
      var ds_grid_attribs = new DsUniformDivAttribs()
      {
        rows = 3,
        cols = 3,
        border = 0,
        margin = 0,
        content_fill_color = SKColor.Parse("#ffffff"),
        border_color = SKColor.Parse("#fffeff")
      };

      var ds_grid_of_grid = new DsUniformGrid(ds_grid_attribs);
      ds_grid.Attach(col, row, ds_grid_of_grid);

      for (int i = 0; i < 9; i++)
      {
        if (idx == 4)
        {
          FillMiddleGrid(i, i % 3, i / 3, ds_grid_of_grid);
        }
        else
        {
          var fill_color = SKColor.Empty;
          if (i != 4)
          {
            fill_color = GetColorByIdx(idx);
          }
          else
          {
            fill_color = GetColorByIdx(i);
          }
          FillGridOfGrid(i, i % 3, i / 3, ds_grid_of_grid, fill_color);
        }
      }
    }

    public DsLotusBuilder(SKRect pic_rect)
    {


      _actual = new DsRoot(pic_rect);
      var ds_grid_attribs = new DsUniformDivAttribs()
      {
        rows = 3,
        cols = 3,
        border = 1
      };
      _base_grid = new DsUniformGrid(ds_grid_attribs);
      _actual.Attach(_base_grid);

      _base_grid_divs = new List<DsDiv>();

      for (int i = 0; i < 9; i++)
      {
        FillTopLevelGrid(i, i % 3, i / 3, _base_grid);
      }

    }

    public void AddRoot(string rootstr)
    {

    }

    public void AddLevel0(string title)
    {

    }

    public void AddLevel1(string title)
    {

    }

    public DsRoot Build()
    {

      return _actual;
    }

    DsRoot _actual;
    DsUniformGrid _base_grid;

    List<DsUniformGrid> _grid_of_grids;

    List<DsDiv> _base_grid_divs;


  }

}