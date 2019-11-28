using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItemObject
{
    string Name { get; }
    string Description { get; }
    Sprite Image { get; }
    AudioClip PickupSound { get; }

    bool IsDroppable { get; }
    bool IsUsable { get; }

    void OnGrab();
    void OnDrop();
    void OnUse();
}

public class InventoryEventArgs : EventArgs
{
    public InventoryEventArgs(IItemObject item)
    {
        Item = item;
    }

    public IItemObject Item;
}
