using System;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

public class Resource
{
    public enum ResourceType
    {
        Iron,
        Copper,
    }

    Dictionary<ResourceType, Texture> resourceTextures = new Dictionary<ResourceType, Texture>
    {
        {ResourceType.Iron, ResourceLoader.fetchTexture(ResourceLoader.TextureType.Iron)},
        {ResourceType.Copper, ResourceLoader.fetchTexture(ResourceLoader.TextureType.Copper)}
    };

    public Sprite sprite;
    public ResourceType type;
    public Vector2f position;

    public ResourceMenu ?  clickMenu;

    public Resource(ResourceType resourceType, Tile tile)
    {
        type = resourceType;
        sprite = new Sprite(resourceTextures[resourceType]);
        sprite.Position = Map.Instance.getTilePosition(tile);
        position = sprite.Position;
        tile.giveResource(this);
    }

    public void highlight()
    {
        clickMenu = new ResourceMenu("Iron", this);
        clickMenu.Position = Camera.camPositionToWin(position);
        clickMenu.closeButton.buttonClicked += Player.playerHighlight.unhightlight;
    }

    public void render()
    {
        RenderQueue.queue(sprite);
    }

}