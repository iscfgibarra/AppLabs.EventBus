namespace AppLabs.EventBus.Test.Events;

public class DbEventArgs: EventArgs
{
    public DateTime PublishTime { get; set; } = DateTime.Now;

    public string OutputFilePath { get; set; } = String.Empty;
}