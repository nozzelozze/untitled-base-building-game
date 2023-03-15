using System;

public class Job
{

    private Tile JobTile;
    private protected Colonist Colonist;

    public bool IsDone = false;

    public Job(Tile tile)
    {
        JobTile = tile;
    }

    private bool CanWork()
    {
        if (Pathfinding.GetNeighborTiles(JobTile).Contains(Map.Instance.GetTileAt(Colonist.Position)))
        {
            return true;
        }
        return false;
    }

    public virtual void DoJob()
    {
        if (CanWork())
        {
            
        }
    }

    public virtual void BeginJob(Colonist colonist)
    {
        this.Colonist = colonist;
        this.Colonist.Walk.BeginWalk(JobTile);
    }

    public virtual void UpdateJob()
    {

        if (IsDone)
        {
            Colonist.PersonalJobManager.JobDone();
        }

    }

}
