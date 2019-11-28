using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Readable : MonoBehaviour
{
    private InventoryUI inventoryUI;
    private ReadableItemSystem readableItemSystem;
    [TextArea(10,10)]
    public string content;
    private bool isInRange;
    [HideInInspector]
    public bool isRead;
    [HideInInspector]
    public bool isObjectiveChanged; //It will be enable so the objective can only be changed once.

    // Start is called before the first frame update
    void Start()
    {
        inventoryUI = GameObject.Find("/Inventory System/Inventory HUDv2").GetComponent<InventoryUI>();
        readableItemSystem = GameObject.Find("/Readable Item System").GetComponent<ReadableItemSystem>();
        isInRange = false;
        isRead = false;
        isObjectiveChanged = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            readableItemSystem.DisplayText(content);
            inventoryUI.CloseMessage();
        }

        if (isInRange && Input.GetKeyDown(KeyCode.Mouse0))
        {
            inventoryUI.DisplayMessage("Press 'E' to examinate '" + gameObject.name + "'");
            isRead = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inventoryUI.DisplayMessage("Press 'E' to examinate '" + gameObject.name + "'");
            isInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inventoryUI.CloseMessage();
            isInRange = false;
        }
    }
}
