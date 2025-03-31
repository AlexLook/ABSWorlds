using System.Collections.Generic;

namespace ABSWorlds.Common.Models;

/// <summary>
/// Цикл произведений
/// </summary>
/// <param name="shortName">Обозначение</param>
/// <param name="name">Наименование</param>
public class StoryCycle(string shortName, string name) {
    public string            ShortName { get; init; } = shortName;
    public string            Name      { get; init; } = name;
    public List<StorySource> Stories   { get; init; } = [];
}
