using System;
using System.Numerics;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

class Player
{

    PlayerMouse mouse = new PlayerMouse();
    Camera camera;

    public Player(Camera camera)
    {
        this.camera = camera;
    }

    public void updatePlayer(List<object> events)
    {
        mouse.renderCrosshair();
        if (events.Contains(Mouse.Button.Right))
        {
            placeObject();
        }
    }

    public void placeObject()
    {
        Structure newStructure = new Structure(mouse.getTileFromMouse());
        Map.Instance.structures.Add(newStructure);
    }

}
