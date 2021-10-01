using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingSystem : MonoBehaviour
{
    [SerializeField] private ItemSlot topLeftSlot;
    [SerializeField] private ItemSlot topCentreSlot;
    [SerializeField] private ItemSlot topRightSlot;
    [SerializeField] private ItemSlot midLeftSlot;
    [SerializeField] private ItemSlot midCentreSlot;
    [SerializeField] private ItemSlot midRightSlot;
    [SerializeField] private ItemSlot botLeftSlot;
    [SerializeField] private ItemSlot botCentreSlot;
    [SerializeField] private ItemSlot botRightSlot;
    [SerializeField] private ItemSlot resultSlot;

    private List<ItemSlot> craftingSlots;
    private Items items;
    private Inventory inventory;

    void Awake()
    {
        inventory = GetComponentInParent<Inventory>();
        craftingSlots = new List<ItemSlot>()
        {
            topLeftSlot, topCentreSlot, topRightSlot, midLeftSlot, midCentreSlot, midRightSlot, botLeftSlot, botCentreSlot, botRightSlot
        };
        items = new Items();
    }

    public void Craft()
    {
        if (resultSlot.Item)
        { 
            Destroy(resultSlot.Item.gameObject);
            Debug.Log("Hi");
        }
        string craftingString = GetCraftingString();
        ShowCraftableItem(craftingString);
        
    }

    private string GetCraftingString()
    {
        string result = "";
        foreach (var craftingSlot in craftingSlots)
        {
            if (craftingSlot.Item)
                result += craftingSlot.Item.itemData.Name; 
            else
                result += "0";

        }
        Debug.Log(result);
        return result;
    }

    private void ShowCraftableItem(string craftingString)
    {
        foreach (var item in items.ItemList)
        {
            if (item.Recipe == craftingString)
            {
                Debug.Log("Activate.");
                Item newItem = Instantiate(resultSlot.itemPrefab, resultSlot.transform).GetComponent<Item>();
                newItem.InitializeItem(item.Name);
                resultSlot.Item = newItem;
            }
        }
    }

    public void CraftNewItem()
    {
        foreach (var slot in craftingSlots)
        {
            if (slot.Item)
                Destroy(slot.Item.gameObject);
        }
    }

    public void CloseCraftingScreen()
    {
        gameObject.SetActive(false);
        foreach (var slot in craftingSlots)
        {
            if (slot.Item)
            {
                ItemSlot emptySlot = inventory.FindFirstEmptySlot();
                slot.Item.transform.SetParent(emptySlot.transform);
                emptySlot.Item = slot.Item;
                emptySlot.Item.transform.localPosition = new Vector3(0, 0, 0);
                DragDrop2 dragDrop2 = emptySlot.Item.GetComponent<DragDrop2>();
                dragDrop2.currentSlot = emptySlot;
                slot.Item = null;
            }
            //put all items back into inventory
        }
    }
}
