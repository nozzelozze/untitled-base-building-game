using System;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

public class PlayerMouse
{
    Texture CrosshairTexture;
    Sprite CrosshairSprite;

    public static bool OnUI;

    static Vector2i CurrentPosition;

    public PlayerMouse()
    {
        CrosshairTexture = ResourceLoader.FetchTexture(ResourceLoader.TextureType.Crosshair);
        CrosshairSprite = new Sprite(CrosshairTexture);
    }

    public static Vector2i GetPosition()
    {
        return CurrentPosition;
    }

    public Tile GetTileFromMouse()
    {
        return Map.Instance.GetTileAt(Camera.WinPositionToCam((Vector2f)GetPosition()));
    }

    public void RenderCrosshair(RenderWindow renderWindow)
    {
        CurrentPosition = Mouse.GetPosition(renderWindow);
        CrosshairSprite.Position = Map.Instance.GetTilePosition(GetTileFromMouse());
        RenderQueue.Queue(CrosshairSprite);
    }
}
