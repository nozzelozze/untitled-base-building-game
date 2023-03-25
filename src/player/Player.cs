using System;
using SFML.Graphics;
using SFML.Window;

public class Player
{

    private static Player _instance;
    public static Player Instance
    {
        get
        {
            if (_instance == null)
            {
                throw new InvalidOperationException("Player instance not created yet.");
            }
            return _instance;
        }
    }

    public PlayerMouse PlayerMouse = new PlayerMouse();
    public PlayerState CurrentState = PlayerState.CreateState("Idle");
    Camera camera;

    public bool IsPanning { get; set; } = false;

    public static PlayerHighlight PlayerHighlight = new PlayerHighlight();

    public DefaultInterface DefaultInterface;

    private Player(Camera camera)
    {
        this.camera = camera;
        DefaultInterface = new DefaultInterface(this);
    }

    // Static method to initialize the Player instance
    public static void Initialize(Camera camera)
    {
        if (_instance != null)
        {
            throw new InvalidOperationException("Player instance already created.");
        }
        _instance = new Player(camera);
    }

    public void OnRightMouseButtonReleased()
    {
        if (IsPanning)
        {
            IsPanning = false;
        }
        else
        {
            EnterNewState(PlayerState.CreateState("Idle"));
        }
    }

    public void EnterNewState(PlayerState newState)
    {
        CurrentState.Leave();
        CurrentState = newState;
        CurrentState.Enter();
    }

    public void UpdatePlayer(RenderWindow renderWindow)
    {
        PlayerMouse.RenderCrosshair(renderWindow);
        DefaultInterface.Update();
        CurrentState.Update(this);
        PlayerHighlight.Update();
    }
}
