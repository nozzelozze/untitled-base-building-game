using System;
using SFML.Graphics;
using SFML.System;

public abstract class GUIElement : Transformable
{
    public RectangleShape BaseRect;
    protected bool IsTransparent;

    protected Vector2f PositionRelativeTo;
    protected Vector2f LocalPosition;
    // Tranformable.Position is 'GlobalPosition'

    protected StyleManager Style;

    public GUIElement(Vector2f startPosition, StyleManager? style = null, Vector2f ? size = null, Vector2f ? relativeTo = null)
    {
        BaseRect = new RectangleShape();
        this.Style = style ?? StyleManager.DefaultStyle;
        BaseRect.Size = size ?? new Vector2f();
        BaseRect.FillColor = Style.BackgroundColor;
        BaseRect.OutlineColor = Style.OutlineColor;
        BaseRect.OutlineThickness = Style.OutlineThickness;

        if (relativeTo != null)
        {
            PositionRelativeTo = new Vector2f(relativeTo.Value.X, relativeTo.Value.Y);
        } else
        {
            PositionRelativeTo = new Vector2f(0, 0);
            LocalPosition = startPosition;
        }

        GUIManager.AddGUIElement(this);
    }

    public void UpdateRelativeTo()
    {
        Position = LocalPosition + PositionRelativeTo;
    }

    public virtual void Update()
    {
        UpdateRelativeTo();
        BaseRect.Position = Position;
        RenderQueue.QueueGUI(BaseRect);
    }

}
