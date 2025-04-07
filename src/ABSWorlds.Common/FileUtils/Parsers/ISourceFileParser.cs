namespace ABSWorlds.Common.FileUtils.Parsers;

public interface ISourceFileParser {
    Task ParseFile(FileInfo sourceFile, string targetPath, bool prettyFormat);
}
