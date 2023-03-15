using System;
using SFML.Graphics;
using SFML.System;

public class Resource
{
    public enum ResourceType
    {
        Iron,
        Copper,
    }

    private static Dictionary<ResourceType, Texture> ResourceTextures = new Dictionary<ResourceType, Texture>
    {
        {ResourceType.Iron, ResourceLoader.FetchTexture(ResourceLoader.TextureType.Iron)},
        {ResourceType.Copper, ResourceLoader.FetchTexture(ResourceLoader.TextureType.Copper)}
    };

    public static Dictionary<ResourceType, Item.Type> ItemTypes = new Dictionary<ResourceType, Item.Type>
    {
        {ResourceType.Iron, Item.Type.Iron},
        {ResourceType.Copper, Item.Type.Iron}
    };

    public Sprite Sprite;
    public ResourceType Type;
    public Vector2f Position;
    public Tile Tile;

    public ResourceMenu? ClickMenu;

    public Resource(ResourceType resourceType, Tile tile)
    {
        Type = resourceType;
        Sprite = new Sprite(ResourceTextures[resourceType]);
        Sprite.Position = Map.Instance.GetTilePosition(tile);
        Position = Sprite.Position;
        tile.GiveResource(this);
        this.Tile = tile;
    }

    public void Highlight()
    {
        ClickMenu = new ResourceMenu("Iron", this);
        ClickMenu.Position = Camera.CamPositionToWin(Position);
        ClickMenu.CloseButton.ButtonClicked += Player.PlayerHighlight.Unhighlight;
    }

    public void Render()
    {
        RenderQueue.Queue(Sprite);
    }
}
