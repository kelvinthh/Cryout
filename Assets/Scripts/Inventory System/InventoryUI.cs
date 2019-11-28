using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    private Inventory inventory;
    private bool UIUpToDate;
    [SerializeField]
    private GameObject instantiateObject;
    [SerializeField]
    private GameObject PickupPanel;

    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        UIUpToDate = false;
        inventory.ItemAdded += InventoryScript_ItemAdded;
        inventory.ItemRemoved += InventoryScript_ItemRemoved;
        inventory.ItemUsed += Inventory_ItemUsed;
    }

    void Update()
    {
        GenerateUI();
    }

    private void InventoryScript_ItemAdded(object sender, InventoryEventArgs e)
    {
        RefreshUI();
    }

    private void InventoryScript_ItemRemoved(object sender, InventoryEventArgs e)
    {
        RefreshUI();
    }

    private void Inventory_ItemUsed(object sender, InventoryEventArgs e)
    {
        RefreshUI();
    }

    private void GenerateUI()
    {
        if (!UIUpToDate)
        {
            for (int i = 0; i < inventory.itemList.Count; i++)
            {
                var sprite = Instantiate(instantiateObject, gameObject.transform.GetChild(1));
                DragHandler dragHandler = sprite.transform.GetChild(0).GetChild(0).GetComponent<DragHandler>();
                sprite.transform.GetChild(0).GetChild(0).GetComponent<Image>().enabled = true;
                sprite.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = inventory.itemList[i].Image;
                dragHandler.Item = inventory.itemList[i];
            }
            UIUpToDate = true;
        }
    }

    private void RefreshUI()
    {
        //Destory every child gameobject under Background
        for(int i = 0; i < gameObject.transform.GetChild(1).childCount; i++)
        {
            Destroy(gameObject.transform.GetChild(1).GetChild(i).gameObject);
        }

        UIUpToDate = false;
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
