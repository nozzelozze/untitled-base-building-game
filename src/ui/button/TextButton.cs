using System;
using SFML.Graphics;
using SFML.System;

public class TextButton : Button
{

    public static int TextButtonMarginW = 25;
    public static int TextButtonMarginH = 25;

    public TextButton(string title, Action onClick, Vector2f? position = null)
    : base(title, onClick, position)
    {

        FloatRect textRect = TitleText.GetLocalBounds();
        BaseRect.Size = new Vector2f(textRect.Width + TextButtonMarginW, textRect.Height + TextButtonMarginH);
        CenterText(TitleText, new Vector2f(Position.X + this.BaseRect.Size.X / 2, Position.Y + this.BaseRect.Size.Y / 2));
    }

    public override void Render()
    {
        base.Render();
    }
}
