using System;
using SFML.System;

public class ConveyorBelt : Structure
{
    
    public ConveyorBelt()
    : base ("Conveyor Belt", ResourceLoader.fetchTexture(ResourceLoader.TextureType.Copper), new Vector2i(1, 1), new Dictionary<Item.Type, int>())
    {

    }

}