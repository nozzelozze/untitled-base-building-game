using System;

public class MineJob : Job
{

	Resource Resource;

	public MineJob(Resource resource)
	: base(resource.Tile)
	{
		this.Resource = resource;
	}

	public override void DoJob()
	{
		base.DoJob();
		for (int i = 0; i < 25; i++)
		{
			Colonist.StorageComponent.AddItem(new Item(Resource.ItemTypes[Resource.Type]));
		}
		IsDone = true;
	}

}
