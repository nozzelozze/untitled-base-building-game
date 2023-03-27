using System;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

public abstract class Button : InteractiveGUIElement
{

    public enum ButtonState
    {
        Normal,
        Hovered,
        Pressed,
        Disabled
    }

    public ButtonState State { get; protected set; } = ButtonState.Normal;

    public event Action ButtonClicked;

    public Button(GUIElementConfig config, Action OnClick)
    : base(config)
    {
        ButtonClicked += OnClick;
    }

    public override void OnMousePressed(Mouse.Button button)
    {
        if (button == Mouse.Button.Left && IsMouseOver)
        {
            State = ButtonState.Pressed;
        }
    }

    public override void OnMouseReleased(Mouse.Button button)
    {
        if (button == Mouse.Button.Left && IsMouseOver)
        {
            ButtonClicked.Invoke();
            State = ButtonState.Hovered;
        }
    }

    public override void OnMouseEnter()
    {
        State = ButtonState.Hovered;
    }

    public override void OnMouseLeave()
    {
        State = ButtonState.Normal;
    }

    protected virtual void UpdateAppearance()
    {
        switch(State)
        {
            case ButtonState.Normal:
                BaseRect.FillColor = Style.BackgroundColor;
                break;
            case ButtonState.Hovered:
                BaseRect.FillColor = Style.BackgroundColor + new Color(30, 30, 30); // Darken the background color
                break;
            case ButtonState.Pressed:
                BaseRect.FillColor = Style.BackgroundColor - new Color(30, 30, 30); // Lighten the background color
                break;
            case ButtonState.Disabled:
                BaseRect.FillColor = Style.BackgroundColor - new Color(60, 60, 60); // Lighten the background color even more
                break;
        }
    }

    public override void Update()
    {
        base.Update();
        UpdateAppearance();
    }

}