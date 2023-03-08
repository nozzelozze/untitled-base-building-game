using System;

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

    public Item(Type type)
    {
        this.type = type;
        this.name = itemNames[type];
    }
}