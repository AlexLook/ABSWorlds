using ABSWorlds.Common.FileUtils.Builders;
using ABSWorlds.Common.Models;

namespace ABSWorlds.Common.FileUtils.Parsers;

/// <summary>
/// Парсер исходных данных для энциклопедии
/// </summary>
public class EncyclopediaSourceFileParser : ISourceFileParser {
    public async Task ParseFile(FileInfo sourceFile, string targetPath, bool prettyFormat) {
        var builder = new EncyclopediaTargetFileBuilder();
        builder.Clear();

        var       order  = 0;
        using var reader = sourceFile.OpenText();
        while (await reader.ReadLineAsync() is { } line) {
            var topic = new Topic();
            if (line.StartsWith('#') || string.IsNullOrWhiteSpace(line)) continue;

            var startMarker = line[0];
            line = line.Remove(0, 1);

            var partType = startMarker switch {
                    '>' => TopicPartType.Term,
                    '^' => TopicPartType.AlternativeTerm,
                    '&' => TopicPartType.TermNote,
                    '=' => TopicPartType.Description,
                    '@' => TopicPartType.Source,
                    '!' => TopicPartType.Cite,
                    '$' => TopicPartType.Reference,
                    _   => TopicPartType.Unknown
            };

            var             content = string.Empty;
            List<TopicPart> parts   = [];

            switch (partType) {
                case TopicPartType.Term:
                case TopicPartType.TermNote:
                case TopicPartType.Description:
                    content = line;
                    break;
                case TopicPartType.AlternativeTerm:
                    parts.AddRange(ParseAlternativeTerms(line));
                    break;
                case TopicPartType.Source:
                    parts.AddRange(ParseSources(line));
                    break;
                case TopicPartType.Cite:
                    parts.AddRange(ParseCites(line));
                    break;
                case TopicPartType.Reference:
                    parts.AddRange(ParseReferences(line));
                    break;
                case TopicPartType.Unknown:
                    content = null;
                    break;
            }

            if (parts.Count > 0)
                topic.AddTopicParts(parts);
            else if (!string.IsNullOrEmpty(content)) {
                topic.AddTopicPart(new TopicPart(order++, partType, content));
            }
            
            builder.AddTopic(topic);
        }

        var targetFile = Path.Combine(targetPath, sourceFile.Name.Replace("sdf", "abswdf"));
        await builder.BuildDataFile(targetFile, prettyFormat);
    }

    private IEnumerable<TopicPart> ParseAlternativeTerms(string line) {
        var parts = new List<TopicPart>();
        return parts;
    }

    private IEnumerable<TopicPart> ParseSources(string line) {
        var parts = new List<TopicPart>();
        return parts;
    }

    private IEnumerable<TopicPart> ParseCites(string line) {
        var parts = new List<TopicPart>();
        return parts;
    }

    private IEnumerable<TopicPart> ParseReferences(string line) {
        var parts = new List<TopicPart>();
        return parts;
    }
}
