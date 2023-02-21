using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

public class Button : GUIActor
{

    public Text titleText;

    public event Action buttonClicked;

    public const int baseButtonSizeX = 100;
    public const int baseButtonSizeY = 50;

    public Func<Vector2f> textCenter;

    public Button(string title, Action onClick, Vector2f ? position = null, 
    bool isTransparent=false, int sizeX = baseButtonSizeX, 
    int sizeY = baseButtonSizeY)
        : base(
        new Vector2f(sizeX, sizeY), 
        position, 
        isTransparent,
        true
        )
    {

        buttonClicked += onClick;

        titleText = new Text(title, ResourceLoader.fetchFont("default"));
        titleText.FillColor = GUIColor.textColor;
        textCenter = () => new Vector2f(Position.X+this.baseRect.Size.X/2, Position.Y+this.baseRect.Size.Y/2);
    }

    public override void render()
    {
        base.render();
        centerText(titleText, textCenter());
        RenderQueue.queueGUI(titleText);
        Vector2i mousePosition = PlayerMouse.getPosition();
        if (collisionRect.Contains(mousePosition.X, mousePosition.Y))
        {
            if (!isTransparent) baseRect.FillColor = GUIColor.greyColor + new Color(15, 15, 15);
            if (Input.events.Contains(Mouse.Button.Left) && Player.currentState == PlayerState.IdleState.IdleInstance)
            {
                buttonClicked.Invoke();
                Input.events.Remove(Mouse.Button.Left);
            }
        } else
        {
            if (!isTransparent) baseRect.FillColor = GUIColor.greyColor;
        }
    }

}