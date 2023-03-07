using System;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using SFML.Audio;
using System.Numerics;

public class BuildJob : Job
{

    private Structure structure;

    public BuildJob(Structure unBuiltStructure)
    : base(unBuiltStructure.startTile)
    {
        if (unBuiltStructure.built == true)
            Log.Warning("Already built structure as argument in BuildJob constructor.");

        structure = unBuiltStructure;
    }

    public override void doJob()
    {
        base.doJob();
        foreach (KeyValuePair<Item.Type, int> itemPair in structure.cost)
        {
            if (colonist.storageComponent.getItems().Any(item => item.type == itemPair.Key))
            {
                Log.Message("DADAD");
                colonist.storageComponent.removeItem(itemPair.Key);
                if (structure.deposit[itemPair.Key] < structure.cost[itemPair.Key]) structure.deposit[itemPair.Key] ++;
            } else
            {
                Tile firstTileWithResource = null;

                for (int i = 0; i < Map.Instance.tiles.GetLength(0); i++)
                {
                    for (int j = 0; j < Map.Instance.tiles.GetLength(1); j++)
                    {
                        if (Map.Instance.tiles[i, j].hasResource())
                        {
                            firstTileWithResource = Map.Instance.tiles[i, j];
                            goto a;
                        }
                    }
                }
                a:
                colonist.addToPersonalJobQueue(new MineJob(firstTileWithResource.resource));
                colonist.pushBackCurrentJob();
            }
        }
        isDone = true;
    }
}