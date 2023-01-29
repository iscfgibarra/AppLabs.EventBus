using AppLabs.EventBus.Abstractions;

namespace AppLabs.EventBus.Services;

public class EventQueueService<TEvent> : IEventQueueService<TEvent> where TEvent : BaseEvent
{
    private readonly List<TEvent> _eventList;

    public EventQueueService()
    {
        _eventList = new List<TEvent>();
    }

    public void PushEvent(TEvent evt)
    {
        _eventList.Add(evt);
    }

    public void PushEvents(IEnumerable<TEvent> list)
    {
        _eventList.AddRange(list);
    }

    public List<TEvent> PullEvents()
    {
        var list = _eventList.ToList();
        _eventList.Clear();
        return list;
    }
}