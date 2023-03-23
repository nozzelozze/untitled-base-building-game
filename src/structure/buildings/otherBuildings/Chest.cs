using System;
using SFML.System;
using System.Collections.Generic;

public class Chest : Structure
{

    public StorageComponent StorageComponent;

    public Chest()
    : base("Chest", ResourceLoader.FetchTexture(ResourceLoader.TextureType.Chest), new Vector2i(2, 1), new Dictionary<Item.Type, int>
    {
        {Item.Type.Iron, 10}
    }
    )
    {
        StorageComponent = new StorageComponent(500);
    }

    public override void Highlight()
    {
        base.Highlight();
        if (WantMenu())
        {
            //InfoMenu.AddRow(new List<GUIActor>{new GUIText("Contents:")});
            foreach(KeyValuePair<Item.Type, int> entry in StorageComponent.ItemCount())
            {
                //InfoMenu.AddRow(new List<GUIActor>{new GUIText($"{Item.ItemNames[entry.Key]}: %v", tickVar: () => entry.Value)});
            }
        }
    }

}