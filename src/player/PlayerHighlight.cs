using System;
using SFML.Graphics;
using SFML.System;

public class PlayerHighlight
{

    public Action ? Maximize;
    public Action ? Minimize;

    public Action ? UpdateHighlight;

    public bool PlayerIsHighlighting;

    public RectangleShape OutlineRectangle;
    public Func<Vector2f> ? GetPosition;

    private int outlineThickness = 3;

    public PlayerHighlight()
    {
        OutlineRectangle = new RectangleShape();
        OutlineRectangle.OutlineColor = GUIColor.TextColor;
        OutlineRectangle.OutlineThickness = outlineThickness;
        OutlineRectangle.FillColor = Color.Transparent;
    }

    public void Highlight(Action newMaximize, Action newMinimize, Action ? newUpdateHighlight = null)
    {
        UpdateHighlight = newUpdateHighlight;
        if (Minimize != null) Minimize();
        Maximize = newMaximize;
        Minimize = newMinimize;
        Maximize();
        PlayerIsHighlighting = true;
    }

    public void Highlight(Action newMaximize, Action newMinimize, Func<Vector2f> rectPosition, Vector2f rectSize, Action ?  newUpdateHighlight = null)
    {
        Highlight(newMaximize, newMinimize, newUpdateHighlight);
        OutlineRectangle.Size = rectSize;
        OutlineRectangle.Position = rectPosition();
        GetPosition = rectPosition;
    }

    public void Unhighlight()
    {
        if (Minimize != null) Minimize();
        UpdateHighlight = null;
        PlayerIsHighlighting = false;
        GetPosition = null;
    }

    public void Update()
    {
        if (UpdateHighlight != null) UpdateHighlight();
        if (GetPosition != null) OutlineRectangle.Position = GetPosition();
        if (PlayerIsHighlighting) RenderQueue.Queue(OutlineRectangle);
    }
}
