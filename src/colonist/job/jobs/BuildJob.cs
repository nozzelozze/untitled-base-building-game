using System;

public class BuildJob : Job
{

    public Structure structure;

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
                for (int i = 0; i < colonist.storageComponent.itemCount()[itemPair.Key]; i++)
                {
                    colonist.storageComponent.removeItem(itemPair.Key);
                    if (structure.deposit[itemPair.Key] < structure.cost[itemPair.Key]) structure.deposit[itemPair.Key] ++;
                    if (structure.isPaidFor())
                    {
                        break;
                    }
                }
            } else
            {
                Tile ? firstTileWithResource = null;

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
                colonist.personalJobManager.queueJob(new MineJob(firstTileWithResource.resource));
                colonist.personalJobManager.pushBackCurrentJob();
            }
        }
    }

    public override void updateJob()
    {
        base.updateJob();
        if (structure.isPaidFor())
        {
            isDone = true;
        }
    }
}