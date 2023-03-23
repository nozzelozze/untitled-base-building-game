using System;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

public abstract class InteractiveGUIElement : GUIElement
{

    public bool IsMouseOver { get; set; }

    public InteractiveGUIElement(Vector2f startPosition, StyleManager ? style = null)
    : base(startPosition, style)
    {

    }

    public abstract void OnMouseEnter();
    public abstract void OnMouseLeave();
    public abstract void OnMousePressed(Mouse.Button button);
    public abstract void OnMouseReleased(Mouse.Button button);

}