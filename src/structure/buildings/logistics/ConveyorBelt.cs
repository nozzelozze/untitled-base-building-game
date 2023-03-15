using System;
using SFML.System;
using System.Collections.Generic;

public class ConveyorBelt : Structure
{
    
    public ConveyorBelt()
    : base ("Conveyor Belt", ResourceLoader.FetchTexture(ResourceLoader.TextureType.Copper), new Vector2i(1, 1), new Dictionary<Item.Type, int>())
    {

    }

}
