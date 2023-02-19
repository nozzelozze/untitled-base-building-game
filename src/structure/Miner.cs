using System;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

public class Miner : Structure
{

    public int oreCount = 1;
    
    public Resource resource;

    public Miner()
    : base(ResourceLoader.fetchTexture(ResourceLoader.TextureType.Bed), new Vector2i(2, 1))
    {

    }

    public override void placeStructure(Tile tile)
    {
        base.placeStructure(tile);
    }

    public override void highlight()
    {
        base.highlight();
        infoMenu.addItem(new TextButton("oreCount", () => {}));
    }

    public override void update()
    {
        base.update();
        //oreCount.value ++;
    }

    public override bool isTileValid(Tile tile)
    {
        if (tile.hasResource() && !tile.isOccupied())
        {
            return true;
        }
        return false;
    }
}