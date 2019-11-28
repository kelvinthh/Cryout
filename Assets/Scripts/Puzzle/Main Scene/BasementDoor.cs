using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasementDoor : MonoBehaviour
{
    private Inventory inventory;
    [SerializeField]
    private BasementDoorKey item;
    private InventoryUI hud;
    [HideInInspector]
    public bool isActivated;
    [HideInInspector]
    public bool isVisited;
    private bool objectiveChanged;

    // Start is called before the first frame update
    void Start()
    {
        isActivated = false;
        inventory = GameObject.FindWithTag("Player").GetComponent<InventoryOnPlayer>().inventory;
        hud = GameObject.Find("/Inventory System/Inventory HUDv2").GetComponent<InventoryUI>();
        objectiveChanged = false;
        isVisited = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !isActivated)
        {
            if (inventory.itemList.Contains(item))
            {
                item.IsUsable = true;
                hud.DisplayMessage("Use your '" + item.Name + "' to unlock " + gameObject.name);
            }
            else
            {
                hud.DisplayMessage("You need a '" + item.Name + "' to unlock " + gameObject.name);
            }

            if (!objectiveChanged)
            {
                objectiveChanged = true;
            }
            isVisited = true;
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
