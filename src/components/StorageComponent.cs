using System;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

public class StorageComponent
{

    private List<Item> items;
    private bool storageFull;
    private int maximumStorage;

    public StorageComponent(int maximumStorage)
    {
        items = new List<Item>();
        storageFull = false;
        this.maximumStorage = maximumStorage;
    }

    public void swapItem(StorageComponent to, Item item)
    {
        if (!to.isFull())
        {
            removeItem(item);
            to.addItem(item);
        }       
    }

    public bool isFull()
    {
        return storageFull;
    }

    public int getCount()
    {
        return items.Count;
    }

    public List<Item> getItems()
    {
        return items;
    }

    public void addItem(Item newItem)
    {
        if (!storageFull)
        {
            items.Add(newItem);
            if (items.Count == maximumStorage)
            {
                storageFull = true;
            }
        }
    }

    public void removeItem(Item.Type itemType)
    {
        foreach (Item item in items)
        {
            if (item.type == itemType)
            {
                items.Remove(item);
                break;
            }
        }
    }

    public void removeItem(Item item)
    {
        if (items.Contains(item)) items.Remove(item);
    }
}