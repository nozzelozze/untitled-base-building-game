using System;
using System.Numerics;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

public class Player
{

    public PlayerMouse mouse = new PlayerMouse();
    public static PlayerState currentState = PlayerState.IdleState.IdleInstance;
    Camera camera;

    public Player(Camera camera)
    {
        this.camera = camera;
    }

    public void enterNewState(PlayerState newState)
    {
        currentState.leave();
        currentState = newState;
    }

    public void updatePlayer(List<object> events)
    {
        mouse.renderCrosshair();
        if (events.Contains(Keyboard.Key.Space))
        {
            enterNewState(PlayerState.BuildState.BuildInstance);
            PlayerState.BuildState.BuildInstance.enterBuild(new Structure());
        }
        if (events.Contains(Keyboard.Key.Enter))
        {
            enterNewState(PlayerState.IdleState.IdleInstance);
        }
        if (events.Contains(Mouse.Button.Left))
        {
            currentState.onPlayerClick(this);
        }
        currentState.update(this);
    }
}
