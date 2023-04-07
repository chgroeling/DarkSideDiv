﻿using Application;
using CommandLine;
using Infrastructure;

internal class Program
{
  public class Options
  {
    // Required option: Set a input filename
    // E.g.:
    // -i filename
    // --inputfile filename
    [Option('i', "inputfile", Required = true, HelpText = "Input filename.")]
    // C# wants that the property of an immutable type is initialized.
    // Therefore I set the default value via the equal sign.
    public string InputFilename { get; set; } = string.Empty;

    // Required option: Set a output filename
    // E.g.:
    // -o filename
    // --outputfile filename
    [Option('o', "outputfile", Required = true, HelpText = "Output filename.")]
    // C# wants that the property of an immutable type is initialized.
    // Therefore I set the default value via the equal sign.
    public string OutputFilename { get; set; } = string.Empty;


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

    var markdig_facade = new MarkDigDocumentConverter();
    var read_text_file = new ReadTextFile();

    var use_case_create_lotus_diag = new UseCaseCreateLotusDiagram(markdig_facade, read_text_file);
    use_case_create_lotus_diag.Execute(opts.InputFilename, opts.OutputFilename);
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