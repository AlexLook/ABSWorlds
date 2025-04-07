namespace ABSWorlds.Common.Models;

/// <summary>
/// Источник (рассказ, повесть, etc)
/// </summary>
/// <param name="shortName">Сокращение</param>
/// <param name="name">Название произведения</param>
/// <param name="year">Год публикации</param>
public class StorySource(string shortName, string name) {
    public string ShortName   { get; init; } = shortName;
    public string Name        { get; init; } = name;
}
