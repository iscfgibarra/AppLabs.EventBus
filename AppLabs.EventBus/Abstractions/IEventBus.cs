namespace AppLabs.EventBus.Abstractions;

public interface IEventBus
{
    SubsToken Subscribe<TEvent>(Func<TEvent, EventArgs, Task> eventHandler) where TEvent : BaseEvent;
    Task Publish<TEvent>(TEvent evnt, EventArgs args) where TEvent : BaseEvent;
    Task Publish<TEvent>(List<TEvent> evnts, EventArgs args) where TEvent : BaseEvent;
    void UnSubscribe<TEvent>(SubsToken subsToken) where TEvent : BaseEvent;
    bool HasSubscription<TEvent>(SubsToken subsToken) where TEvent : BaseEvent;
    bool HasSubscriptions<TEvent>() where TEvent : BaseEvent;
}