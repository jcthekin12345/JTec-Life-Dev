namespace JLifeEngine.Core.Life.Entities;

public abstract class Need
{
    public float Intensity { get; set; } // 0.0 to 1.0
    public DateTime LastSatisfied { get; set; }
    public abstract bool ShouldActOn();
}

public class HungerNeed : Need { }
public class SocialNeed : Need { }
public class RestNeed : Need { }