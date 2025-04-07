using ArgumentOutOfRangeException = System.ArgumentOutOfRangeException;

namespace ABSWorlds.Common.FileUtils;

public static class FileTypeQualifier {
    private const int MinVersion    = 1;
    private const int MinSubVersion = 0;

    public static SourceFileType GetSourceFileType(FileInfo file) {
        string?  fileHeader;
        string? formatVersion;
        string? sourceFileType;

        using (var reader = file.OpenText()) {
            fileHeader     = reader.ReadLine();
            formatVersion  = reader.ReadLine();
            sourceFileType = reader.ReadLine();
            reader.Close();
        }

        if (fileHeader == null || !fileHeader.Equals("# ABSWorlds Source Data File", StringComparison.InvariantCulture))
            throw new FormatException("File format is not source type.");
        if (formatVersion == null || !formatVersion.StartsWith("# Format Version:"))
            throw new FormatException("File format is corrupt: Unknown format version.");
        if (sourceFileType == null || !sourceFileType.StartsWith("# Source Type:"))
            throw new FormatException("File format is corrupt: No source type.");

        var fullVersion = formatVersion.Replace("# Format Version:", "").Trim();
        if (!int.TryParse(fullVersion[.. fullVersion.IndexOf('.')],      out var version)) version = 0;
        if (!int.TryParse(fullVersion[(fullVersion.IndexOf('.') + 1)..], out var subVersion)) subVersion = 0;

        if (version    < MinVersion)    throw new FormatException("Format version is too old.");
        if (subVersion < MinSubVersion) throw new FormatException("Format subversion is too old.");

        var sourceType = sourceFileType.Replace("# Source Type:", "").Trim();

        return sourceType switch {
                "Appendices"          => SourceFileType.Appendices,
                "Article"             => SourceFileType.Article,
                "Bibliography"        => SourceFileType.Bibliography,
                "Book"                => SourceFileType.Book,
                "Cites"               => SourceFileType.Cites,
                "CommentsToPath"      => SourceFileType.CommentsToPath,
                "Encyclopedia"        => SourceFileType.Encyclopedia,
                "NoondayWorldHistory" => SourceFileType.NoondayWorldHistory,
                "Source"              => SourceFileType.Source,
                _                     => throw new ArgumentOutOfRangeException("SourceFileType")
        };
    }
}