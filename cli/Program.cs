using Application;
using CommandLine;
using MarkDigFacade;

internal class Program
{
  public class Options
  {
    // Required option: Set a input filename
    // E.g.:
    // -f filename
    // --filename filename
    [Option('f', "filename", Required = true, HelpText = "Input filename.")]
    // C# wants that the property of an immutable type is initialized.
    // Therefore I set the default value via the equal sign.
    public string InputFilename { get; set; } = string.Empty;


    // Used to enable verbose messages. Usually associated with logging.
    [Option('v', "verbose", Required = false, HelpText = "Set output to verbose messages.")]
    public bool Verbose { get; set; }
  }

  private static void Run(Options opts)
  {
    
    // Checking the existence of the specified
    if (!File.Exists(opts.InputFilename))
    {
     throw new FileNotFoundException($"File {opts.InputFilename} does not exists");
    }

    var text = File.ReadAllText(opts.InputFilename);
    Console.WriteLine(text);


    MarkDigExample.Test(text, "");

    UseCases.CreateLotusDiagram();

  }

  private static void HandleParseError(IEnumerable<Error> errors)
  {
    // handle errors
  }

  private static void Main(string[] args)
  {
    Parser.Default.ParseArguments<Options>(args)
          .WithParsed<Options>(Run)
          .WithNotParsed(HandleParseError); // errors is a sequence of type IEnumerable<Error>

  }

}