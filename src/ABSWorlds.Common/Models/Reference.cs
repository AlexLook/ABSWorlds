namespace ABSWorlds.Common.Models;

public class Reference(string path, string position) {
    public string Path     { get; init; } = path;
    public string Position { get; init; } = position;
}
