using JLifeEngine.Core.Life;
using JLifeEngine.Core.Life.Systems;
using JLifeEngine.Core.Persistence;
using JLifeEngine.Core.Presentation;

namespace JLifeEngine.Core.Infrastructure;

public class LifeEngine
{
    private readonly List<ILifeSystem> _lifeSystems = new();
    private readonly TerminalRenderer _renderer;
    private readonly SoundEngine _soundEngine;
    private readonly WorldSaveManager _saveManager;
    private bool _isRunning;
    private World _world;
    private ViewMode _currentViewMode;

    public async Task RunAsync()
    {
        var lastUpdate = DateTime.UtcNow;
        
        while (_isRunning)
        {
            var now = DateTime.UtcNow;
            var deltaTime = now - lastUpdate;
            
            // Update all life systems
            foreach (var system in _lifeSystems)
            {
                system.Update(deltaTime, _world);
            }
            
            // Render current state
            _renderer.RenderWorld(_world, _currentViewMode);
            
            // Handle input
            await HandleInputAsync();
            
            lastUpdate = now;
            await Task.Delay(16); // ~60 FPS
        }
    }

    private async Task HandleInputAsync()
    {
        throw new NotImplementedException();
    }
}

internal class ViewMode
{
}