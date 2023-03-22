using System;
using SFML.Graphics;
using SFML.System;

public abstract class GUIElement : Transformable
{
    public RectangleShape BaseRect;
    protected bool IsTransparent;

    protected StyleManager Style;

    public GUIElement(Vector2f startPosition, StyleManager? style = null)
    {
        Position = startPosition;
        BaseRect = new RectangleShape();
        this.Style = style ?? StyleManager.DefaultStyle;
        BaseRect.Size = new Vector2f(500, 500);
        BaseRect.FillColor = Style.BackgroundColor;
        BaseRect.OutlineColor = Style.OutlineColor;
        BaseRect.OutlineThickness = Style.OutlineThickness;

        GUIManager.AddGUIElement(this);
    }

    public virtual void Update()
    {
        BaseRect.Position = Position;
        RenderQueue.QueueGUI(BaseRect);
    }

}
