using System;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

public class IconButton : Button
{

    private Sprite IconSprite;
    private GUIText Label;

    public IconButton(GUIElementConfig config, Action onClick, Texture iconTexture, string label)
    : base(config, onClick)
    {
        IconSprite = new Sprite(iconTexture);
        Label = new GUIText(
            label,
            new GUIElementConfig{
                StartPosition = new Vector2f(Position.X + iconTexture.Size.X + 10, Position.Y + iconTexture.Size.Y * 1.15f / 2),
                Style = config.Style
                },
            anchorPoint: GUIText.AnchorPoint.Left
        );
        BaseRect.Size = new Vector2f(
            iconTexture.Size.X + Label.Text.GetGlobalBounds().Width*1.15f,
            iconTexture.Size.Y + Label.Text.GetGlobalBounds().Height*1.15f
        );
        IconSprite.Position = new Vector2f(
            Position.X + 5,
            ( Position.Y + BaseRect.Size.Y / 2 ) - iconTexture.Size.Y / 2
        );
    }

    public override void Update()
    {
        base.Update();

        RenderQueue.QueueGUI(IconSprite);
    }

}