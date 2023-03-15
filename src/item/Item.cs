using System;

public class Item
{

    public enum Type
    {
        Iron
    }
    public static Dictionary<Type, string> ItemNames = new Dictionary<Type, string>
    {
        {Type.Iron, "Iron"}
    };

    string Name;
    public Type ItemType;

    public Item(Type type)
    {
        this.ItemType = type;
        this.Name = ItemNames[type];
    }
}
