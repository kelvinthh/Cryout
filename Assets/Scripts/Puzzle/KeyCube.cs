using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyCube : MonoBehaviour
{
    private Inventory inventory;
    public Key item;
    private InventoryUI hud;
    public bool isActivated;

    private void Start()
    {
        isActivated = false;
        inventory = GameObject.FindWithTag("Player").GetComponent<InventoryOnPlayer>().inventory;
        hud = GameObject.Find("Inventory HUDv2").GetComponent<InventoryUI>();
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !isActivated)
        {
            if (inventory.itemList.Contains(item)){
                item.IsUsable = true;
                hud.DisplayMessage("Use your '" + item.Name + "' to proceed.");
            }
            else
            {
                hud.DisplayMessage("You need a '" + item.Name + "' to proceed.");
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
