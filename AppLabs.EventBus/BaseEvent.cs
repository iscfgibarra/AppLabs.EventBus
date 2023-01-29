using System.ComponentModel;

namespace AppLabs.EventBus;

public enum EventType
{
    [Description("Information")]
    Information,
    [Description("Error")]
    Error,
    [Description("Debug")]
    Debug,
    [Description("Warning")]
    Warning,
}

public abstract class BaseEvent
{
    public string EventId { get; set; } = Guid.NewGuid().ToString();

    public string Description { get; set; } = string.Empty;

    public EventType EventType { get; set; } = EventType.Information;

    public DateTime RegistredAt { get; set; } = DateTime.Now;

    public DateTime EndAt { get; set; }
}