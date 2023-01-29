using AppLabs.EventBus.Abstractions;

namespace AppLabs.EventBus;

public class EventBus : IEventBus
{
    private readonly IDictionary<Type, List<ISubscription>> _subscriptions;

    public EventBus()
    {
        _subscriptions = new Dictionary<Type, List<ISubscription>>();
    }

    public SubsToken Subscribe<TEvent>(Func<TEvent, EventArgs, Task> eventHandler) where TEvent : BaseEvent
    {
        var token = new SubsToken();
        var subscription = new Subscription<TEvent>(eventHandler, token);

        if (!_subscriptions.ContainsKey(typeof(TEvent)))
        {
            _subscriptions.Add(typeof(TEvent), new List<ISubscription>() { subscription });
        }
        else
        {
            _subscriptions[typeof(TEvent)].Add(subscription);
        }

        return token;
    }

    public async Task Publish<TEvent>(TEvent evnt, EventArgs args) where TEvent : BaseEvent
    {
        if (!_subscriptions.ContainsKey(typeof(TEvent)))
        {
            return;
        }

        foreach (var subscription in _subscriptions[typeof(TEvent)])
        {
            await subscription.Publish(evnt, args);
        }
    }

    public async Task Publish<TEvent>(List<TEvent> evnts, EventArgs args) where TEvent : BaseEvent
    {
        if (!_subscriptions.ContainsKey(typeof(TEvent)))
        {
            return;
        }

        foreach (var subscription in _subscriptions[typeof(TEvent)])
        {
            foreach (var evnt in evnts)
            {
                await subscription.Publish(evnt, args);
            }
        }
    }

    public void UnSubscribe<TEvent>(SubsToken subsToken) where TEvent : BaseEvent
    {
        var subscription = _subscriptions[typeof(TEvent)]
            .FirstOrDefault(x => x.SubsToken.SubscriptionId == subsToken.SubscriptionId);
        if (subscription != null)
            _subscriptions[typeof(TEvent)].Remove(subscription);
    }

    public bool HasSubscription<TEvent>(SubsToken subsToken) where TEvent : BaseEvent
    {
        var key = typeof(TEvent);
        return _subscriptions.ContainsKey(key) && _subscriptions[key].Any(x => x.SubsToken.SubscriptionId == subsToken.SubscriptionId);
    }

    public bool HasSubscriptions<TEvent>() where TEvent : BaseEvent
    {
        var key = typeof(TEvent);
        return _subscriptions.ContainsKey(key) && _subscriptions[key].Any();
    }
}