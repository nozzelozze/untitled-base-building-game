using System;
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
        if (storageComponent.isFull())
        {
            List<StorageJob> storageJobs = JobManager.jobQueue.OfType<StorageJob>().ToList();
            List<StorageJob> currentJobs = ColonistJobManager.currentJobs.OfType<StorageJob>().ToList();
            if (!storageJobs.Any(job => job.storage == storageComponent) && !currentJobs.Any(job => job.storage == storageComponent))
            {
                Log.Message("DADA");
                JobManager.addToQueue(new StorageJob(storageComponent, startTile, true));
            }
        }
        else
        {
            if (new Random().Next(30) == 5) storageComponent.addItem(new Item(Item.Type.Iron));
        }
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