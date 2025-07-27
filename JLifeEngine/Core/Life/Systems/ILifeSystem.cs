namespace JLifeEngine.Core.Life.Systems;

public interface ILifeSystem
{
    void Update(TimeSpan deltaTime, World world);
}
