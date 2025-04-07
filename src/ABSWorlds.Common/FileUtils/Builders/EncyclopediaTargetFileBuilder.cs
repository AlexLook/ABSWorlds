using System.Text.Json;
using ABSWorlds.Common.Models;

namespace ABSWorlds.Common.FileUtils.Builders;

public class EncyclopediaTargetFileBuilder {
    private readonly List<Topic> _topics = [];

    public void AddTopic(Topic topic) { _topics.Add(topic); }

    public void Clear() { _topics.Clear(); }

    public async Task BuildDataFile(string targetFile, bool prettyFormat) {
        var options = new JsonSerializerOptions { WriteIndented = prettyFormat };
        var content = JsonSerializer.Serialize(_topics, options);
        if (string.IsNullOrEmpty(content)) throw new InvalidOperationException("Serialization failed!");

        var             fileInfo = new FileInfo(targetFile);
        await using var writer   = fileInfo.CreateText();
        await writer.WriteAsync(content);
        await writer.FlushAsync();
        writer.Close();
    }
}