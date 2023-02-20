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
    : base(ResourceLoader.fetchTexture(ResourceLoader.TextureType.Bed), new Vector2i(2, 1))
    {
        storageComponent = new StorageComponent(100);
    }

    public override void placeStructure(Tile tile)
    {
        base.placeStructure(tile);
        if (tile.resource != null) resourceType = tile.resource.type;
    }

    public override void highlight()
    {
        base.highlight();
        if (infoMenu != null)
        {
            infoMenu.changeTitle("Miner");
            infoMenu.addItem(new GUIText($"Harvesting {resourceType}"));
            infoMenu.addItem(new GUIText($"Ore Count: %v", tickVar: storageComponent.count));
        }
    }

    public override void update()
    {
        base.update();
        if (!storageComponent.isFull()) 
            storageComponent.addItem(new Item("iron"));
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