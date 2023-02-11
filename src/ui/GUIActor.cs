using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

public class GUIActor : Transformable
{
    public FloatRect collisionRect;
    public RectangleShape baseRect = new RectangleShape();
    public Vector2f size;

    public static int outlineThickness = 2;
    public static Color outlineColor = GUIColor.darkColor;

    public enum characterSize
    {
        Info,
        HeadingSmall,
        HeadingLarge
    }

    public static Dictionary<characterSize, uint> characterSizes = new Dictionary<characterSize, uint>
    {
        {characterSize.Info, 15},
        {characterSize.HeadingSmall, 20},
        {characterSize.HeadingLarge, 50}
    };

    public static uint getCharacterSize(characterSize size)
    {
        return characterSizes[size];
    }

    public GUIActor(Vector2f size, Vector2f position, bool isTransparent = false, bool hasOutline = true)
    {
        collisionRect.Width = size.X;
        collisionRect.Height = size.Y;
        baseRect.Size = size;
        Position = position;
        baseRect.FillColor = !isTransparent ? GUIColor.greyColor : Color.Transparent;
        if (hasOutline)
        {
            baseRect.OutlineThickness = outlineThickness;
            baseRect.OutlineColor = outlineColor;
        }
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
            textRect.Top + textRect.Height/2.0f
        );
        textToCenter.Position = centerPosition;
    }
}