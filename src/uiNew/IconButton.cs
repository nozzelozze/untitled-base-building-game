using System;
using SFML.Graphics;
using SFML.Window;
using SFML.System;


public class _IconButton : _Button
{

    private Sprite IconSprite;
    private GUIText Label;

    public _IconButton(Vector2f position, Action onClick, Texture iconTexture, string label)
    : base(position, onClick)
    {
        IconSprite = new Sprite(iconTexture);
        
    }

    public override void Update()
    {
        base.Update();
        IconSprite.Position = Position;
        

        RenderQueue.QueueGUI(IconSprite);
    }

}