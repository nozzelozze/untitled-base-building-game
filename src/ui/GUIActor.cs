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
        FloatRect textRect = textToCenter.GetGlobalBounds();
        //textToCenter.Origin = new Vector2f(
        //    textRect.Left + textRect.Width/2.0f,
        //    textRect.Top  + textRect.Height/2.0f);
        Vector2f center = new Vector2f(textRect.Width, textRect.Height) / 2f;
        Vector2f localBounds = center + new Vector2f(textToCenter.GetLocalBounds().Left/2, textToCenter.GetLocalBounds().Top/2);
        textToCenter.Origin = localBounds;
        textToCenter.Position = new Vector2f(centerPosition.X,centerPosition.Y);
    }
}