using System.Text.Json;
using ABSWorlds.Common.Models;

namespace ABSWorlds.Common.FileUtils.Builders;

public class SourceTargetFileBuilder {
    private readonly List<StorySource> _sources = [];

    public void AddSource(StorySource source) { _sources.Add(source); }

    public void Clear() { _sources.Clear(); }

    public async Task BuildDataFile(string targetFile, bool prettyFormat) {
        var options = new JsonSerializerOptions { WriteIndented = prettyFormat };
        var content = JsonSerializer.Serialize(_sources, options);
        if (string.IsNullOrEmpty(content)) throw new InvalidOperationException("Serialization failed!");

        var             fileInfo = new FileInfo(targetFile);
        await using var writer   = fileInfo.CreateText();
        await writer.WriteAsync(content);
        await writer.FlushAsync();
        writer.Close();
    }
}
