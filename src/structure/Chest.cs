using System;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

public class Chest : Structure
{

    StorageComponent storageComponent;

    public Chest()
    : base(ResourceLoader.fetchTexture(ResourceLoader.TextureType.Chest), new Vector2i(2, 1))
    {
        storageComponent = new StorageComponent(500);
    }

    public override void highlight()
    {
        base.highlight();
        if (infoMenu != null)
        {
            infoMenu.changeTitle("Chest");
        }
    }

}