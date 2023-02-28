using System;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using SFML.Audio;
using System.Numerics;

public class Job
{

    public Tile jobTile;
    public Colonist colonist;

    public Job(Tile tile)
    {
        jobTile = tile;
    }

    private bool canWork()
    {
        if (Pathfinding.GetNeighborTiles(jobTile).Contains(Map.Instance.getTileAt(colonist.Position)))
        {
            return true;
        }
        return false;
    }

    public virtual void doJob(Colonist colonist)
    {
        this.colonist = colonist;
        if (canWork())
        {
            
        }
    }

    public virtual void beginJob()
    {
        colonist.walk.beginWalk(jobTile);
    }

}