namespace JLifeEngine.Core.Infrastructure;

public class EventBus
{
    public event Action<LifeEvent> LifeEventOccurred;
    
    public void PublishEvent(LifeEvent evt)
    {
        LifeEventOccurred?.Invoke(evt);
        // Log to history, trigger sound effects, update UI
    }
}