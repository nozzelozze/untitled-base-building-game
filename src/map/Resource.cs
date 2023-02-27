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

    private static Dictionary<ResourceType, Texture> resourceTextures = new Dictionary<ResourceType, Texture>
    {
        {ResourceType.Iron, ResourceLoader.fetchTexture(ResourceLoader.TextureType.Iron)},
        {ResourceType.Copper, ResourceLoader.fetchTexture(ResourceLoader.TextureType.Copper)}
    };

    public static Dictionary<ResourceType, Item.Type> itemTypes = new Dictionary<ResourceType, Item.Type>
    {
        {ResourceType.Iron, Item.Type.Iron},
        {ResourceType.Copper, Item.Type.Iron}
    };

    public Sprite sprite;
    public ResourceType type;
    public Vector2f position;
    public Tile tile;

    public ResourceMenu ?  clickMenu;

    public Resource(ResourceType resourceType, Tile tile)
    {
        type = resourceType;
        sprite = new Sprite(resourceTextures[resourceType]);
        sprite.Position = Map.Instance.getTilePosition(tile);
        position = sprite.Position;
        tile.giveResource(this);
        this.tile = tile;
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