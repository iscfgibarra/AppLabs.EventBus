namespace AppLabs.EventBus.Abstractions;

public interface ISubscription
{
    SubsToken SubsToken { get;}
    Task Publish(BaseEvent evnt, EventArgs args);
}