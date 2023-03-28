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
            new GUIElementConfig{ StartPosition = new Vector2f(), Style = config.Style, GetRelativeTo = () => new Vector2f(ElementPosition.X + BaseRect.Size.X/2, ElementPosition.Y + BaseRect.Size.Y/2) },
            anchorPoint: GUIText.AnchorPoint.Center
        );
        BaseRect.Size = new Vector2f(
            Label.Text.GetGlobalBounds().Width * 2.3f,
            Label.Text.GetGlobalBounds().Height * 2.3f
        );
        Label.ElementPosition = new Vector2f(2, 2);
        Label.SetAnchorPosition();
        ChildGUIElements.Add(Label);
    }

    public override void Update()
    {
        base.Update();
    }

}