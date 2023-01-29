namespace AppLabs.EventBus.Test.Events;

public class DbEvent : BaseEvent
{
    public string Query { get; set; } = string.Empty;
}