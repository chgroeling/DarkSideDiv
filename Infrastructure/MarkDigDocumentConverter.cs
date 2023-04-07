using Markdig;
using Markdig.Syntax;
using Markdig.Extensions.Yaml;
using Application.Common.Interfaces;

namespace Infrastructure;

public class MarkDigDocumentConverter : IDocumentConverter
{

  public IList<IDocumentConverter.Topic> GetTableOfContents(string text)
  {
    var extension = "";

    var topic_list = new List<IDocumentConverter.Topic>();
    try
    {
      Console.WriteLine($"MarkDig Version: {Markdown.Version}");

      // Builder Pattern for pipeline
      var pipeline = new MarkdownPipelineBuilder()
        .UseYamlFrontMatter()
        .Configure(extension).Build();

      var result_ast = Markdown.Parse(text, pipeline);

      foreach (var i in result_ast.Descendants<YamlFrontMatterBlock>())
      {
        var lines = i.Lines;
        topic_list.Add(new IDocumentConverter.Topic()
        {
          level = 0,
          label = lines.ToString().Replace("title: ", "")
        }
        );
      }

      // mittels descendants Erweiterungsmethode
      foreach (var i in result_ast.Descendants<HeadingBlock>())
      {
        //Console.WriteLine(i);

        if (i.Inline != null)
        {
          //Console.WriteLine(i.Inline.FirstChild);

          topic_list.Add(new IDocumentConverter.Topic()
          {
            level = i.Level,
            label = i.Inline.FirstChild?.ToString()
          }
          );
        }
      }

    }
    catch (Exception ex)
    {
      Console.WriteLine(ex);
    }

    return topic_list;
  }
}