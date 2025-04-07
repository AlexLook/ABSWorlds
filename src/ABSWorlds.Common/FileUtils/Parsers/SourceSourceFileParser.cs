using ABSWorlds.Common.FileUtils.Builders;
using ABSWorlds.Common.Models;

namespace ABSWorlds.Common.FileUtils.Parsers;

public class SourceSourceFileParser : ISourceFileParser {
    public async Task ParseFile(FileInfo sourceFile, string targetPath, bool prettyFormat) {
        var builder = new SourceTargetFileBuilder();
        builder.Clear();

        using var reader = sourceFile.OpenText();
        while (await reader.ReadLineAsync() is { } line) {
            if (line.StartsWith('#')            ||
                string.IsNullOrWhiteSpace(line) ||
                line.IndexOf(':') < 1) continue;

            var parts = line.Split(':', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 2) continue;
            
            var shortName = parts[0];
            var name      = parts[1];

            builder.AddSource(new StorySource(shortName, name));
        }

        var targetFile = Path.Combine(targetPath, sourceFile.Name.Replace("sdf", "abswdf"));
        await builder.BuildDataFile(targetFile, prettyFormat);
    }
}
