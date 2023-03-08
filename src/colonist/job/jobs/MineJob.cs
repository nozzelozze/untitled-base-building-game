using System;

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
		for (int i = 0; i < 25; i++)
		{
			colonist.storageComponent.addItem(new Item(Resource.itemTypes[resource.type]));
		}
		isDone = true;
	}

}