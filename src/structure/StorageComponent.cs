using System;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

public class StorageComponent
{

    private Dictionary<Item.Type, int> items;
    bool storageFull;
    int maximumStorage;
    public int count
    {
        get
        {
            int sum = 0;
            foreach (KeyValuePair<Item.Type, int> entry in items)
            {
                sum += entry.Value;
            }
            return sum;
        }
        set
        {
            
        }
    }

    public StorageComponent(int maximumStorage)
    {
        items = new Dictionary<Item.Type, int>();
        storageFull = false;
        this.maximumStorage = maximumStorage;
    }

    public bool isFull()
    {
        return storageFull;
    }

    public Dictionary<Item.Type, int> getItems()
    {
        return items;
    }

    public void addItem(Item newItem)
    {
        if (items.Count < maximumStorage) 
            if (items.ContainsKey(newItem.type))
                items[newItem.type] += 1;
            else
                items.Add(newItem.type, 1);
        else
            storageFull = true;
    }

}