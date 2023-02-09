using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

class GUIActor : Transformable
{
    public FloatRect collisionRect;
    public RectangleShape baseRect = new RectangleShape();
    public Vector2f size;

    public static int outlineThickness = 5;
    public static Color outlineColor = GUIColor.darkColor;

    public GUIActor(Vector2f size, Vector2f position)
    {
        collisionRect.Width = size.X;
        collisionRect.Height = size.Y;
        baseRect.Size = size;
        Position = position;
        baseRect.FillColor = GUIColor.greyColor;
        baseRect.OutlineThickness = outlineThickness;
        baseRect.OutlineColor = outlineColor;
    }

    public virtual void render()
    {
        baseRect.Position = Position;
        collisionRect.Top = Position.Y;
        collisionRect.Left = Position.X;
        RenderQueue.queueGUI(baseRect);
    }

    public void centerText(Text textToCenter, Vector2f centerPosition)
    {
        FloatRect textRect = textToCenter.GetLocalBounds();
        textToCenter.Origin = new Vector2f(
            textRect.Left + textRect.Width/2.0f,
            textRect.Top  + textRect.Height/2.0f);
        textToCenter.Position = new Vector2f(centerPosition.X/2.0f,centerPosition.Y/2.0f);
    }

}