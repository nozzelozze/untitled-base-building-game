using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

public class PlayerHighlight
{

    public Action ? maximize;
    public Action ? minimize;

    public Action ? updateHighlight;

    public bool playerIsHighlighting;

    public RectangleShape outlineRectangle;

    private int outlineThickness = 3;

    public PlayerHighlight()
    {
        outlineRectangle = new RectangleShape();
        outlineRectangle.OutlineColor = GUIColor.textColor;
        outlineRectangle.OutlineThickness = outlineThickness;
        outlineRectangle.FillColor = Color.Transparent;
    }

    public void highlight(Action newMaximize, Action newMinimize, Action ? newUpdateHighlight = null)
    {
        updateHighlight = newUpdateHighlight;
        if (minimize != null) minimize();
        maximize = newMaximize;
        minimize = newMinimize;
        maximize();
        playerIsHighlighting = true;
    }

    public void highlight(Action newMaximize, Action newMinimize, Vector2f rectPosition, Vector2f rectSize, Action ?  newUpdateHighlight = null)
    {
        highlight(newMaximize, newMinimize, newUpdateHighlight);
        outlineRectangle.Size = rectSize;
        outlineRectangle.Position = rectPosition;
    }

    public void unhightlight()
    {
        if (minimize != null) minimize();
        updateHighlight = null;
        playerIsHighlighting = false;
    }

    public void update()
    {
        if (updateHighlight != null) updateHighlight();
        if (playerIsHighlighting) RenderQueue.queue(outlineRectangle);
    }

}