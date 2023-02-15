using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

public class TextButton : Button
{

    public static int textButtonMarginW = 75;
    public static int textButtonMarginH = 125;

    public TextButton(string title, Vector2f position, Action onClick)
    : base(title, position, onClick)
    {
        
        FloatRect textRect = titleText.GetLocalBounds();
        size.X = textRect.Width + textButtonMarginW;
        size.Y = textRect.Height + textButtonMarginH;
        centerText(titleText, new Vector2f(Position.X+this.baseRect.Size.X/2, Position.Y+this.baseRect.Size.Y/2));
    }

    public override void render()
    {
        base.render();
    }
}