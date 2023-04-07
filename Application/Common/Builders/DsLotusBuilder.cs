using SkiaSharp;

using DarkSideDiv.Enums;
using DarkSideDiv.Components;
using DarkSideDiv.Common;
using DarkSideDiv.Divs;
using Application.Device;
using Application.Common;

namespace Application.Builders
{
  public class DsLotusBuilder
  {
    private static RectDistance CalculateCellBorder(int col, int row, float value, float value_outline)
    {
      var border = new RectDistance();
      border.distance_from_left = col == 0 ? value : 0f;
      border.distance_from_right = col == 2 ? value : 0f;
      border.distance_from_top = row == 0 ? value : 0f;
      border.distance_from_bottom = row == 2 ? value : 0f;

      border.distance_from_right = col < 2 ? value_outline : border.distance_from_right;
      border.distance_from_bottom = row < 2 ? value_outline : border.distance_from_bottom;
      return border;
    }

    public IDsDiv CreateCornerDiv(int base_grid_idx, int idx, int col, int row, ColorString fill_color)
    {
      var attribs = new DsDivAttribs()
      {
        Border = CalculateCellBorder(col, row, 2.0f, 2.0f),
        Margin = 0f,
        content_fill_color = fill_color,
        border_color = new ColorString("#000000"),
      };
      var ds_div = new DsDiv(_device_repo.DivDevice, attribs);

      var text_attribs = new DsDivComponentAlignedTextAttribs()
      {
        text = _grid_texts[base_grid_idx, idx],
        text_size = FontSize,
        alignment = Alignment
      };
      var text_comp = new DsDivComponentAlignedText(_device_repo.DivTextDevice, text_attribs);
      ds_div.Append(text_comp);
      return ds_div;
    }

    public IDsDiv CreateMiddleDiv(int idx, int col, int row)
    {
      var attribs = new DsDivAttribs()
      {
        Border = CalculateCellBorder(col, row, 2f, 2f),
        content_fill_color = _palette_algo.GetColorByIdx(idx),
        border_color = new ColorString("#000000")
      };
      var ds_div = new DsDiv(_device_repo.DivDevice, attribs);
      var text_attribs = new DsDivComponentAlignedTextAttribs()
      {
        text = _grid_texts[4, idx],
        text_size = FontSize,
        alignment = Alignment
      };
      var text_comp = new DsDivComponentAlignedText(_device_repo.DivTextDevice, text_attribs);
      ds_div.Append(text_comp);
      return ds_div;

    }

    public ColorString CreateCornerDivColor(int base_grid_sector, int grid_sector)
    {
      // if the div is in the middle use the color of the base_grid_sector
      if (grid_sector != 4)
      {
        return _palette_algo.GetColorByIdx(base_grid_sector);
      }

      return _palette_algo.GetColorByIdx(grid_sector);
    }

    public IDsDiv CreateUnderlaidGridDiv(int base_grid_sector, int grid_sector)
    {
      if (base_grid_sector == 4)
      {
        var middle_grid = CreateMiddleDiv(grid_sector, grid_sector % 3, grid_sector / 3);
        return middle_grid;
      }

      var fill_color = CreateCornerDivColor(base_grid_sector, grid_sector);
      var corner_grid = CreateCornerDiv(base_grid_sector, grid_sector, grid_sector % 3, grid_sector / 3, fill_color);

      return corner_grid;

    }
    public IDsDiv CreateUnderlaidGrid(int base_grid_sector, int base_grid_col, int base_grid_row)
    {
      var base_grid_comp = new DsDivComponentGrid(3, 3);

      for (int i = 0; i < 9; i++)
      {
        var grid_div = CreateUnderlaidGridDiv(base_grid_sector, i);
        base_grid_comp.Attach(i % 3, i / 3, grid_div);
      }


      // Attributes of the base grid
      var attribs = new DsDivAttribs()
      {
        Border = 0f,
        Margin = CalculateCellBorder(base_grid_col, base_grid_row, Border, Spacing),
        content_fill_color = new ColorString("#ffffff"),
        border_color = new ColorString("#fffeff")
      };

      var div = new DsDiv(_device_repo.DivDevice, attribs);
      div.Append(base_grid_comp);

      return div;
    }

    public DsLotusBuilder(IDeviceRepo device_repo, SKRect pic_rect)
    {
      _device_repo = device_repo;
      _pic_rect = pic_rect;
      _grid_texts = new string[9, 9];

      // Initialize Array
      for (var i = 0; i < 9; i++)
      {
        for (var j = 0; j < 9; j++)
        {
          _grid_texts[i, j] = "";
        }
      }
      _grid_texts[4, 4] = "ROOT";
    }

    public DsRoot Build()
    {
      var base_grid = new DsDivComponentGrid(3, 3);
      base_grid.SetColPropFactor(1, 2f);
      base_grid.SetRowPropFactor(1, 2f);

      for (int i = 0; i < 9; i++)
      {
        var grid = CreateUnderlaidGrid(i, i % 3, i / 3);
        base_grid.Attach(i % 3, i / 3, grid);
      }


      var attribs = new DsDivAttribs()
      {
        Border = 1f,
        //border_color = SKColor.Parse("#ff0000"),
        content_fill_color = new ColorString("#ffffff")
      };

      var div = new DsDiv(_device_repo.DivDevice, attribs);
      div.Append(base_grid);

      var root_div = new DsRoot(ConversionFactories.ToRect(_pic_rect));
      root_div.Attach(div);
      return root_div;
    }

    public void AddTopic(int level, string label)
    {
      switch (level)
      {
        case 0:
          AddLevel0(label);
          break;
        case 1:
          AddLevel1(label);
          break;
        case 2:
          AddLevel2(label);
          break;
        default:
          throw new ArgumentException($"Level {level} is not supported");
      }
    }

    void AddLevel0(string label)
    {
      if (_topic_idx != -2)
      {
        throw new Exception("Level1 was added beforehand");
      }
      _grid_texts[4, 4] = label + "\nfdfdf";
      _topic_idx++;
    }
    void AddLevel1(string label)
    {
      if (_topic_idx < -1)
      {
        throw new Exception("Level1 must be added first");
      }
      if (_topic_idx >= 9)
      {
        throw new Exception("No space left for level2 headlines.");
      }

      _topic_idx++;
      // jump over middle element
      _topic_idx = _topic_idx == 4 ? _topic_idx + 1 : _topic_idx;

      _grid_texts[4, _topic_idx] = label;
      _grid_texts[_topic_idx, 4] = label;


      // reset subtopic idx
      _subtopic_idx = -1;

    }

    void AddLevel2(string label)
    {
      if (_subtopic_idx >= 9)
      {
        throw new Exception("No space left for level 3 headlines.");
      }

      _subtopic_idx++;
      // jump over middle element
      _subtopic_idx = _subtopic_idx == 4 ? _subtopic_idx + 1 : _subtopic_idx;

      _grid_texts[_topic_idx, _subtopic_idx] = label;


    }

    int _topic_idx = -2;
    int _subtopic_idx = 0;
    private SKRect _pic_rect;

    public float FontSize
    {
      get;
      set;
    } = 20.0f;

    public float Border
    {
      get;
      set;
    } = 5.0f;

    public float Spacing
    {
      get;
      set;
    } = 15.0f;

    string[,] _grid_texts;

    DsAlignment Alignment
    {
      get;
      set;
    } = DsAlignment.TopLeft;

    DsPalette _palette_algo = new DsPalette();

    IDeviceRepo _device_repo;
  }

}