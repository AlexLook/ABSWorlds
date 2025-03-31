namespace ABSWorlds.Common.Models;

/// <summary>
/// Простая цитата
/// </summary>
/// <param name="citeText">Текст цитаты</param>
/// <param name="source">Источник</param>
/// <param name="actor">Персонаж произнесший цитату</param>
public class Cite(string citeText, StorySource source, string actor = "") {
    public string      CiteText { get; init; } = citeText;
    public StorySource Source   { get; init; } = source;
    public string      Actor    { get; init; } = actor;
    
}
