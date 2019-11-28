using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour, IItemObject
{
    public string Name
    {
        get
        {
            return gameObject.name;
        }
    }

    [SerializeField]
    private string _Description;
    public string Description
    {
        get
        {
            return _Description;
        }
    }

    [SerializeField]
    private Sprite _Image;
    public Sprite Image
    {
        get
        {
            return _Image;
        }
    }

    [SerializeField]
    private AudioClip _PickupSound;
    public AudioClip PickupSound
    {
        get
        {
            return _PickupSound;
        }
    }

    [SerializeField]
    private bool _IsDroppable;
    public bool IsDroppable
    {
        get
        {
            return _IsDroppable;
        }
        set
        {
            _IsDroppable = value;
        }
    }

    [SerializeField]
    private bool _IsUsable;
    public bool IsUsable
    {
        get
        {
            return _IsUsable;
        }
        set
        {
            _IsUsable = value;
        }
    }

    public virtual void OnDrop()
    {
        Vector3 playerPos = GameObject.FindWithTag("Player").transform.position;
        Vector3 direction = -GameObject.FindWithTag("Player").transform.up;

        Ray ray = new Ray(playerPos, direction);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 30f))
        {
            gameObject.SetActive(true);
            gameObject.transform.position = hit.point;
        }
    }

    public virtual void OnGrab()
    {
        gameObject.SetActive(false);
        Debug.Log("Picked up an item!");
        GameObject.FindWithTag("Player").GetComponent<AudioSource>().PlayOneShot(PickupSound);
    }

    public virtual void OnUse()
    {
        //Close the Pickup Panel message
        GameObject.Find("/Inventory System/Inventory HUDv2").GetComponent<InventoryUI>().CloseMessage();
        GameObject.FindWithTag("Player").GetComponent<AudioSource>().PlayOneShot(PickupSound);
        GameObject.FindWithTag("Player").GetComponent<InventoryOnPlayer>().ToggleHint("You have used the " + Name + "!");
        GameObject.FindWithTag("Player").GetComponent<InventoryOnPlayer>().ShowInventory(); // Close the inventory

    }
}
