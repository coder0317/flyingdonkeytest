namespace Todo_App.Domain.Events;

public class TagDeletedEvent : BaseEvent
{
    public TagDeletedEvent(Tag tag)
    {
        Tag = tag;
    }

    public Tag Tag { get; }
}