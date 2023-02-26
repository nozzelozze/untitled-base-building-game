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
        storage.
    }
}