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

    public DefaultInterface defaultInterface;

    public Player(Camera camera)
    {
        this.camera = camera;
        defaultInterface = new DefaultInterface(this);
    }

    public void enterNewState(PlayerState newState)
    {
        currentState.leave();
        currentState = newState;
    }

    public void updatePlayer(RenderWindow renderWindow)
    {
        mouse.renderCrosshair(renderWindow);
        if (Input.events.Contains(Keyboard.Key.Enter))
        {
            enterNewState(PlayerState.IdleState.IdleInstance);
        }
        if (Input.events.Contains(Mouse.Button.Left))
        {
            currentState.onPlayerClick(this);
        }
        currentState.update(this);
        defaultInterface.update();
    }
}
