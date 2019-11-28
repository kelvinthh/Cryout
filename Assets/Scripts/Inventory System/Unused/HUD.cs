using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Inventory inventory;
    public GameObject PickupPanel;

    private void Start()
    {
        inventory.ItemAdded += InventoryScript_ItemAdded;
        inventory.ItemRemoved += InventoryScript_ItemRemoved;
        inventory.ItemUsed += Inventory_ItemUsed;
    }

    private void InventoryScript_ItemAdded(object sender, InventoryEventArgs e)
    {
        Transform inventoryBackground = transform.Find("Background");
        foreach (Transform Slot in inventoryBackground)
        {
            Transform imageTransform = Slot.GetChild(0).GetChild(0);
            Image image = imageTransform.GetComponent<Image>();
            DragHandler draggHandler = imageTransform.GetComponent<DragHandler>();

            //If empty slot is found
            if (!image.enabled)
            {
                image.enabled = true;
                image.sprite = e.Item.Image;

                //Store a reference to the object
                draggHandler.Item = e.Item;
                break;
            }

        }
    }

    private void InventoryScript_ItemRemoved(object sender, InventoryEventArgs e)
    {
        Transform inventoryBackground = transform.Find("Background");
        foreach(Transform Slot in inventoryBackground)
        {
            Transform imageTransform = Slot.GetChild(0).GetChild(0);
            Image image = imageTransform.GetComponent<Image>();
            DragHandler dragHandler = imageTransform.GetComponent<DragHandler>();

            if (dragHandler.Item.Equals(e.Item))
            {
                image.enabled = false;
                image.sprite = null;
                dragHandler.Item = null;
                Debug.Log("Image Disabled");
                break;
            }
        }
    }

    private void Inventory_ItemUsed(object sender, InventoryEventArgs e)
    {
        Transform inventoryBackground = transform.Find("Background");
        foreach (Transform Slot in inventoryBackground)
        {
            Transform imageTransform = Slot.GetChild(0).GetChild(0);
            Image image = imageTransform.GetComponent<Image>();
            DragHandler dragHandler = imageTransform.GetComponent<DragHandler>();

            if (dragHandler.Item.Equals(e.Item))
            {
                image.enabled = false;
                image.sprite = null;
                dragHandler.Item = null;
                break;
            }
        }
    }
    
    public void DisplayMessage(string text)
    {
        PickupPanel.SetActive(true);
        PickupPanel.GetComponentInChildren<TextMeshProUGUI>().SetText(text);
    }

    public void CloseMessage()
    {
        PickupPanel.SetActive(false);
    }
}
