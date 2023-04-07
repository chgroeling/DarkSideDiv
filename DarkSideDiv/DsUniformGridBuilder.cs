namespace DarkSideDiv
{

  public class DsUniformGridBuilder
  {
    public DsUniformGridBuilder(int cols, int rows, DsDivAttribs attribs)
    {
      _rows = rows;
      _cols = cols;
      _attribs = attribs;

      _base_div = new DsDiv(_attribs);
      _grid_component = new DsUniformGridComponent(_cols, _rows);
      _base_div.Append(_grid_component);
    }

    public void Reset()
    {
      _base_div = new DsDiv(_attribs);
      _grid_component = new DsUniformGridComponent(_cols, _rows);
      _base_div.Append(_grid_component);
    }

    public void Attach(int col, int row, IDsDiv dsdiv)
    {
      _grid_component.Attach(col, row, dsdiv);
    }

    public IDsDiv Build()
    {
      return _base_div;
    }

    private int _rows;
    private int _cols;

    DsDiv _base_div;

    DsDivAttribs _attribs;

    DsUniformGridComponent _grid_component;
  }

}

