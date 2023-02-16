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

    public Button(string title, Action onClick, Vector2f ? position = null, bool isTransparent=false, int sizeX = baseButtonSizeX, int sizeY = baseButtonSizeY)
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
        centerText(titleText, new Vector2f(Position.X+this.baseRect.Size.X/2, Position.Y+this.baseRect.Size.Y/2));
    }

    public override void render()
    {
        base.render();
        centerText(titleText, new Vector2f(Position.X+this.baseRect.Size.X/2, Position.Y+this.baseRect.Size.Y/2));
        RenderQueue.queueGUI(titleText);
        if (Input.events.Contains(Mouse.Button.Left) && Player.currentState == PlayerState.IdleState.IdleInstance)
        {
            Vector2i mousePosition = PlayerMouse.getPosition();
            if (collisionRect.Contains(mousePosition.X, mousePosition.Y))
            {
                buttonClicked.Invoke();
            }
        }
    }

}