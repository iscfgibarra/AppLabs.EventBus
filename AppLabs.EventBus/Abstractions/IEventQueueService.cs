namespace AppLabs.EventBus.Abstractions;

public interface IEventQueueService<TEvent> where TEvent : BaseEvent
{
    /// <summary>
    /// Encolar un evento.
    /// </summary>
    /// <param name="evt"></param>
    void PushEvent(TEvent evt);
    /// <summary>
    /// Encolar una lista de eventos.
    /// </summary>
    /// <param name="list"></param>
    public void PushEvents(IEnumerable<TEvent> list);
    /// <summary>
    /// Obtener el listado de eventos de tipo TEvent.
    /// </summary>
    /// <returns></returns>
    List<TEvent> PullEvents();
}