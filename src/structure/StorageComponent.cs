using System;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

public class StorageComponent
{

    private List<Item> items;
    bool storageFull;
    int maximumStorage;
    public int count
    {
        get
        {
            return items.Count;
        }
        set
        {
            
        }
    }

    public StorageComponent(int maximumStorage)
    {
        items = new List<Item>();
        storageFull = false;
        this.maximumStorage = maximumStorage;
    }

    public bool isFull()
    {
        return storageFull;
    }

    public void addItem(Item newItem)
    {
        if (items.Count < maximumStorage) 
            items.Add(newItem);
        else
            storageFull = true;
    }

}