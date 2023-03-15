using System;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

public class Button : GUIActor
{

    public Text TitleText;

    public event Action ButtonClicked;

    public const int BaseButtonSizeX = 100;
    public const int BaseButtonSizeY = 50;

    public Func<Vector2f> TextCenter;

    public Button(string title, Action onClick, Vector2f? position = null,
        bool isTransparent = false, int sizeX = BaseButtonSizeX,
        int sizeY = BaseButtonSizeY)
        : base(
        new Vector2f(sizeX, sizeY),
        position,
        isTransparent,
        true
        )
    {

        ButtonClicked += onClick;

        TitleText = new Text(title, ResourceLoader.FetchFont("default"));
        TitleText.FillColor = GUIColor.TextColor;
        TextCenter = () => new Vector2f(Position.X + this.BaseRect.Size.X / 2, Position.Y + this.BaseRect.Size.Y / 2);
    }

    public override void Render()
    {
        base.Render();
        CenterText(TitleText, TextCenter());
        RenderQueue.QueueGUI(TitleText);
        Vector2i mousePosition = PlayerMouse.GetPosition();
        if (CollisionRect.Contains(mousePosition.X, mousePosition.Y))
        {
            if (!IsTransparent) BaseRect.FillColor = GUIColor.GreyColor + new Color(15, 15, 15);
            if (Input.Events.Contains(Mouse.Button.Left) && Player.CurrentState == PlayerState.IdleState.IdleInstance)
            {
                ButtonClicked.Invoke();
                Input.Events.Remove(Mouse.Button.Left);
            }
        }
        else
        {
            if (!IsTransparent) BaseRect.FillColor = GUIColor.GreyColor;
        }
    }

}
