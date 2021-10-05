using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private RectTransform background;
    [SerializeField] private RectTransform craftingGrid;
    [SerializeField] private Button inventoryButton;

    [SerializeField] private CraftingSystem craftingSystem;
    [SerializeField] private GarbageCollection garbageCollection;

    public void ShowCollectionScreen()
    {
        inventoryButton.gameObject.SetActive(false);
        background.gameObject.SetActive(true);
        garbageCollection.gameObject.SetActive(true);
        garbageCollection.RespawnItem();
    }

    public void ShowCraftingScreen()
    {
        inventoryButton.gameObject.SetActive(false);
        background.gameObject.SetActive(true);
        craftingGrid.gameObject.SetActive(true);
        craftingSystem.CraftingActive = true;
    }

    public void CloseCraftingScreen()
    {
        craftingSystem.CraftingActive = false;
        Inventory inventory = GetComponent<Inventory>();
        foreach (var slot in craftingSystem.craftingSlots)
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

        if (garbageCollection.collectionSlot)
        {
            garbageCollection.collectionSlot.KillTheChildren();
        }

        CloseAll();
    }

    public void OpenInventory()
    {
        inventoryButton.gameObject.SetActive(false);
        background.gameObject.SetActive(true);
    }

    private void CloseAll()
    {
        background.gameObject.SetActive(false);
        craftingGrid.gameObject.SetActive(false);
        inventoryButton.gameObject.SetActive(true);
        garbageCollection.gameObject.SetActive(false);
    }
}
