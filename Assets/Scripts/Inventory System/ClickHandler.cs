using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickHandler : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Inventory inventory;
    private AudioClip nouse;
    private AudioClip click;
    [SerializeField]
    private GameObject itemDesc;
    private GameObject hud;

    void Start()
    {
        nouse = Resources.Load<AudioClip>("Sounds/415764__thebuilder15__wrong");
        click = Resources.Load<AudioClip>("Sounds/271140__strange-dragoon__menu-navigation");
        inventory = GameObject.Find("/Inventory System/Inventory").GetComponent<Inventory>();
        hud = GameObject.Find("/Inventory System/Inventory HUDv2");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            DragHandler draghandler = gameObject.transform.GetChild(0).GetComponent<DragHandler>();
            IItemObject item = draghandler.Item;
            if (item != null && item.IsUsable)
            {
                inventory.UseItem(item);
                DestroyItemDescription();
            }
            else
            {
                GameObject.FindWithTag("Player").GetComponent<AudioSource>().PlayOneShot(nouse, 0.5f);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GameObject.FindWithTag("Player").GetComponent<AudioSource>().PlayOneShot(click);
        DragHandler draghandler = gameObject.transform.GetChild(0).GetComponent<DragHandler>();
        IItemObject item = draghandler.Item;
        GameObject tempObject = Instantiate(itemDesc, GameObject.Find("Inventory HUDv2").transform.GetChild(0));
        tempObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(item.Name + "\n" + item.Description);
        tempObject.transform.GetChild(1).GetComponent<Image>().sprite = item.Image;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DestroyItemDescription();
    }

    private void DestroyItemDescription()
    {
        Destroy(hud.transform.GetChild(0).GetChild(0).gameObject);
    }
}
