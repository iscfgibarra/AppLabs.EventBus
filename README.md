# AppLabs Event Bus

Implements a simple event bus for use in NET applications.

## Usage

### Creating an event bus

To create an event bus, you can use the `EventBus` class. 

```csharp
using AppLabs.EventBus;

var eventBus = new EventBus();
```

### Subscribing to events

To subscribe to an event, you can use the `Subscribe` method. 

```csharp
using AppLabs.EventBus;

//Event inherited from BaseEvent required
public class DbEvent : BaseEvent
{
    public string Query { get; set; } = string.Empty;
}

var eventBus = new EventBus();

var subsToken = _eventbus.Subscribe<DbEvent>( async (DbEvent sender, EventArgs e) =>
{
    //Do something with the event
     Console.WriteLine(sender.Query);
});      
```

### Publishing events

To publish an event, you can use the `Publish` method.

```csharp

//Suppose we have a DbEvent and we want to publish it
varevt0 = new DbEvent
   {
        Query = "SELECT * from table"
   };
   
   //EventArgs will be a custom EventArgs     
  _eventbus.Publish(evt0, new EventArgs();
```

### Unsubscribing from events

To unsubscribe from an event, you can use the `Unsubscribe` method.

```csharp
//subsToken is the token returned by the Subscribe method
eventBus.Unsubscribe(subsToken);
```
