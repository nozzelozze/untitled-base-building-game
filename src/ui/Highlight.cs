using System;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

public class Highlight
{
    RectangleShape highlightRect;

    public Highlight(Vector2f size, Vector2f position)
    {
        highlightRect = new RectangleShape(size);
        highlightRect.Position = position;
        highlightRect.OutlineColor = GUIColor.textColor;
        highlightRect.OutlineThickness = GUIActor.outlineThickness;
        highlightRect.FillColor = Color.Transparent;
    }

    public void render()
    {
        RenderQueue.queue(highlightRect);
    }

    public void setPosition(Vector2f newPosition) { highlightRect.Position = newPosition; }
}