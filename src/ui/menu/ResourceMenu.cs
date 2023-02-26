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

        addItem(new TextButton(
            "Mine",
            () => Colonist.pullColonist(1).mineResource(resource)
        ));

        Margin = 45;
        marginOffsetY = barRectSizeY/2;
    }

}