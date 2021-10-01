using System.Linq;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{
    [SerializeField] private Image desiredItemImage;
    [SerializeField] private Button satisfyButton;

    private Inventory inventory;

    public string DesiredItem;

    void Awake()
    {
        inventory = FindObjectOfType<Inventory>();
        Items items = new Items();
        if (DesiredItem != "")
            desiredItemImage.sprite = items.ItemList.FirstOrDefault(item => item.Name == DesiredItem).Sprite;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(inventory.CheckForItem(DesiredItem))
                satisfyButton.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
            satisfyButton.gameObject.SetActive(false);
    }

    public void Satisfy()
    {
        inventory.RemoveItem(DesiredItem);
        Destroy(this.gameObject);
    }
}
