namespace ABSWorlds.Common.Models;

public class TopicPart(int order, TopicPartType partType, string content) {
    
    public int           Order    { get; init; } = order;
    public TopicPartType PartType { get; init; } = partType;
    public string        Content  { get; init; } = content;
}
