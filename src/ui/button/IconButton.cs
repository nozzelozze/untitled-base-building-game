using System;
using SFML.Graphics;
using SFML.System;

public class IconButton : Button
{

    Sprite IconSprite;

    public IconButton(Texture iconTexture, Action onClick, Vector2f? position = null, string? title = null) : base(
        title == null ? "" : title,
        onClick,
        position,
        true,
        (int)iconTexture.Size.X,
        (int)iconTexture.Size.Y
        )
    {
        IconSprite = new Sprite(iconTexture);

        TextCenter = () => new Vector2f(Position.X + this.BaseRect.Size.X / 2, Position.Y + this.BaseRect.Size.Y + 20);
    }

    public override void Render()
    {
        IconSprite.Position = Position;
        RenderQueue.QueueGUI(IconSprite);
        base.Render();
    }
}
