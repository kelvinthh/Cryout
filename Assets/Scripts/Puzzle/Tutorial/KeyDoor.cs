using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : MonoBehaviour
{
    private Inventory inventory;
    [SerializeField]
    private Key_Tutorial item;
    private InventoryUI hud;
    [HideInInspector]
    public bool isActivated;
    private bool objectiveChanged;
    // Start is called before the first frame update
    void Start()
    {
        isActivated = false;
        inventory = GameObject.FindWithTag("Player").GetComponent<InventoryOnPlayer>().inventory;
        hud = GameObject.Find("/Inventory System/Inventory HUDv2").GetComponent<InventoryUI>();
        objectiveChanged = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !isActivated)
        {
            if (inventory.itemList.Contains(item))
            {
                item.IsUsable = true;
                hud.DisplayMessage("Use your '" + item.Name + "' to unlock the door.");
            }
            else
            {
                hud.DisplayMessage("You need a '" + item.Name + "' to unlock the door.");
            }

            if (!objectiveChanged)
            {
                GameObject.FindWithTag("Player").GetComponent<HealthSystem>().SetObjective("Head to the barn house to find the key.");
                objectiveChanged = true;
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
