using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseMainDoor : MonoBehaviour
{
    private Inventory inventory;
    [SerializeField]
    private HouseMainDoorKey item;
    private InventoryUI hud;
    [HideInInspector]
    public bool isActivated;
    private bool isObjectiveChanged;
    // Start is called before the first frame update
    void Start()
    {
        isActivated = false;
        inventory = GameObject.FindWithTag("Player").GetComponent<InventoryOnPlayer>().inventory;
        hud = GameObject.Find("/Inventory System/Inventory HUDv2").GetComponent<InventoryUI>();
        isObjectiveChanged = false;
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

            if (!isObjectiveChanged)
            {
                //GameObject.FindWithTag("Player").GetComponent<HealthSystem>().SetObjective("Head to the barn house to find the key.");
                isObjectiveChanged = true;
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
