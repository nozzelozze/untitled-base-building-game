using System;
using SFML.Graphics;
using SFML.System;

public class GUIActor : Transformable
{
    public FloatRect CollisionRect;
    public RectangleShape BaseRect = new RectangleShape();

    public static int OutlineThickness = 2;
    public static Color OutlineColor = GUIColor.DarkColor;

    public bool IsTransparent;

    public enum CharacterSize
    {
        Info,
        HeadingSmall,
        HeadingLarge
    }

    public static Dictionary<CharacterSize, uint> CharacterSizes = new Dictionary<CharacterSize, uint>
    {
        {CharacterSize.Info, 15},
        {CharacterSize.HeadingSmall, 20},
        {CharacterSize.HeadingLarge, 50}
    };

    public static uint GetCharacterSize(CharacterSize size)
    {
        return CharacterSizes[size];
    }

    public GUIActor(Vector2f size, Vector2f? position = null, bool isTransparent = false, bool hasOutline = true)
    {
        CollisionRect.Width = size.X;
        CollisionRect.Height = size.Y;
        BaseRect.Size = size;
        Position = position ?? new Vector2f(0, 0);
        BaseRect.FillColor = !isTransparent ? GUIColor.GreyColor : Color.Transparent;
        if (hasOutline)
        {
            BaseRect.OutlineThickness = OutlineThickness;
            BaseRect.OutlineColor = OutlineColor;
        }
        IsTransparent = isTransparent;
    }

    public Vector2f GetSize()
    {
        return BaseRect.Size;
    }

    public virtual void Render()
    {
        BaseRect.Position = Position;
        CollisionRect.Top = Position.Y;
        CollisionRect.Left = Position.X;
        CollisionRect.Width = BaseRect.Size.X;
        CollisionRect.Height = BaseRect.Size.Y;
        Vector2i mousePosition = PlayerMouse.GetPosition();
        if (CollisionRect.Contains(mousePosition.X, mousePosition.Y))
            PlayerMouse.OnUI = true;
        RenderQueue.QueueGUI(BaseRect);
    }

    public void CenterText(Text textToCenter, Vector2f centerPosition)
    {
        FloatRect textRect = textToCenter.GetLocalBounds();
        textToCenter.Origin = new Vector2f(
            textRect.Left + textRect.Width/2.0f,
            textRect.Top + textRect.Height/2.0f
        );
        textToCenter.Position = centerPosition;
    }
}
