using System;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using SFML.Audio;
using System.Numerics;

public class StorageJob : Job
{
    StorageComponent storage;

    public StorageJob(StorageComponent storage, Tile jobTile) : base(jobTile)
    {
        this.storage = storage;
    }

    public override void doJob()
    {
        base.doJob();
        
        //do
        //{
            foreach (Item item in colonist.storageComponent.getItems().ToList())
            {
                colonist.storageComponent.swapItem(storage, item);
            }
            //for (int i = 0; i < colonist.storageComponent.getItems().Count; i++)
            //{
            //    colonist.storageComponent.swapItem(storage, colonist.storageComponen t.getItems()[i]);
            //}
        //} while (colonist.storageComponent.getCount() != 0 || !storage.isFull());

        Log.Message("Job done!");
        isDone = true;
    }
}