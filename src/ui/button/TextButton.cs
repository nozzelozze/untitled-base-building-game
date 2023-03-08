using System;
using SFML.Graphics;
using SFML.System;

public class TextButton : Button
{

    public static int textButtonMarginW = 25;
    public static int textButtonMarginH = 25;
    
    public TextButton(string title, Action onClick, Vector2f ? position = null)
    : base(title, onClick, position)
    {
        
        FloatRect textRect = titleText.GetLocalBounds();
        baseRect.Size = new Vector2f(textRect.Width + textButtonMarginW, textRect.Height + textButtonMarginH);
        centerText(titleText, new Vector2f(Position.X+this.baseRect.Size.X/2, Position.Y+this.baseRect.Size.Y/2));
    }

    public override void render()
    {
        base.render();
    }
}