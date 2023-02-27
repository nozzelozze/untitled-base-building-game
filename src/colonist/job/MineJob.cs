using System;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using SFML.Audio;
using System.Numerics;

public class MineJob : Job§
{

	Resource resource;

	public MineJob(Resource resource, Colonist colonist)
	: base(resource.tile, colonist)
	{
		this.resource = resource;
	}

	public override void doJob()
	{
		base.doJob();

		for (int i = 0; i < 25; i++)
		{
			colonist.storageComponent.addItem();
		}

	}

}