using System;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

public class LabelButton : Button
{

    private GUIText Label { get; set; }

    public LabelButton(GUIElementConfig config, Action onClick, string labelText)
    : base (config, onClick)
    {
        Label = new GUIText(
            labelText,
            new GUIElementConfig{ StartPosition = new Vector2f(), Style = config.Style },
            anchorPoint: GUIText.AnchorPoint.Center
        );
        BaseRect.Size = new Vector2f(
            Label.Text.GetGlobalBounds().Width * 2.3f,
            Label.Text.GetGlobalBounds().Height * 2.3f
        );
        Label.ElementPosition = new Vector2f(config.StartPosition.X+BaseRect.Size.X/2, config.StartPosition.Y+BaseRect.Size.Y/2);
        Label.SetAnchorPosition();
    }

    public override void Update()
    {
        base.Update();
    }

}