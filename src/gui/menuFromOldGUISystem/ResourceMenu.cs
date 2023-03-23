using System;
using SFML.System;

/* public class ResourceMenu : Menu
{

    public ResourceMenu(string title, Resource resource) 
    : base(title, Camera.CamPositionToWin(resource.Position))
    {
        Vector2f menuOffset = new Vector2f(Map.TileSize*1.2f, Map.TileSize*1.2f);
        Position += menuOffset;

        AddItem(new TextButton(
            "Mine",
            () => JobManager.AddToQueue(new MineJob(resource))
        ));

        Margin = 45;
        MarginOffsetY = BarRectSizeY/2;
    }

}
 */