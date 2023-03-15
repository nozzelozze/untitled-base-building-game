using System;

public class BuildJob : Job
{

    public Structure Structure;

    public BuildJob(Structure unBuiltStructure)
    : base(unBuiltStructure.StartTile)
    {
        if (unBuiltStructure.Built == true)
            Log.Warning("Already built structure as argument in BuildJob constructor.");

        Structure = unBuiltStructure;
    }

    public override void DoJob()
    {
        base.DoJob();
        foreach (KeyValuePair<Item.Type, int> itemPair in Structure.Cost)
        {
            if (Colonist.StorageComponent.GetItems().Any(item => item.ItemType == itemPair.Key))
            {
                for (int i = 0; i < Colonist.StorageComponent.ItemCount()[itemPair.Key]; i++)
                {
                    Colonist.StorageComponent.RemoveItem(itemPair.Key);
                    if (Structure.Deposit[itemPair.Key] < Structure.Cost[itemPair.Key]) Structure.Deposit[itemPair.Key] ++;
                    if (Structure.IsPaidFor())
                    {
                        break;
                    }
                }
            } else
            {
                Tile ? firstTileWithResource = null;

                for (int i = 0; i < Map.Instance.Tiles.GetLength(0); i++)
                {
                    for (int j = 0; j < Map.Instance.Tiles.GetLength(1); j++)
                    {
                        if (Map.Instance.Tiles[i, j].HasResource())
                        {
                            firstTileWithResource = Map.Instance.Tiles[i, j];
                            goto a;
                        }
                    }
                }
                a:
                Colonist.PersonalJobManager.QueueJob(new MineJob(firstTileWithResource.Resource));
                Colonist.PersonalJobManager.PushBackCurrentJob();
            }
        }
    }

    public override void UpdateJob()
    {
        base.UpdateJob();
        if (Structure.IsPaidFor())
        {
            IsDone = true;
        }
    }
}
