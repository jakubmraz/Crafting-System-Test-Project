using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<ItemSlot> ItemSlots;

    public ItemSlot FindFirstEmptySlot()
    {
        foreach (var itemSlot in ItemSlots)
        {
            if (!itemSlot.Item)
                return itemSlot;
        }

        return null;
    }
}
