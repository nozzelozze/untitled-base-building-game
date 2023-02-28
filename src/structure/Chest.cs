using System;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

public class Chest : Structure
{

    public StorageComponent storageComponent;

    public Chest()
    : base("Chest", ResourceLoader.fetchTexture(ResourceLoader.TextureType.Chest), new Vector2i(2, 1), new Dictionary<Item.Type, int>())
    {
        storageComponent = new StorageComponent(500);
    }

    public override void highlight()
    {
        base.highlight();
        infoMenu.changeTitle("Chest");
        if (wantMenu())
        {
            infoMenu.addRow(new List<GUIActor>{new GUIText("Contents:")});
            foreach(KeyValuePair<Item.Type, int> entry in storageComponent.itemCount())
            {
                infoMenu.addRow(new List<GUIActor>{new GUIText($"{Item.itemNames[entry.Key]}: %v", tickVar: () => entry.Value)});
            }
        }
    }

}