using Application.Common.Interfaces;

namespace Infrastructure;

public class ReadTextFile : IReadTextFile
{
  public string Read(string filename)
  {
    var text = File.ReadAllText(filename);

    return text;
  }
}