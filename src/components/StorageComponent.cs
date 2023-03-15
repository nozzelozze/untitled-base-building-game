using System;

public class StorageComponent
{

    private List<Item> Items;
    private List<Item.Type>? AcceptedItems;
    private bool StorageFull;
    private int MaximumStorage;

    public StorageComponent(int maximumStorage, List<Item.Type>? acceptedItems = null)
    {
        Items = new List<Item>();
        StorageFull = false;
        MaximumStorage = maximumStorage;
        AcceptedItems = acceptedItems;
    }

    public void SwapItem(StorageComponent to, Item item)
    {
        if (!to.IsFull())
        {
            if (to.AcceptedItems == null)
            {
                RemoveItem(item);
                to.AddItem(item);
            }
            else
            {
                if (to.AcceptedItems.Contains(item.ItemType))
                {
                    RemoveItem(item);
                    to.AddItem(item);
                }
            }
        }
    }

    public bool IsFull()
    {
        return StorageFull;
    }

    public int GetCount()
    {
        return Items.Count;
    }

    public Dictionary<Item.Type, int> ItemCount()
    {
        Dictionary<Item.Type, int> Count = new Dictionary<Item.Type, int>();
        foreach (Item item in GetItems())
        {
            if (!Count.ContainsKey(item.ItemType))
            {
                Count.Add(item.ItemType, 1);
            }
            else
            {
                Count[item.ItemType]++;
            }
        }
        return Count;
    }

    public List<Item> GetItems()
    {
        return Items;
    }

    public void AddItem(Item newItem)
    {
        if (AcceptedItems != null)
        {
            if (!AcceptedItems.Contains(newItem.ItemType))
            {
                return;
            }
        }
        if (!StorageFull)
        {
            Items.Add(newItem);
            if (Items.Count == MaximumStorage)
            {
                StorageFull = true;
            }
        }
    }

    public void RemoveItem(Item.Type itemType)
    {
        foreach (Item item in Items)
        {
            if (item.ItemType == itemType)
            {
                Items.Remove(item);
                break;
            }
        }
        StorageFull = false;
    }

    public void RemoveItem(Item item)
    {
        if (Items.Contains(item)) Items.Remove(item);
        StorageFull = false;
    }
}
