namespace AppLabs.EventBus.Test.Events;

public class FileLogEventHandler
{
    public Func<object, EventArgs, Task> GetEventHandler<T>(Callback callback) where T : DbEvent
    {
        return async (object? sender, EventArgs e) =>
        {
            try
            {
                var evt = (T)sender;
                var a = (DbEventArgs)e;

                using (var file = new StreamWriter(a.OutputFilePath, true))
                {
                    file.WriteLine(a.PublishTime.ToString() + " [" + evt.EventType.ToString() + "]: " + evt.Query);
                }

                callback.OutputHelper.WriteLine($"Store event in file {a.OutputFilePath} {((T)sender).EventId}");
            }
            catch (Exception ex)
            {
                callback.OutputHelper.WriteLine(
                    $"Failed to call service for orderId {((T)sender).EventId}, {ex.Message}");
            }
        };
    }
}