using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

public class IconButton : Button
{

    Sprite iconSprite;

    public IconButton(Texture iconTexture, Action onClick, Vector2f ? position = null, string ? title = null) : base(
        title==null ? "" : title, 
        onClick,
        position,
        true,
        (int)iconTexture.Size.X,
        (int)iconTexture.Size.Y
        )
    {
        iconSprite = new Sprite(iconTexture);
    }

    public override void render()
    {
        base.render();
        iconSprite.Position = Position;
        RenderQueue.queueGUI(iconSprite);
    }
}