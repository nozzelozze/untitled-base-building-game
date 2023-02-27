using System;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

public class Miner : Structure
{

    public int oreCount = 1;
    
    public Resource.ResourceType resourceType;

    private StorageComponent storageComponent;

    public Miner()
    : base("Miner", ResourceLoader.fetchTexture(ResourceLoader.TextureType.Bed), new Vector2i(2, 1), new Dictionary<Item.Type, int>())
    {
        storageComponent = new StorageComponent(100);
    }

    public override void placeStructure(Tile tile, bool instaBuild = false)
    {
        base.placeStructure(tile);
        if (tile.resource != null) resourceType = tile.resource.type;
    }

    public override void highlight()
    {
        base.highlight();
        infoMenu.changeTitle("Miner");
        if (wantMenu())
        {
            infoMenu.addItem(new GUIText($"Harvesting {resourceType}"));
            infoMenu.addItem(new GUIText($"Ore Count: %v", tickVar: () => storageComponent.getCount()));
        }
    }

    public override void update()
    {
        base.update();
        if (!storageComponent.isFull()) 
            if (new Random().Next(10) == 5) storageComponent.addItem(new Item("iron", Item.Type.Iron));
        else
            {}
    }

    public override bool isTileValid(Tile tile)
    {
        if (tile.hasResource() && !tile.isOccupied())
        {
            return true;
        }
        return false;
    }
}