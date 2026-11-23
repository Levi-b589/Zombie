using UnityEngine;
using UnityEngine.UI;

public class InventoryBarUI : MonoBehaviour
{
    [SerializeField] private Image[] slotIcons;
    [SerializeField] private GameObject[] slotObjects;
    private inventory_hotbar[] items;

    //When the game starts the icons dont show until you pick it up
    private void Awake()
    {
        items = new inventory_hotbar[slotIcons.Length];

        foreach (Image icon in slotIcons)
        {
            if (icon != null)
            {
                icon.enabled = false;
            }

        }
        foreach (GameObject slot in slotObjects)
        {
            slot.SetActive(false);
        }

    }

    //Allows the item to be added 
    public bool AddItem(inventory_hotbar item)
    {
        if (item == null)
        {
            return false;
        }

        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
            {
                items[i] = item;
                slotIcons[i].sprite = item.item_Sprite;
                slotIcons[i].enabled = item.item_Sprite != null;
                return true;
            }
            slotObjects[i].SetActive(true);
        }

        Debug.Log("Inventory bar is full.");
        return false;
    }
    //Allows you to drop the item and gets rid of the image for the inventory
    public void RemoveItem(inventory_hotbar item)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == item)
            {
                items[i] = null;
                slotIcons[i].sprite = null;
                slotIcons[i].enabled = false;
                slotObjects[i].SetActive(false);
                return;
            }
        }

    }
}