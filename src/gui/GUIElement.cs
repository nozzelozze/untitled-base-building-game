using System;
using SFML.Graphics;
using SFML.System;

public abstract class GUIElement : Transformable
{
    public RectangleShape BaseRect;
    protected bool IsTransparent;

    protected Vector2f PositionRelativeTo;
    private Vector2f LocalPosition;
    // Tranformable.Position is 'GlobalPosition'
    public Vector2f ElementPosition
    {
        get { return Position; }
        set { LocalPosition = value; }
    }

    protected StyleManager Style;

    public GUIElement(GUIElementConfig config)
    {
        BaseRect = new RectangleShape();
        this.Style = config.Style ?? StyleManager.DefaultStyle;
        BaseRect.Size = config.Size ?? new Vector2f();
        BaseRect.FillColor = Style.BackgroundColor;
        BaseRect.OutlineColor = Style.OutlineColor;
        BaseRect.OutlineThickness = Style.OutlineThickness;

        if (config.RelativeTo != null)
            PositionRelativeTo = new Vector2f(config.RelativeTo.Value.X, config.RelativeTo.Value.Y);
        else
            PositionRelativeTo = new Vector2f(0, 0);
        LocalPosition = config.StartPosition;

        UpdateRelativeTo();

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
