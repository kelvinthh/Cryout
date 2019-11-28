using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondFloorDoor : MonoBehaviour
{
    private InventoryUI hud;
    [HideInInspector]
    public bool isActivated;
    [HideInInspector]
    public bool isVisited;
    GameObject ebox;
    // Start is called before the first frame update
    void Start()
    {
        isActivated = false;
        isVisited = false;
        hud = GameObject.Find("/Inventory System/Inventory HUDv2").GetComponent<InventoryUI>();
        ebox = GameObject.Find("/Objectives/ElectricalBox");
    }

    // Update is called once per frame
    void Update()
    {
        if (ebox.GetComponent<ElectricBox>().isActivated)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !isActivated)
        {
            hud.DisplayMessage("The electronic lock seems offline");
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
