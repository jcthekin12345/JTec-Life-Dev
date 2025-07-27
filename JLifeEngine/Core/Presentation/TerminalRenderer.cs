namespace JLifeEngine.Core.Presentation;

public class TerminalRenderer
{
    public void RenderWorld(World world, ViewMode mode)
    {
        switch (mode)
        {
            case ViewMode.Overview:
                RenderOverview(world);
                break;
            case ViewMode.FocusedBeing:
                RenderBeingFocus(world.SelectedBeing);
                break;
            case ViewMode.Location:
                RenderLocation(world.CurrentLocation);
                break;
        }
    }

    private void RenderBeingStatus(Being being)
    {
        Console.ForegroundColor = GetBeingMoodColor(being);
        Console.WriteLine($"{being.Name}: {being.CurrentAction?.Description ?? "Idle"}");
        // Show needs, relationships, current thoughts
    }
}