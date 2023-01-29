namespace AppLabs.EventBus;

public class SubsToken
{
    public string SubscriptionId { get; set; } = Guid.NewGuid().ToString();
}