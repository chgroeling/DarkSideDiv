using SkiaSharp;

using DarkSideDiv.Enums;
using DarkSideDiv.Common;
using DarkSideDiv.Divs;
using Application.Device;
using Application.Common;

namespace Application.Builders
{
  public class DsLotusBuilder
  {
      public DsLotusBuilder(IDeviceRepo device_repo, IGridLayoutAlgorithmn grid_layout_algorithmn, SKRect pic_rect)
    {
      _grid_layout_algorithmn = grid_layout_algorithmn;
      
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

    public IDsDiv CreateDivInCellInBlossom(int base_grid_idx, int idx, int col, int row, ColorString fill_color)
    {
      var attribs = new DsDivAttribs()
      {
        ContentFillColor = fill_color,
        BorderColor = new ColorString("#000000"),
      };
      var ds_div = new DsDiv(_device_repo.DivDevice, attribs);
      var text_attribs = new DsDivAlignedTextAttribs()
      {
        Text = _grid_texts[base_grid_idx, idx],
        TextSize = FontSize,
        Alignment = Alignment
      };

      if (idx == 4)
      {
        text_attribs.FontWeight = FontWeight.Bold;
      }
      var text_comp = new DsDivAlignedText(_device_repo.DivTextDevice, text_attribs);
      ds_div.Append(text_comp);
      return ds_div;
    }

    public IDsDiv CreateMiddleCellInMiddleBlossom(int idx, int col, int row)
    {
      var attribs = new DsDivAttribs()
      {
        ContentFillColor = _palette_algo.GetColorByIdx(idx),
        BorderColor = new ColorString("#000000")
      };
  
      var ds_div = new DsDiv(_device_repo.DivDevice, attribs);
      var text_attribs = new DsDivAlignedTextAttribs()
      {
        Text = _grid_texts[4, idx],
        TextSize = FontSize,
        FontWeight = FontWeight.Bold,
        Alignment = Alignment
      };

      if (idx == 4)
      {
        text_attribs.TextSize = FontSize + 4f * 2f;
      }
      var text_comp = new DsDivAlignedText(_device_repo.DivTextDevice, text_attribs);
      ds_div.Append(text_comp);
      return ds_div;

    }

    public ColorString GetColorOfCellInBlossom(int base_grid_sector, int grid_sector)
    {
      // if the div is in the middle use the color of the base_grid_sector
      if (grid_sector != 4)
      {
        return _palette_algo.GetColorByIdx(base_grid_sector);
      }

      return _palette_algo.GetColorByIdx(grid_sector);
    }

    public IDsDiv CreateCellInBlossom(int base_grid_sector, int grid_sector)
    {
      if (base_grid_sector == 4)
      {
        var middle_grid = CreateMiddleCellInMiddleBlossom(grid_sector, grid_sector % 3, grid_sector / 3);
        return middle_grid;
      }

      var fill_color = GetColorOfCellInBlossom(base_grid_sector, grid_sector);
      var corner_grid = CreateDivInCellInBlossom(base_grid_sector, grid_sector, grid_sector % 3, grid_sector / 3, fill_color);

      return corner_grid;

    }
    public IDsDiv CreateBlossoms(int base_grid_sector)
    {
      var base_grid_comp = new DsDivGrid( _grid_layout_algorithmn, 3, 3);
      base_grid_comp.SetDivSpacing(CellBorder);

      for (int i = 0; i < 9; i++)
      {
        var grid_div = CreateCellInBlossom(base_grid_sector, i);
        base_grid_comp.Attach(i % 3, i / 3, grid_div);
      }


      // Attributes of one blossom
      var attribs = new DsDivAttribs()
      {
        Border = CellBorder,
        ContentFillColor = new ColorString("#000000"),
        BorderColor = new ColorString("#000000")
      };

      if (base_grid_sector == 4)
      {
        attribs.Border = 5f; // make the border of the central blossom a bit thicker
      }

      var div = new DsDiv(_device_repo.DivDevice, attribs);
      div.Append(base_grid_comp);

      return div;
    }

    public DsRoot Build()
    {
      var base_grid = new DsDivGrid( _grid_layout_algorithmn, 3, 3);
      base_grid.SetDivSpacing(Spacing);

      for (int i = 0; i < 9; i++)
      {
        // 4,4 is the central blossom
        var grid = CreateBlossoms(i);
        base_grid.Attach(i % 3, i / 3, grid);
      }

      var attribs = new DsDivAttribs()
      {
        Border = Spacing, // outer border
        BorderColor = new ColorString("#ffffff"),
        ContentFillColor = new ColorString("#ffffff"),
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
      _grid_texts[4, 4] = label;
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
    } = 19.0f * 2f;

    public float Spacing
    {
      get;
      set;
    } = 20.0f;

    public float CellBorder
    {
      get;
      set;
    } = 2.0f;

    string[,] _grid_texts;

    DsAlignment Alignment
    {
      get;
      set;
    } = DsAlignment.Center;

    DsPalette _palette_algo = new DsPalette();

    IDeviceRepo _device_repo;

     IGridLayoutAlgorithmn _grid_layout_algorithmn;
  }

}