using System;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

public class StorageComponent
{

    private List<Item> items;
    private List<Item.Type> ? acceptedItems;
    private bool storageFull;
    private int maximumStorage;

    public StorageComponent(int maximumStorage, List<Item.Type> ? acceptedItems = null)
    {
        items = new List<Item>();
        storageFull = false;
        this.maximumStorage = maximumStorage;
    }

    public void swapItem(StorageComponent to, Item item)
    {
        if (!to.isFull())
        {
            if (to.acceptedItems == null)
            {
                removeItem(item);
                to.addItem(item);
            } else
            {
                if (to.acceptedItems.Contains(item.type))
                {
                    removeItem(item);
                    to.addItem(item);
                }
            }
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

    public Dictionary<Item.Type, int> itemCount()
    {
        Dictionary<Item.Type, int> count = new Dictionary<Item.Type, int>();
        foreach (Item item in getItems())
        {
            if (!count.ContainsKey(item.type))
            {
                count.Add(item.type, 1);
            } else
            {
                count[item.type] ++;
            }
        }
        return count;
    }

    public List<Item> getItems()
    {
        return items;
    }

    public void addItem(Item newItem)
    {
        if (acceptedItems != null)
        {
            if (!acceptedItems.Contains(newItem.type)) 
            {
                return;
            }
        }
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
        storageFull = false;
    }

    public void removeItem(Item item)
    {
        if (items.Contains(item)) items.Remove(item);
        storageFull = false;
    }
}