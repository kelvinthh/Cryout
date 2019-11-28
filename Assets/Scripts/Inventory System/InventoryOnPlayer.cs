using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class InventoryOnPlayer : MonoBehaviour
{
    public Inventory inventory;
    private InventoryUI hud;

    private IItemObject pickUpItem = null;

    private TextMeshProUGUI UIText;
    private Color BackgroundColor;
    private Color BlackPanel;
    private AudioClip OpenInventory;
    private TextMeshProUGUI toptext;

    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.Find("/Inventory System/Inventory").GetComponent<Inventory>();
        hud = GameObject.Find("/Inventory System/Inventory HUDv2").GetComponent<InventoryUI>();
        OpenInventory = Resources.Load<AudioClip>("Sounds/270392__littlerobotsoundfactory__inventory-open-01");
        UIText = GameObject.Find("/Inventory System/Inventory HUDv2").GetComponentInChildren<TextMeshProUGUI>();
        BackgroundColor = GameObject.Find("/Inventory System/Inventory HUDv2/Background").GetComponent<Image>().color;
        BlackPanel = GameObject.Find("/Inventory System/Inventory HUDv2/Black Panel").GetComponent<Image>().color;
        toptext = GameObject.Find("/Inventory System/Inventory HUDv2/Inventory Top Text").GetComponent<TextMeshProUGUI>();
        toptext.enabled = false;

        //Block the cursor on game starts
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && GetComponent<HealthSystem>().IsAlive())
        {
            ShowInventory();
        }

        if (Input.GetKeyDown(KeyCode.E) && pickUpItem != null && GetComponent<HealthSystem>().IsAlive())
        {
            inventory.AddItem(pickUpItem);
            hud.CloseMessage();
            ToggleHint("You have found " + pickUpItem.Name);
            pickUpItem = null;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        IItemObject item = other.GetComponent<IItemObject>();
        if(item != null)
        {
            pickUpItem = item;
            hud.DisplayMessage("Press E to pick up '" + item.Name + "'");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IItemObject item = other.GetComponent<IItemObject>();
        if (item != null)
        {
            pickUpItem = null;
            hud.CloseMessage();
        }
    }
    public void ShowInventory()
    {

        Cursor.visible = !Cursor.visible;
        gameObject.GetComponent<AudioSource>().PlayOneShot(OpenInventory);
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            //Disable the cursor movement and show the cursor
            GetComponent<FirstPersonController>().DisableMouseLook(true);
            Cursor.lockState = CursorLockMode.None;

            BlackPanel.a = 0.5f;
            BackgroundColor.a = 1f;
            GameObject.Find("Background").GetComponent<Image>().color = BackgroundColor;
            GameObject.Find("Black Panel").GetComponent<Image>().color = BlackPanel;
            toptext.enabled = true;
        }
        else
        {
            //Enable the cursor movement and hide the cursor
            GetComponent<FirstPersonController>().DisableMouseLook(false);
            Cursor.lockState = CursorLockMode.Locked;

            BlackPanel.a = 0f;
            BackgroundColor.a = 0.2f;
            GameObject.Find("Background").GetComponent<Image>().color = BackgroundColor;
            GameObject.Find("Black Panel").GetComponent<Image>().color = BlackPanel;
            toptext.enabled = false;
        }
        Debug.Log("Now the cursor mode is now " + Cursor.lockState);
    }

    public void ToggleHint(string text)
    {
        UIText.enabled = true;
        UIText.SetText(text);
        CancelInvoke("DisableText");
        Invoke("DisableText", 2);
    }

    public void DisableText()
    {
        UIText.enabled = false;
    }
}
