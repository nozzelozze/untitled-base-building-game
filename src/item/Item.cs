using System;
using System.Linq;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

public class Item
{

    public enum Type
    {
        Iron
    }

    string name;
    Type type;

    public Item(string name, Type type)
    {
        this.name = name;
        this.type = type;
    }
}