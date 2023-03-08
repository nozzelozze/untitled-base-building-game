using System;

public class StorageJob : Job
{
    public StorageComponent storage;
    public bool reverse;

    public StorageJob(StorageComponent storage, Tile jobTile, bool reverse = false) : base(jobTile)
    {
        this.storage = storage;
        this.reverse = reverse;
    }

    public override void doJob()
    {
        base.doJob();
        foreach (Item item in colonist.storageComponent.getItems().ToList())
        {
            colonist.storageComponent.swapItem(storage, item);
        }

        if (reverse)
        {
            foreach (Item item in storage.getItems().ToList())
            {
                storage.swapItem(colonist.storageComponent, item);
            }    
        } else
        {
            foreach (Item item in colonist.storageComponent.getItems().ToList())
            {
                colonist.storageComponent.swapItem(storage, item);
            }
        }

        isDone = true;
    }
}