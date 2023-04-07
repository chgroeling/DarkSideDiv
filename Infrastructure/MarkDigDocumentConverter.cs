using Markdig;
using Markdig.Syntax;
using Application.Common.Interfaces;

namespace Infrastructure;

public class MarkDigDocumentConverter : IDocumentConverter
{

  public void GetTableOfContents(string text)
  {
    var extension = "";
    try
    {
      if (text == null)
      {
        text = string.Empty;
      }

      // restrict length
      if (text.Length > 1000)
      {
        text = text.Substring(0, 1000);
      }

      Console.WriteLine($"MarkDig Version: {Markdown.Version}");

      // Builder Pattern for pipeline
      var pipeline = new MarkdownPipelineBuilder().Configure(extension).Build();

      // Different conversion functions
      var result_html = Markdown.ToHtml(text, pipeline);
      var result_txt = Markdown.ToPlainText(text, pipeline);
      var result_ast = Markdown.Parse(text, pipeline);

      // Einzelzugriff
      Console.WriteLine(result_ast[1]);

      //  Iteriere MarkDownDocument 
      //  Das listet alle Elemente in Markdown auf.
      foreach (var i in result_ast)
      {
        // Automatische Konvertierung in die Child-Klasse und setzen der Variable i_t
        if (i is Markdig.Syntax.HeadingBlock i_t)
        {
          // String interpolation
          Console.WriteLine($"Heading Block {i_t.Level}");

          var inline = i_t.Inline;

          if (inline == null)
          {
            throw new Exception("blabla");
          }

          foreach (var j in inline)
          {
            Console.WriteLine($"'{j}'");
          }

        }
        else
        {
          // Generische Abarbeitung
          Console.WriteLine($"{i.ToString()} /  {i.ToPositionText()}");
        }

      }
      Console.WriteLine("------");

      // mittels descendants Erweiterungsmethode
      foreach (var i in result_ast.Descendants<HeadingBlock>())
      {
        Console.WriteLine(i);

        if (i.Inline != null)
        {
          Console.WriteLine(i.Inline.FirstChild);
        }
      }

      Console.WriteLine("------");
      Console.WriteLine(result_html);

    }
    catch (Exception ex)
    {
      Console.WriteLine(ex);
    }
  }
}