using System;
using System.Numerics;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

public class PlayerMouse
{
    Texture crosshairTexture;
    Sprite crosshairSprite;

    static Vector2i currentPosition;

    public PlayerMouse()
    {
        crosshairTexture = ResourceLoader.fetchTexture(ResourceLoader.TextureType.Crosshair);
        crosshairSprite = new Sprite(crosshairTexture);
    }

    public static Vector2i getPosition()
    {
        return currentPosition;
    }

    public Tile getTileFromMouse()
    {
        return Map.Instance.getTileAt(Camera.winPositionToCam((Vector2f)getPosition()));
    }

    public void renderCrosshair(RenderWindow renderWindow)
    {
        currentPosition = Mouse.GetPosition(renderWindow);
        crosshairSprite.Position = Map.Instance.getTilePosition(getTileFromMouse());
        RenderQueue.queue(crosshairSprite);
    }

}