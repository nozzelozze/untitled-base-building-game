using System;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using SFML.Audio;
using System.Numerics;

public class MineJob : Job
{

	Resource resource;

	public MineJob(Resource resource)
	: base(resource.tile)
	{
		this.resource = resource;
	}

	public override void doJob()
	{
		base.doJob();
        Log.Message("in adaadadasssssssssssssssssssss");
		for (int i = 0; i < 50; i++)
		{
			colonist.storageComponent.addItem(new Item(Resource.itemTypes[resource.type]));
		}
		isDone = true;
	}

}