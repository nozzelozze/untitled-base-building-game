using System;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using SFML.Audio;
using System.Numerics;

public class StorageJob : Job
{
    StorageComponent storage;

    public StorageJob(StorageComponent storage, Tile jobTile, Colonist colonist) : base(jobTile, colonist)
    {
        this.storage = storage;
    }

    public override void doJob()
    {
        base.doJob();
        
        do
        {
            foreach (Item item in storage.getItems())
            {
                //StorageComponent.swapItem(storage, colonist.storageComponent, item);
            }
        } while (false)//(!colonist.storageComponent.isFull());

        Log.Message("Job done!");
    }
}