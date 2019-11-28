using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vase : MonoBehaviour
{
    private Inventory inventory;
    [SerializeField]
    private Crowbar item;
    private InventoryUI hud;
    [HideInInspector]
    public bool isActivated;
    [HideInInspector]
    public bool isVisited;

    // Start is called before the first frame update
    void Start()
    {
        isActivated = false;
        inventory = GameObject.FindWithTag("Player").GetComponent<InventoryOnPlayer>().inventory;
        hud = GameObject.Find("/Inventory System/Inventory HUDv2").GetComponent<InventoryUI>();
        isVisited = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !isActivated)
        {
            if (inventory.itemList.Contains(item))
            {
                item.IsUsable = true;
                hud.DisplayMessage("Use your '" + item.Name + "' to smash the " + gameObject.name);
            }
            else
            {
                hud.DisplayMessage("Your hand can't reach the bottom! Find something to break the " + gameObject.name);
                GameObject.Find("/Objective Items/Crowbar").GetComponent<Collider>().enabled = true;
            }
            isVisited = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            hud.CloseMessage();
        }
    }
}
