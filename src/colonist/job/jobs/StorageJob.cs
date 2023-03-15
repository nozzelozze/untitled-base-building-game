using System;

public class StorageJob : Job
{
    public StorageComponent Storage;
    public bool Reverse;

    public StorageJob(StorageComponent storage, Tile jobTile, bool reverse = false) : base(jobTile)
    {
        this.Storage = storage;
        this.Reverse = reverse;
    }

    public override void DoJob()
    {
        base.DoJob();
        foreach (Item item in Colonist.StorageComponent.GetItems().ToList())
        {
            Colonist.StorageComponent.SwapItem(Storage, item);
        }

        if (Reverse)
        {
            foreach (Item item in Storage.GetItems().ToList())
            {
                Storage.SwapItem(Colonist.StorageComponent, item);
            }    
        } else
        {
            foreach (Item item in Colonist.StorageComponent.GetItems().ToList())
            {
                Colonist.StorageComponent.SwapItem(Storage, item);
            }
        }

        IsDone = true;
    }
}
