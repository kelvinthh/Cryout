using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Click : MonoBehaviour
{
    public Inventory inventory;
    private AudioClip nodrop;

    void Start()
    {
        nodrop = Resources.Load<AudioClip>("Sounds/415764__thebuilder15__wrong");
    }

    public void ButtonClick()
    {
        DragHandler draghandler = gameObject.transform.Find("ItemImage").GetComponent<DragHandler>();
        IItemObject item = draghandler.Item;
        if (item != null && item.IsUsable)
        {
            Debug.Log("Fucking shit " + gameObject.transform.Find("ItemImage").GetComponent<Image>().enabled);
            inventory.UseItem(item);
        }
        else
        {
            GameObject.FindWithTag("Player").GetComponent<AudioSource>().PlayOneShot(nodrop, 0.5f);
        }
    }
}
