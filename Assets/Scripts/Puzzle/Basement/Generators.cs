using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generators : MonoBehaviour
{
    private InventoryUI hud;
    [HideInInspector]
    public bool isActivated;
    private bool isInRange;
    private Light greenlight;
    private Light redlight;
    [SerializeField]
    private AudioClip switchOn;
    public static int disabledGenerators;
    // Start is called before the first frame update
    void Start()
    {
        isActivated = false;
        isInRange = false;
        greenlight = transform.GetChild(0).GetComponent<Light>();
        redlight = transform.GetChild(1).GetComponent<Light>();
        hud = GameObject.Find("/Inventory System/Inventory HUDv2").GetComponent<InventoryUI>();
        disabledGenerators = 0;
    }

    void Update()
    {
        if (isActivated)
        {
            greenlight.enabled = false;
            redlight.enabled = true;
        }
        else
        {
            greenlight.enabled = true;
            redlight.enabled = false;
        }

        if (!isActivated && isInRange && Input.GetKeyDown(KeyCode.E))
        {
            isActivated = true;
            GameObject.FindWithTag("Player").GetComponent<AudioSource>().PlayOneShot(switchOn);
            disabledGenerators++;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !isActivated)
        {
            hud.DisplayMessage("Press E to deactivate");
            isInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            hud.CloseMessage();
            isInRange = false;
        }
    }
}
