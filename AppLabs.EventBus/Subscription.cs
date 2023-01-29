using AppLabs.EventBus.Abstractions;

namespace AppLabs.EventBus;

public class Subscription<TEvent> : ISubscription where TEvent : BaseEvent
{
    private event Func<TEvent, EventArgs, Task> EventHandler;

    public SubsToken SubsToken { get; }

    public Subscription(Func<TEvent, EventArgs, Task> eventHandler, SubsToken subsToken)
    {
        EventHandler = eventHandler;
        SubsToken = subsToken;
    }


    public async Task Publish(BaseEvent evnt, EventArgs args)
    {
        await EventHandler((TEvent)evnt, args);
    }
}