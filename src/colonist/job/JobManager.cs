using System;

public class JobManager
{
    
    public static List<Job> JobQueue = new List<Job>();

    public JobManager()
    {

    }

    public static void AddToQueue(Job newJob)
    {
        JobQueue.Add(newJob);
    }

    public static Job GetJob()
    {
    	Job job = JobQueue[0];
    	JobQueue.Remove(job);
        return job;
    }

}
