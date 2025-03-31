using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia.Media.TextFormatting;

namespace ABSWorlds.Common.Models;

/// <summary>
/// Статья энциклопедии
/// </summary>
/// <param name="term">Термин/Наименование</param>
/// <param name="alternativeTerms">Альтернативные термины/наименования</param>
/// <param name="termNote">Примечания к термину/наименованию</param>
/// <param name="description">Описание</param>
/// <param name="sources">Источники</param>
/// <param name="cites">Список цитат</param>
/// <param name="references">Ссылки</param>
public class Topic(
        string            term,
        List<string>      alternativeTerms,
        string            termNote,
        string            description, // м.б. сделать сложнее из разных частей?
        List<StorySource> sources,
        List<Cite>        cites,
        List<Reference>   references) {
    public string            Term             { get; init; } = term;
    public List<string>      AlternativeTerms { get; init; } = alternativeTerms;
    public string            TermNote         { get; init; } = termNote;
    public string            Description      { get; init; } = description;
    public List<StorySource> Sources          { get; init; } = sources;
    public List<Cite>        Cites            { get; init; } = cites;
    public List<Reference>   References       { get; init; } = references;
}
