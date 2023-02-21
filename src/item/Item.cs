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
    public static Dictionary<Type, string> itemNames = new Dictionary<Type, string>
    {
        {Type.Iron, "Iron"}
    };

    string name;
    public Type type;

    public Item(string name, Type type)
    {
        this.name = name;
        this.type = type;
    }
}