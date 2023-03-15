using System;

public class ColonistJobManager
{

    private Colonist Colonist;
    public Job ? CurrentJob;
    private List<Job> PersonalJobQueue = new List<Job>();
    
    public static List<Job> CurrentJobs = new List<Job>();

    public ColonistJobManager(Colonist colonist)
    {
        this.Colonist = colonist;
    }

    public void WorkCurrentJob()
    {
        CurrentJob?.DoJob();
    }

    public void JobDone()
    {
        CurrentJobs.Remove(CurrentJob);
        CurrentJob = null;
    }

    public void QueueJob(Job newJob)
    {
        PersonalJobQueue.Add(newJob);
    }

    public List<Job> SeeJobs()
    {
        List<Job> Jobs = new List<Job>();
        Jobs.AddRange(PersonalJobQueue);
        Jobs.AddRange(JobManager.JobQueue);
        if (CurrentJob != null) Jobs.Add(CurrentJob);
        return Jobs;
    }

    public void EmptyStorage()
    {
        List<StorageJob> StorageJobs = SeeJobs().OfType<StorageJob>().ToList();
        foreach (StorageJob job in StorageJobs)
        {
            if (!job.Reverse) return;
        }
        Chest FirstChest = Structure.GetNearestStructure<Chest>();
        if (FirstChest != null) QueueJob(new StorageJob(FirstChest.StorageComponent, FirstChest.StartTile));
    }

    public void PushBackCurrentJob()
    {
        if (CurrentJob != null)
        {
            PersonalJobQueue.Insert(0, CurrentJob);
            CurrentJob = PersonalJobQueue[1];
            CurrentJobs.Add(CurrentJob);
            PersonalJobQueue.RemoveAt(1);
            CurrentJob.BeginJob(Colonist);
        }
    }

    public void Update()
    {
        if (CurrentJob == null)
        {
            if (PersonalJobQueue.Count == 0)
            {
                if (JobManager.JobQueue.Count != 0)
                {
                    QueueJob(JobManager.GetJob());
                }
            } else
            {
                CurrentJob = PersonalJobQueue[0];
                CurrentJobs.Add(CurrentJob);
                CurrentJob.BeginJob(Colonist);
                PersonalJobQueue.Remove(CurrentJob);
            }
        } else
        {
            CurrentJob.UpdateJob();
        }
    }

}
