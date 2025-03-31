using System.CommandLine;

Console.WriteLine("ABSWorlds Data Tool. Version 1.0");

var sourceFileOption = new Option<FileInfo?>(
        name: "--source",
        description: "Source data file",
        isDefault: true,
        parseArgument: result => {
            if (result.Tokens.Count == 0) {
                result.ErrorMessage = "Source file not defined.";
                return null;
            }

            var filePath = result.Tokens.Single().Value;
            if (!File.Exists(filePath)) {
                result.ErrorMessage = "Source file does not exists.";
                return null;
            }

            return new FileInfo(filePath);
        });

var targetPathOption = new Option<string>(
        name: "--targetPath",
        description: "Target path for output data file.",
        getDefaultValue: () => string.Empty);

var prettyFormatOption = new Option<bool>(
        name: "--pretty-format",
        description: "Pretty format output data file.",
        getDefaultValue: () => false);

var rootCommand = new RootCommand("Data tools for ABSWorlds application.");

var buildCommand = new Command("build", "Build data file") {sourceFileOption, targetPathOption };

buildCommand.SetHandler(
        /*async*/ (sourceFile, targetPath, prettyFormat) => {
            /*await*/ Console.WriteLine($"build from {sourceFile} to {targetPath} with {prettyFormat}");
        },
        sourceFileOption,
        targetPathOption,
        prettyFormatOption);

rootCommand.AddCommand(buildCommand);
rootCommand.Invoke(args);