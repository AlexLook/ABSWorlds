namespace ABSWorlds.Common.FileUtils.Parsers;

public class SourceFileParser : ISourceFileParser{
    public async Task ParseFile(FileInfo sourceFile, string targetPath, bool prettyFormat) {
        var sourceType = FileTypeQualifier.GetSourceFileType(sourceFile);

        ISourceFileParser parser = sourceType switch {
                SourceFileType.Appendices          => new AppendicesSourceFileParser(),
                SourceFileType.Article             => new ArticleSourceFileParser(),
                SourceFileType.Bibliography        => new BibliographySourceFileParser(),
                SourceFileType.Book                => new BookSourceFileParser(),
                SourceFileType.Cites               => new CitesSourceFileParser(),
                SourceFileType.CommentsToPath      => new CommentsToPathSourceFileParser(),
                SourceFileType.Encyclopedia        => new EncyclopediaSourceFileParser(),
                SourceFileType.NoondayWorldHistory => new NoondayWorldHistorySourceFileParser(),
                SourceFileType.Source              => new SourceSourceFileParser(),
                _                                  => throw new ArgumentOutOfRangeException("FileSourceType")
        };
        
        await parser.ParseFile(sourceFile, targetPath, prettyFormat);
    }
}