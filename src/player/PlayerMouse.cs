using System;
using System.Numerics;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

public class PlayerMouse
{
    Texture crosshairTexture;
    Sprite crosshairSprite;

    public PlayerMouse()
    {
        crosshairTexture = ResourceLoader.fetchTexture(ResourceLoader.TextureType.Crosshair);
        crosshairSprite = new Sprite(crosshairTexture);
    }

    public Tile getTileFromMouse()
    {
        return Map.Instance.getTileAt(Camera.winPositionToCam((Vector2f)Mouse.GetPosition()));
    }

    public void renderCrosshair()
    {
        crosshairSprite.Position = Map.Instance.getTilePosition(getTileFromMouse());
        RenderQueue.queue(crosshairSprite);
    }

}