using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndDoor : MonoBehaviour
{
    private Inventory inventory;
    [SerializeField]
    private Fuel item;
    private GameObject blackpanel;
    private InventoryUI hud;
    [HideInInspector]
    public bool isActivated;
    private bool isInRange;
    [SerializeField]
    private AudioClip doorOpen;

    // Start is called before the first frame update
    void Start()
    {
        isActivated = false;
        inventory = GameObject.FindWithTag("Player").GetComponent<InventoryOnPlayer>().inventory;
        hud = GameObject.Find("/Inventory System/Inventory HUDv2").GetComponent<InventoryUI>();
        blackpanel = GameObject.Find("/Inventory System/Inventory HUDv2/Black Panel");
        isInRange = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (inventory.itemList.Contains(item) && Input.GetKeyDown(KeyCode.E) && isInRange)
        {
            hud.CloseMessage();
            blackpanel.GetComponent<Animation>().Play();
            GameObject.FindWithTag("Player").GetComponent<AudioSource>().PlayOneShot(doorOpen);
            Invoke("ChangeScene", 2);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !isActivated)
        {
            isInRange = true;
            if (inventory.itemList.Contains(item))
            {
                hud.DisplayMessage("Press 'E' to leave!");
            }
            else
            {
                hud.DisplayMessage("You need a '" + item.Name + "' to leave this place!");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isInRange = false;
            hud.CloseMessage();
        }
    }

    void ChangeScene()
    {
        SceneManager.LoadScene(4);
    }
}
