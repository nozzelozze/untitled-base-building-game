using System;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

public class Miner : Structure
{

    private int myVariable;

    public int MyVariable
    {
        get { return myVariable; }
        set { myVariable = value; }
    }

    //public int oreCount = 1;

    public Miner()
    : base(ResourceLoader.fetchTexture(ResourceLoader.TextureType.Bed), new Vector2i(2, 1))
    {

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
}