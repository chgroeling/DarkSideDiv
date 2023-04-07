using Infrastructure;
using Xunit;

namespace Test.Common
{

  public class MarkDigDocumentConverterTest
  {
 

    [Fact]
    public void TestSimpleMarkDigCallWithTwoElements()
    {
      var test_text = "# Heading 1\n ## Heading 1.1\n";
      var markdig_converter = new MarkDigDocumentConverter();
      var list = markdig_converter.GetTableOfContents(test_text);
      Assert.Equal( 1, list[0].level);
      Assert.Equal("Heading 1", list[0].label);
      Assert.Equal(2,list[1].level);
      Assert.Equal("Heading 1.1", list[1].label);
    }

    [Fact]
    public void TestSimpleMarkDigCallWithTwoElementsAndYaml()
    {
      var test_text = "---\ntitle: this is a frontmatter\n---\n# Heading 1\n ## Heading 1.1\n";
      var markdig_converter = new MarkDigDocumentConverter();
      var list = markdig_converter.GetTableOfContents(test_text);
      Assert.Equal( 0, list[0].level);
      Assert.Equal("this is a frontmatter", list[0].label);
      Assert.Equal( 1, list[1].level);
      Assert.Equal("Heading 1", list[1].label);
      Assert.Equal(2,list[2].level);
      Assert.Equal("Heading 1.1", list[2].label);
    }
  }
}