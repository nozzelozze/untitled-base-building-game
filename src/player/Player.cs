using System;
using SFML.Graphics;
using SFML.Window;

public class Player
{

    public PlayerMouse PlayerMouse = new PlayerMouse();
    public static PlayerState CurrentState = PlayerState.IdleState.IdleInstance;
    Camera camera;

    public static PlayerHighlight PlayerHighlight = new PlayerHighlight();

    public DefaultInterface DefaultInterface;

    public Player(Camera camera)
    {
        this.camera = camera;
        DefaultInterface = new DefaultInterface(this);
    }

    public void EnterNewState(PlayerState newState)
    {
        CurrentState.Leave();
        CurrentState = newState;
    }

    public void UpdatePlayer(RenderWindow renderWindow)
    {
        PlayerMouse.RenderCrosshair(renderWindow);
        DefaultInterface.Update();
        CurrentState.Update(this);
        PlayerHighlight.Update();
        if (Input.Events.Contains(Mouse.Button.Left))
        {
            CurrentState.OnPlayerClick(this);
        }
    }
}
