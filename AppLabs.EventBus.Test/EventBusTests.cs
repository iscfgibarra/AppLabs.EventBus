using System.Reflection;
using AppLabs.EventBus.Abstractions;
using AppLabs.EventBus.Test.Events;
using Xunit.Abstractions;

namespace AppLabs.EventBus.Test;

public class EventBusTests
{
    private readonly IEventBus _eventbus;
    private readonly ITestOutputHelper _testOutput;
    private readonly string? _outputFilePath;


    public EventBusTests(ITestOutputHelper outputHelper)
    {
        _eventbus = new EventBus();
        _testOutput = outputHelper;
        _outputFilePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location.Replace("bin\\Debug\\net6.0", string.Empty));
    }

    [Fact]
    public void EventBus_PushEvent_Ok()
    {
        var consoleHdlr = new ConsoleEventHandler();
        var fileHdlr = new FileLogEventHandler();

        var callback = new Callback
        {
            OutputHelper = _testOutput
        };

        var consoleHdlrToken = _eventbus.Subscribe<DbEvent>(consoleHdlr.GetEventHandler<DbEvent>(callback));
        var fileHdlrToken = _eventbus.Subscribe<DbEvent>(fileHdlr.GetEventHandler<DbEvent>(callback));

        var evt = new DbEvent
        {
            Query = "SELECT * from tabla"
        };

        var evtArgs = new DbEventArgs
        {
            OutputFilePath = $"{_outputFilePath}\\eventOutput.txt"
        };

        _eventbus.Publish(evt, evtArgs);

        _eventbus.UnSubscribe<DbEvent>(consoleHdlrToken);

        Assert.False(_eventbus.HasSubscription<DbEvent>(consoleHdlrToken));
        Assert.True(_eventbus.HasSubscription<DbEvent>(fileHdlrToken));
    }

    [Fact]
    public void EventBus_PushEvents_Ok()
    {
        var consoleHdlr = new ConsoleEventHandler();
        var fileHdlr = new FileLogEventHandler();

        var callback = new Callback
        {
            OutputHelper = _testOutput
        };

        var consoleHdlrToken = _eventbus.Subscribe<DbEvent>(consoleHdlr.GetEventHandler<DbEvent>(callback));
        var fileHdlrToken = _eventbus.Subscribe<DbEvent>(fileHdlr.GetEventHandler<DbEvent>(callback));

        var evt0 = new DbEvent
        {
            Query = "SELECT * from tabla"
        };

        var evt1 = new DbEvent
        {
            Query = "INSERT INTO tabla"
        };

        var evt2 = new DbEvent
        {
            Query = "UPDATE FROM tabla SET columna=valor"
        };

        var evtArgs = new DbEventArgs
        {
            OutputFilePath = $"{_outputFilePath}\\eventOutput.txt"
        };

        _eventbus.Publish(new List<DbEvent> { evt0, evt1, evt2 }, evtArgs);

        _eventbus.UnSubscribe<DbEvent>(consoleHdlrToken);

        Assert.False(_eventbus.HasSubscription<DbEvent>(consoleHdlrToken));
        Assert.True(_eventbus.HasSubscription<DbEvent>(fileHdlrToken));
    }
    
    [Fact]
public void EventBusPushEventWithNoSubscribersOk()
    {
        var evt = new DbEvent
        {
            Query = "SELECT * from tabla"
        };

        var evtArgs = new DbEventArgs
        {
            OutputFilePath = $"{_outputFilePath}\\eventOutput.txt"
        };

        _eventbus.Publish(evt, evtArgs);
        
        Assert.False(_eventbus.HasSubscriptions<DbEvent>());
    }

    [Fact]
    public void EventBusPushEventsWithNoSubscribersOk()
    {
        var evt0 = new DbEvent
        {
            Query = "SELECT * from tabla"
        };

        var evt1 = new DbEvent
        {
            Query = "INSERT INTO tabla"
        };

        var evt2 = new DbEvent
        {
            Query = "UPDATE FROM tabla SET columna=valor"
        };

        var evtArgs = new DbEventArgs
        {
            OutputFilePath = $"{_outputFilePath}\\eventOutput.txt"
        };

        _eventbus.Publish(new List<DbEvent> { evt0, evt1, evt2 }, evtArgs);

        Assert.False(_eventbus.HasSubscriptions<DbEvent>());
    }

    [Fact]
    public void SimpleSubscribeEventTest()
    {
        var callEvent = false;
        var evt0 = new DbEvent
        {
            Query = "SELECT * from tabla"
        };
        
        _eventbus.Subscribe<DbEvent>( async (DbEvent sender, EventArgs e) =>
        {
            callEvent = true;
            _testOutput.WriteLine(sender.Query);
        });

        _eventbus.Publish(evt0, new EventArgs
        {

        });
        
        Assert.True(callEvent);
    }
}