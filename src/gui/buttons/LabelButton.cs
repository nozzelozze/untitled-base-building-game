using System;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

public class LabelButton : Button
{

    private GUIText Label { get; set; }

    public LabelButton(Vector2f position, Action onClick, string labelText, StyleManager ? style = null)
    : base (position, onClick, style)
    {
        Label = new GUIText(
            labelText,
            new Vector2f(),
            anchorPoint: GUIText.AnchorPoint.Center,
            style: style
        );
        BaseRect.Size = new Vector2f(
            Label.Text.GetGlobalBounds().Width * 2.3f,
            Label.Text.GetGlobalBounds().Height * 2.3f
        );
        Label.Position = new Vector2f(position.X+BaseRect.Size.X/2, position.Y+BaseRect.Size.Y/2);
        Label.SetAnchorPosition();
    }

    public override void Update()
    {
        base.Update();
    }

}