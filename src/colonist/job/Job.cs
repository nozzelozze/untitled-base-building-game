using System;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using SFML.Audio;
using System.Numerics;

public class Job
{

    Tile jobTile;
    Colonist colonist;

    public Job(Tile tile, Colonist colonist)
    {
        jobTile = tile;
        this.colonist = colonist;
    }

    private bool canWork()
    {
        if (Pathfinding.GetNeighborTiles(jobTile).Contains(Map.Instance.getTileAt(colonist.Position)))
        {
            return true;
        }
        return false;
    }

    public virtual void doJob()
    {
        if (canWork())
        {
            Log.Message("jobbing!");
        } else
        {
            Log.Message("cant work!");
        }
    }

}