using System;
using SFML.Graphics;
using SFML.System;

public abstract class GUIElement : Transformable
{
    public RectangleShape BaseRect;
    protected bool IsTransparent;

    private Vector2f PositionRelativeTo;
    private Func<Vector2f> ? GetRelativeTo;
    private Vector2f LocalPosition;
    // Tranformable.Position is 'GlobalPosition'
    public Vector2f ElementPosition
    {
        get { return Position; }
        set { LocalPosition = value; }
    }

    protected List<GUIElement> ChildGUIElements = new List<GUIElement>();

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
        GetRelativeTo = config.GetRelativeTo;
        UpdateRelativeTo();

        GUIManager.AddGUIElement(this);
    }

    public IReadOnlyList<GUIElement> GetChildGUIElements()
    {
        return ChildGUIElements.AsReadOnly();
    }

    public void UpdateRelativeTo()
    {
        if (GetRelativeTo != null)
        {
            Position = LocalPosition + GetRelativeTo();
        } else
        {
            Position = LocalPosition + PositionRelativeTo;
        }
    }

    public virtual void Update()
    {
        UpdateRelativeTo();
        BaseRect.Position = Position;
        RenderQueue.QueueGUI(BaseRect);
    }

}
