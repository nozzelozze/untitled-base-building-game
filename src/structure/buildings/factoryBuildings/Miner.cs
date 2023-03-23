using System;
using SFML.System;
using System.Collections.Generic;
using System.Linq;

public class Miner : Structure
{

    public int OreCount = 1;
    
    public Resource.ResourceType ResourceType;

    private StorageComponent StorageComponent;

    public Miner()
    : base("Miner", ResourceLoader.FetchTexture(ResourceLoader.TextureType.Bed), new Vector2i(2, 1), new Dictionary<Item.Type, int>())
    {
        StorageComponent = new StorageComponent(100);
    }

    public override void PlaceStructure(Tile tile, bool instaBuild = false)
    {
        base.PlaceStructure(tile);
        if (tile.Resource != null) ResourceType = tile.Resource.Type;
    }

    public override void Highlight()
    {
        base.Highlight();
        if (WantMenu())
        {
            //InfoMenu.AddItem(new GUIText($"Harvesting {ResourceType}"));
            //InfoMenu.AddItem(new GUIText($"Ore Count: %v", tickVar: () => StorageComponent.GetCount()));
        }
    }

    public override void Update()
    {
        base.Update();
        if (StorageComponent.IsFull())
        {
            List<StorageJob> storageJobs = JobManager.JobQueue.OfType<StorageJob>().ToList();
            List<StorageJob> currentJobs = ColonistJobManager.CurrentJobs.OfType<StorageJob>().ToList();
            if (!storageJobs.Any(job => job.Storage == StorageComponent) && !currentJobs.Any(job => job.Storage == StorageComponent))
            {
                Log.Message("DADA");
                JobManager.AddToQueue(new StorageJob(StorageComponent, StartTile, true));
            }
        }
        else
        {
            if (new Random().Next(30) == 5) StorageComponent.AddItem(new Item(Item.Type.Iron));
        }
    }

    public override bool IsTileValid(Tile tile)
    {
        if (tile.HasResource() && !tile.IsOccupied())
        {
            return true;
        }
        return false;
    }
}
