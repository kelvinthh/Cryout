using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private const int SLOTS = 5;

    public List<IItemObject> itemList = new List<IItemObject>();

    public event EventHandler<InventoryEventArgs> ItemAdded;
    public event EventHandler<InventoryEventArgs> ItemRemoved;
    public event EventHandler<InventoryEventArgs> ItemUsed;

    public void AddItem(IItemObject item)
    {
        if (itemList.Count < SLOTS)
        {
            Collider collider = (item as MonoBehaviour).GetComponent<Collider>();
            if (collider.enabled)
            {
                collider.enabled = false;
                itemList.Add(item);
                item.OnGrab();
            }
            ItemAdded?.Invoke(this, new InventoryEventArgs(item));
        }
        else
        {
            Debug.Log("Inventory is full.");
        }
    }

    public void RemoveItem(IItemObject item)
    {
        if (itemList.Contains(item))
        {
            itemList.Remove(item);
            item.OnDrop();

            Collider collider = (item as MonoBehaviour).GetComponent<Collider>();
            if (collider != null)
            {
                collider.enabled = true;
            }
            ItemRemoved?.Invoke(this, new InventoryEventArgs(item));
        }
    }

    internal void UseItem(IItemObject item)
    {
        if (itemList.Contains(item))
        {
            item.OnUse();
            itemList.Remove(item);
            ItemUsed?.Invoke(this, new InventoryEventArgs(item));
        }
    }
}
