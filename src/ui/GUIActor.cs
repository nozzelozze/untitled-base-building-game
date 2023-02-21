using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

public class GUIActor : Transformable
{
    public FloatRect collisionRect;
    public RectangleShape baseRect = new RectangleShape();

    public static int outlineThickness = 2;
    public static Color outlineColor = GUIColor.darkColor;

    public bool isTransparent;

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

    public GUIActor(Vector2f size, Vector2f ? position = null, bool isTransparent = false, bool hasOutline = true)
    {
        collisionRect.Width = size.X;
        collisionRect.Height = size.Y;
        baseRect.Size = size;
        Position = position ?? new Vector2f(0, 0);
        baseRect.FillColor = !isTransparent ? GUIColor.greyColor : Color.Transparent;
        if (hasOutline)
        {
            baseRect.OutlineThickness = outlineThickness;
            baseRect.OutlineColor = outlineColor;
        }
        this.isTransparent = isTransparent;
    }

    public Vector2f getSize()
    {
        return baseRect.Size;
    }

    public virtual void render()
    {
        baseRect.Position = Position;
        collisionRect.Top = Position.Y;
        collisionRect.Left = Position.X;
        collisionRect.Width = baseRect.Size.X;
        collisionRect.Height = baseRect.Size.Y;
        Vector2i mousePosition = PlayerMouse.getPosition();
        if (collisionRect.Contains(mousePosition.X, mousePosition.Y))
            PlayerMouse.onUI = true;
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