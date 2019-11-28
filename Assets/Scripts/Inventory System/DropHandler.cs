using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropHandler : MonoBehaviour, IDropHandler
{
    private Inventory inventory;
    private AudioClip dropsound;
    private AudioClip nodrop;
    private GameObject hud;

    void Start()
    {
        inventory = GameObject.Find("/Inventory System/Inventory").GetComponent<Inventory>();
        dropsound = Resources.Load<AudioClip>("Sounds/ItemDrop");
        nodrop = Resources.Load<AudioClip>("Sounds/415764__thebuilder15__wrong");
        hud = GameObject.Find("/Inventory System/Inventory HUDv2");
    }

    public void OnDrop(PointerEventData eventData)
    {
        RectTransform inventoryBackground = transform as RectTransform;
        if(!RectTransformUtility.RectangleContainsScreenPoint(inventoryBackground, Input.mousePosition, null))
        {
            IItemObject item = eventData.pointerDrag.gameObject.GetComponent<DragHandler>().Item;
            if(item != null && item.IsDroppable)
            {
                inventory.RemoveItem(item);
                GameObject.FindWithTag("Player").GetComponent<AudioSource>().PlayOneShot(dropsound);
                DestroyItemDescription();
                item = null;
            }
            else
            {
                GameObject.FindWithTag("Player").GetComponent<AudioSource>().PlayOneShot(nodrop, 0.5f);
                DestroyItemDescription();
                item = null;
            }
        }
    }

    private void DestroyItemDescription()
    {
        Destroy(hud.transform.GetChild(0).GetChild(0).gameObject);
    }
}
