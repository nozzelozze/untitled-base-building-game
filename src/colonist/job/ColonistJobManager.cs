using System;

public class ColonistJobManager
{

    private Colonist colonist;
    public Job ? currentJob;
    private List<Job> personalJobQueue = new List<Job>();
    
    public static List<Job> currentJobs = new List<Job>();

    public ColonistJobManager(Colonist colonist)
    {
        this.colonist = colonist;
    }

    public void workCurrentJob()
    {
        currentJob?.doJob();
    }

    public void jobDone()
    {
        currentJobs.Remove(currentJob);
        currentJob = null;
    }

    public void queueJob(Job newJob)
    {
        personalJobQueue.Add(newJob);
    }

    public List<Job> seeJobs()
    {
        List<Job> jobs = new List<Job>();
        jobs.AddRange(personalJobQueue);
        jobs.AddRange(JobManager.jobQueue);
        if (currentJob != null) jobs.Add(currentJob);
        return jobs;
    }

    public void emptyStorage()
    {
        List<StorageJob> storageJobs = seeJobs().OfType<StorageJob>().ToList();
        foreach (StorageJob job in storageJobs)
        {
            if (!job.reverse) return;
        }
        Chest firstChest = Structure.getNearestStructure<Chest>();
        if (firstChest != null) queueJob(new StorageJob(firstChest.storageComponent, firstChest.startTile));
    }

    public void pushBackCurrentJob()
    {
        if (currentJob != null)
        {
            personalJobQueue.Insert(0, currentJob);
            currentJob = personalJobQueue[1];
            currentJobs.Add(currentJob);
            personalJobQueue.RemoveAt(1);
            currentJob.beginJob(colonist);
        }
    }

    public void update()
    {
        if (currentJob == null)
        {
            if (personalJobQueue.Count == 0)
            {
                if (JobManager.jobQueue.Count != 0)
                {
                    queueJob(JobManager.getJob());
                }
            } else
            {
                currentJob = personalJobQueue[0];
                currentJobs.Add(currentJob);
                currentJob.beginJob(colonist);
                personalJobQueue.Remove(currentJob);
            }
        } else
        {
            currentJob.updateJob();
        }
    }

}