# JLifeEngine Documentation

## Table of Contents
1. [Overview](#overview)
2. [Getting Started](#getting-started)
3. [Architecture](#architecture)
4. [Core Systems](#core-systems)
5. [API Reference](#api-reference)
6. [Development Guide](#development-guide)
7. [Examples](#examples)

## Overview

JLifeEngine is a life simulation engine designed for creating rich, autonomous virtual worlds where beings live, interact, and evolve. The engine focuses on emergent storytelling through genuine artificial life rather than scripted narratives.

### Key Features
- **Autonomous Beings**: Characters with needs, personalities, and decision-making capabilities
- **Emergent Storytelling**: Stories arise naturally from being interactions
- **Rich Presentation**: Advanced terminal rendering and sound systems
- **Persistent Worlds**: Save and load complete world states
- **Modular Architecture**: Clean separation of concerns for easy extension

### Philosophy
The core principle of JLifeEngine is that **Life comes first**. Every system and feature serves the goal of creating believable, living beings that players become emotionally invested in.

## Getting Started

### Prerequisites
- .NET 8.0 or later
- Windows/Linux/macOS terminal with color support
- Audio device for sound features (optional)

### Installation

```bash
# Clone the repository
git clone https://github.com/youruser/JLifeEngine.git
cd JLifeEngine

# Build the solution
dotnet build

# Run the sample world
dotnet run --project JLifeEngine.Console
```

### Quick Start

```csharp
using JLifeEngine.Core;
using JLifeEngine.Core.Life;
using JLifeEngine.Core.Presentation;

// Create a new world
var world = new World();

// Add some beings
var alice = new Being 
{ 
    Name = "Alice",
    Needs = new List<Need> { new HungerNeed(), new SocialNeed() },
    Traits = new List<Trait> { new Trait("Friendly", 0.8f) }
};

world.AddBeing(alice);

// Create and start the engine
var engine = new LifeEngine(world);
await engine.RunAsync();
```

## Architecture

JLifeEngine follows a modular architecture with four main layers:

```
â”Œâ”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”
â”‚  Presentation   â”‚ â† Terminal rendering, sound effects
â”œâ”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”¤
â”‚      Life       â”‚ â† Beings, needs, relationships, actions
â”œâ”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”¤
â”‚   Persistence   â”‚ â† Save/load, world history
â”œâ”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”¤
â”‚ Infrastructure  â”‚ â† Game loop, events, utilities
â””â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”˜
```

### Design Principles

1. **Life-First Design**: All features serve the life simulation
2. **Emergent Behavior**: Complex stories arise from simple rules
3. **Autonomous Agency**: Beings make their own decisions
4. **Observable Life**: Rich presentation reveals the depth of simulation
5. **Persistent Growth**: Worlds evolve continuously over time

## Core Systems

### Life System

The heart of JLifeEngine where autonomous beings live and interact.

#### Beings
Every being in the world is an instance of the `Being` class:

```csharp
public class Being
{
    public BeingId Id { get; set; }
    public string Name { get; set; }
    public List<Need> Needs { get; set; }
    public List<Trait> Traits { get; set; }
    public List<Relationship> Relationships { get; set; }
    public List<Memory> Memories { get; set; }
    public Action? CurrentAction { get; set; }
    public Location Location { get; set; }
    public BeingState State { get; set; }
}
```

#### Needs System
Beings have various needs that drive their behavior:

- **HungerNeed**: Drives beings to seek food
- **SocialNeed**: Motivates social interactions
- **RestNeed**: Requires periodic rest/sleep
- **SafetyNeed**: Seeks secure environments
- **CreativityNeed**: Pursues creative activities

```csharp
public abstract class Need
{
    public float Intensity { get; set; }    // 0.0 to 1.0
    public DateTime LastSatisfied { get; set; }
    public float DecayRate { get; set; }
    
    public abstract bool ShouldActOn();
    public abstract List<ActionType> GetSatisfyingActions();
}
```

#### Action System
Beings perform actions to satisfy their needs:

```csharp
public abstract class Action
{
    public ActionId Id { get; set; }
    public BeingId Actor { get; set; }
    public TimeSpan Duration { get; set; }
    public ActionState State { get; set; }
    
    public abstract bool CanExecute(World world);
    public abstract void Execute(World world);
    public abstract void Complete(World world);
}
```

#### Relationship System
Beings form and maintain relationships with each other:

```csharp
public class Relationship
{
    public BeingId From { get; set; }
    public BeingId To { get; set; }
    public RelationshipType Type { get; set; }
    public float Strength { get; set; }     // -1.0 to 1.0
    public List<Memory> SharedMemories { get; set; }
    public DateTime LastInteraction { get; set; }
}
```

### Presentation System

#### Terminal Renderer
The `TerminalRenderer` provides rich console output with multiple view modes:

```csharp
public enum ViewMode
{
    Overview,       // World summary
    FocusedBeing,   // Detail view of one being
    Location,       // Location-based view
    Relationships,  // Social network view
    History        // Event timeline
}
```

#### Sound Engine
The `SoundEngine` provides ambient audio and event-triggered sound effects:

```csharp
public class SoundEngine
{
    public void PlayLifeEvent(LifeEvent evt);
    public void UpdateAmbient(World world);
    public void SetMasterVolume(float volume);
    public void EnableSpatialAudio(bool enabled);
}
```

### Persistence System

#### World Save Manager
Complete world state can be saved and restored:

```csharp
public class WorldSaveManager
{
    public async Task SaveWorldAsync(World world, string filePath);
    public async Task<World> LoadWorldAsync(string filePath);
    public async Task<List<SaveInfo>> GetSaveListAsync(string directory);
    public async Task CreateBackupAsync(World world, string backupPath);
}
```

### Infrastructure System

#### Life Engine
The main engine coordinates all systems:

```csharp
public class LifeEngine
{
    public async Task RunAsync();
    public void Pause();
    public void Resume();
    public void SetTimeScale(float scale);
    public void RegisterSystem(ILifeSystem system);
}
```

#### Event System
All significant events flow through the event bus:

```csharp
public class EventBus
{
    public event Action<LifeEvent> LifeEventOccurred;
    public void PublishEvent(LifeEvent evt);
    public List<LifeEvent> GetHistory(TimeSpan timeSpan);
}
```

## API Reference

### Core Classes

#### Being
Represents an autonomous entity in the world.

**Properties:**
- `Id`: Unique identifier
- `Name`: Display name
- `Needs`: List of current needs
- `Traits`: Personality traits affecting behavior
- `Relationships`: Connections to other beings
- `Memories`: Stored experiences
- `CurrentAction`: Active action (if any)
- `Location`: Current position in world

**Methods:**
- `AddNeed(Need need)`: Add a new need
- `SatisfyNeed<T>() where T : Need`: Satisfy a specific need type
- `GetStrongestNeed()`: Returns the most urgent need
- `CanInteractWith(Being other)`: Check if interaction is possible
- `FormRelationship(Being other, RelationshipType type)`: Create new relationship

#### World
Container for all world state and beings.

**Properties:**
- `Beings`: All beings in the world
- `Locations`: Available locations
- `CurrentTime`: World simulation time
- `EventHistory`: Historical events

**Methods:**
- `AddBeing(Being being)`: Add being to world
- `RemoveBeing(BeingId id)`: Remove being from world
- `GetBeingsAt(Location location)`: Get beings at specific location
- `GetNearbyBeings(Being being, float radius)`: Find beings within range
- `AdvanceTime(TimeSpan delta)`: Progress world time

#### LifeEngine
Main engine orchestrating all systems.

**Methods:**
- `RunAsync()`: Start the main game loop
- `Pause()`: Pause simulation
- `Resume()`: Resume simulation
- `SaveWorldAsync(string path)`: Save current world state
- `LoadWorldAsync(string path)`: Load world from file
- `RegisterEventHandler(Action<LifeEvent> handler)`: Subscribe to events

### Life System Interfaces

#### ILifeSystem
Base interface for all life systems.

```csharp
public interface ILifeSystem
{
    string Name { get; }
    int Priority { get; }
    void Initialize(World world);
    void Update(TimeSpan deltaTime, World world);
    void Shutdown();
}
```

#### INeedProvider
Interface for objects that can satisfy needs.

```csharp
public interface INeedProvider
{
    List<Type> SatisfiableNeeds { get; }
    bool CanSatisfy(Need need, Being being);
    void Satisfy(Need need, Being being);
}
```

### Events

#### LifeEvent
Base class for all life events.

**Properties:**
- `Timestamp`: When the event occurred
- `Type`: Classification of the event
- `PrimaryBeing`: Main actor (if applicable)
- `SecondaryBeing`: Secondary actor (if applicable)
- `Description`: Human-readable description
- `Data`: Additional event-specific data

**Event Types:**
- `BeingBorn`: New being enters the world
- `BeingDied`: Being leaves the world
- `NeedSatisfied`: A need was fulfilled
- `ActionStarted`: Being begins new action
- `ActionCompleted`: Being finishes action
- `RelationshipFormed`: New relationship created
- `RelationshipChanged`: Existing relationship modified
- `InteractionOccurred`: Beings interacted
- `LocationChanged`: Being moved to new location

## Development Guide

### Adding New Need Types

1. Create a class inheriting from `Need`:

```csharp
public class CreativityNeed : Need
{
    public override bool ShouldActOn()
    {
        return Intensity > 0.7f;
    }
    
    public override List<ActionType> GetSatisfyingActions()
    {
        return new List<ActionType> 
        { 
            ActionType.Paint, 
            ActionType.Write, 
            ActionType.Sing 
        };
    }
}
```

2. Register the need with the `NeedSystem`.

3. Create actions that can satisfy the need.

### Adding New Action Types

1. Create a class inheriting from `Action`:

```csharp
public class PaintAction : Action
{
    public override bool CanExecute(World world)
    {
        // Check if being has access to painting supplies
        return Actor.Location.HasItem(ItemType.PaintSupplies);
    }
    
    public override void Execute(World world)
    {
        // Perform painting behavior
        var being = world.GetBeing(Actor);
        being.SatisfyNeed<CreativityNeed>();
        
        // Publish event
        world.EventBus.PublishEvent(new LifeEvent
        {
            Type = LifeEventType.ActionCompleted,
            PrimaryBeing = Actor,
            Description = $"{being.Name} painted a beautiful picture"
        });
    }
}
```

### Custom Life Systems

1. Implement `ILifeSystem`:

```csharp
public class WeatherSystem : ILifeSystem
{
    public string Name => "Weather";
    public int Priority => 10;
    
    public void Initialize(World world)
    {
        // Setup initial weather state
    }
    
    public void Update(TimeSpan deltaTime, World world)
    {
        // Update weather conditions
        // Affect being behavior based on weather
    }
    
    public void Shutdown()
    {
        // Cleanup resources
    }
}
```

2. Register with the engine:

```csharp
engine.RegisterSystem(new WeatherSystem());
```

### Testing Life Systems

Use the built-in testing framework:

```csharp
[Test]
public void HungerNeed_ShouldTriggerEatingAction()
{
    // Arrange
    var world = new World();
    var being = new Being();
    being.AddNeed(new HungerNeed { Intensity = 0.9f });
    world.AddBeing(being);
    
    var needSystem = new NeedSystem();
    var actionSystem = new ActionSystem();
    
    // Act
    needSystem.Update(TimeSpan.FromMinutes(1), world);
    actionSystem.Update(TimeSpan.FromMinutes(1), world);
    
    // Assert
    Assert.IsInstanceOf<EatAction>(being.CurrentAction);
}
```

## Examples

### Basic World Setup

```csharp
// Create world with two beings
var world = new World();

var alice = new Being
{
    Name = "Alice",
    Needs = new List<Need> 
    { 
        new HungerNeed(),
        new SocialNeed(),
        new RestNeed()
    },
    Traits = new List<Trait>
    {
        new Trait("Extroverted", 0.8f),
        new Trait("Creative", 0.6f)
    }
};

var bob = new Being
{
    Name = "Bob", 
    Needs = new List<Need>
    {
        new HungerNeed(),
        new SocialNeed { Intensity = 0.9f }
    },
    Traits = new List<Trait>
    {
        new Trait("Introverted", 0.7f),
        new Trait("Analytical", 0.9f)
    }
};

world.AddBeing(alice);
world.AddBeing(bob);

// Create engine and run
var engine = new LifeEngine(world);
await engine.RunAsync();
```

### Custom Event Handling

```csharp
engine.EventBus.LifeEventOccurred += (evt) =>
{
    switch (evt.Type)
    {
        case LifeEventType.RelationshipFormed:
            Console.WriteLine($"New relationship formed: {evt.Description}");
            PlaySound("relationship_formed.wav");
            break;
            
        case LifeEventType.BeingDied:
            Console.WriteLine($"Tragic loss: {evt.Description}");
            ShowMemorialScreen(evt.PrimaryBeing);
            break;
    }
};
```

### Advanced World Configuration

```csharp
var config = new WorldConfiguration
{
    MaxBeings = 50,
    TimeScale = 1.0f,
    NeedDecayRate = 0.1f,
    RelationshipFormationRate = 0.05f,
    EnableAging = true,
    EnableDeath = true,
    RandomEventFrequency = 0.02f
};

var world = new World(config);

// Add locations
world.AddLocation(new Location("Town Square") 
{ 
    Type = LocationType.Social,
    Capacity = 20,
    Features = { LocationFeature.FoodSource, LocationFeature.Shelter }
});

world.AddLocation(new Location("Forest")
{
    Type = LocationType.Nature,
    Capacity = 10,
    Features = { LocationFeature.Resources, LocationFeature.Peaceful }
});
```

### Save System Usage

```csharp
// Auto-save every 5 minutes
var saveTimer = new Timer(async _ =>
{
    await engine.SaveWorldAsync($"autosave_{DateTime.Now:yyyyMMdd_HHmmss}.json");
}, null, TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(5));

// Load from specific save
if (File.Exists("my_world.json"))
{
    var world = await engine.LoadWorldAsync("my_world.json");
    Console.WriteLine($"Loaded world with {world.Beings.Count} beings");
}
```

### Performance Monitoring

```csharp
engine.PerformanceMonitor.SystemUpdated += (systemName, updateTime) =>
{
    if (updateTime > TimeSpan.FromMilliseconds(16))
    {
        Console.WriteLine($"Warning: {systemName} took {updateTime.TotalMilliseconds}ms");
    }
};

// Get performance stats
var stats = engine.GetPerformanceStats();
Console.WriteLine($"Average FPS: {stats.AverageFPS:F1}");
Console.WriteLine($"Memory Usage: {stats.MemoryUsageMB:F1} MB");
```

---

*JLifeEngine - Where artificial life comes alive*
