namespace JLifeEngine.Core.Persistence;

public class WorldSaveManager
{
    public async Task SaveWorldAsync(World world, string filePath)
    {
        var saveData = new WorldSaveData
        {
            Beings = world.Beings.ToList(),
            Locations = world.Locations.ToList(),
            GlobalEvents = world.EventHistory.ToList(),
            Timestamp = DateTime.UtcNow
        };

        var json = JsonSerializer.Serialize(saveData, _serializerOptions);
        await File.WriteAllTextAsync(filePath, json);
    }

    public async Task<World> LoadWorldAsync(string filePath)
    {
        var json = await File.ReadAllTextAsync(filePath);
        var saveData = JsonSerializer.Deserialize<WorldSaveData>(json, _serializerOptions);
        return RestoreWorld(saveData);
    }
}