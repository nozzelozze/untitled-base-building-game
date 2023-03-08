using System;

public class JobManager
{
    
    public static List<Job> jobQueue = new List<Job>();

    public JobManager()
    {

    }

    public static void addToQueue(Job newJob)
    {
        jobQueue.Add(newJob);
    }

    public static Job getJob()
    {
    	Job job = jobQueue[0];
    	jobQueue.Remove(job);
        return job;
    }

}