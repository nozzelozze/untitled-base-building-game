using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

class IconButton : Button
{

    Sprite iconSprite;

    public IconButton(Texture iconTexture, Vector2f position, Action onClick, string ? title=null) : base(
        title==null ? "" : title, 
        position,
        onClick,
        true
        )
    {
        iconSprite = new Sprite(iconTexture);
        iconSprite.Position = position;
    }

    public override void render()
    {
        base.render();
        RenderQueue.queueGUI(iconSprite);
    }
}