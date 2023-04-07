namespace Application.Common.Interfaces;

public interface IDocumentConverter
{
  struct Topic
  {
    public int level;
    public string? label;
  };

  IList<Topic> GetTableOfContents(string text);
};