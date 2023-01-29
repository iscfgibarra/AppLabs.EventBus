using AppLabs.EventBus.Abstractions;
using AppLabs.EventBus.Services;
using AppLabs.EventBus.Test.Events;
using FluentAssertions;

namespace AppLabs.EventBus.Test.Services;

public class EventQueueServiceTests
{
    private readonly IEventQueueService<DbEvent> _eventQueueService;


    public EventQueueServiceTests()
    {
        _eventQueueService = new EventQueueService<DbEvent>();
    }

    [Fact]
    public void PushEvent_Test_Ok()
    {
        var evt = new DbEvent()
        {
            Description = "Database Test",
            Query = "SELECT * from table"
        };
        _eventQueueService.PushEvent(evt);

        var list = _eventQueueService.PullEvents();

        Assert.Equal(list?.Count, 1);
        list[0].Should().Be(evt);
    }

    [Fact]
    public void PullEvents_Test_Ok()
    {
        var evt1 = new DbEvent()
        {
            Description = "Database Test 1",
            Query = "SELECT * from table"
        };

        var evt2 = new DbEvent()
        {
            Description = "Database Test 2",
            Query = "SELECT * from table"
        };

        var evtList = new List<DbEvent>();
        evtList.Add(evt1);
        evtList.Add(evt2);

        _eventQueueService.PushEvents(evtList);

        var list = _eventQueueService.PullEvents();

        Assert.Equal(list?.Count, 2);
        Assert.Equal(list[0], evt1);
        Assert.Equal(list[1], evt2);

        var finalList = _eventQueueService.PullEvents();

        Assert.Empty(finalList);
    }
}