using JLifeEngine.Core.Infrastructure;

namespace JLifeEngine.Core.Life.Entities;

public class Being
{
    public BeingId Id { get; set; }
    public List<Need> Needs { get; set; }
    public List<Trait> Traits { get; set; }
    public List<Relationship> Relationships { get; set; }
    public List<Memory> Memories { get; set; }
    public Action? CurrentAction { get; set; }
    public Location Location { get; set; }
    public DateTime LastUpdate { get; set; }
}

public class Location
{
}