using System;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using SFML.Audio;
using System.Numerics;

public class JobManager
{
    
    public static List<Job> jobQueue = new List<Job>();

    public JobManager()
    {

    }

    public Job getJob()
    {
    	return jobQueue[jobQueue.Count-1];
    	jobQueue.RemoveAt(jobQueue.Count-1);
    }

}