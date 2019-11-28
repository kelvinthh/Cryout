using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrigger : MonoBehaviour
{
    private Inventory inventory;
    [SerializeField]
    private Extinguisher item;
    private InventoryUI hud;
    [HideInInspector]
    public bool isActivated;
    // Start is called before the first frame update
    void Start()
    {
        isActivated = false;
        inventory = GameObject.FindWithTag("Player").GetComponent<InventoryOnPlayer>().inventory;
        hud = GameObject.Find("/Inventory System/Inventory HUDv2").GetComponent<InventoryUI>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !isActivated)
        {
            if (inventory.itemList.Contains(item))
            {
                item.IsUsable = true;
                hud.DisplayMessage("Use your '" + item.Name + "' to put out the fire!");
            }
            else
            {
                hud.DisplayMessage("Find something to put out the fire!");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (inventory.itemList.Contains(item))
            {
                item.IsUsable = false;
            }
            hud.CloseMessage();
        }
    }
}
