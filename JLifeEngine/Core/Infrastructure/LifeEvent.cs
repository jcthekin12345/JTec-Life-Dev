namespace JLifeEngine.Core.Infrastructure;

public class LifeEvent
{
    public DateTime Timestamp { get; set; }
    public BeingId? PrimaryBeing { get; set; }
    public BeingId? SecondaryBeing { get; set; }
    public string Description { get; set; }
    public LifeEventType Type { get; set; }
}