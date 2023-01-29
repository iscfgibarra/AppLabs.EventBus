namespace AppLabs.EventBus.Test.Events;

public class ConsoleEventHandler
{
    public Func<object, EventArgs, Task> GetEventHandler<T>(Callback callback) where T : DbEvent
    {
        return async (object sender, EventArgs e) =>
        {
            try
            {
                var evt = (T)sender;
                var a = (DbEventArgs)e;

                callback.OutputHelper.WriteLine($"Publish in {a.PublishTime} [{evt.EventType.ToString()}] : {evt.Query}");
            }
            catch (Exception ex)
            {
                callback.OutputHelper.WriteLine($"Failed to call service for id {((T)sender).EventId}, {ex.Message}");
            }
        };
    }
}