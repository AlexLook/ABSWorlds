using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia.Media.TextFormatting;

namespace ABSWorlds.Common.Models;

/// <summary>
/// Статья энциклопедии
/// </summary>
public class Topic() {
    public List<TopicPart> TopicParts { get; } = [];

    public void Clear() {
        TopicParts.Clear();
    }

    public void AddTopicPart(TopicPart topicPart) {
        TopicParts.Add(topicPart);
    }
    
    public void AddTopicParts(List<TopicPart> topicParts) {
        TopicParts.AddRange(topicParts);
    }
}
