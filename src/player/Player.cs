using System;
using System.Numerics;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

class Player
{

    PlayerMouse mouse = new PlayerMouse();
    PlayerState currentState;
    Camera camera;

    public Player(Camera camera)
    {
        this.camera = camera;
        currentState = PlayerState.IdleState;
    }

    public void enterNewState(PlayerState newState)
    {
        currentState = newState;
    }

    public void updatePlayer(List<object> events)
    {
        mouse.renderCrosshair();
        if (events.Contains(Keyboard.Key.Space))
        {
            enterBuildState();
        }
    }

    public void placeObject()
    {
        Structure newStructure = new Structure(mouse.getTileFromMouse());
        Map.Instance.structures.Add(newStructure);
    }

}
