using System;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using SFML.Audio;
using System.Numerics;

public class Job
{

    private Tile jobTile;
    private protected Colonist colonist;

    private protected bool isDone = false;

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

    public virtual void doJob()
    {
        if (canWork())
        {
            
        }
    }

    public virtual void beginJob(Colonist colonist)
    {
        this.colonist = colonist;
        this.colonist.walk.beginWalk(jobTile);
    }

    public virtual void updateJob()
    {

        if (isDone)
        {
            colonist.jobDone();
        }

    }

}