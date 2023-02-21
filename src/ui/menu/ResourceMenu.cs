using System;
using System.Numerics;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

public class ResourceMenu : Menu
{

    public ResourceMenu(string title, Resource resource) 
    : base(title, Camera.camPositionToWin(resource.position))
    {
        Vector2f menuOffset = new Vector2f(Map.tileSize*1.2f, Map.tileSize*1.2f);
        Position += menuOffset;
        
        Pathfinding pathfinding = new Pathfinding();

        addItem(new TextButton(
            "Mine",
            () => pathfinding.getPathTiles(Map.Instance.tiles[10, 10], Map.Instance.getTileAt(resource.position))
        ));

        Margin = 45;
        marginOffsetY = barRectSizeY/2;
    }

}