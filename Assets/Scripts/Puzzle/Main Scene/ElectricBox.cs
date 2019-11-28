using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricBox : MonoBehaviour
{
    private InventoryUI hud;
    [HideInInspector]
    public bool isActivated;
    private bool isInRange;

    [SerializeField]
    private Light greenlight;
    [SerializeField]
    private Light redlight;
    [SerializeField]
    private AudioClip switchOn;
    GameObject fire;
    [SerializeField]
    GameObject lights;

    // Start is called before the first frame update
    void Start()
    {
        isActivated = false;
        isInRange = false;
        hud = GameObject.Find("/Inventory System/Inventory HUDv2").GetComponent<InventoryUI>();
        fire = GameObject.Find("/Objectives").transform.GetChild(4).gameObject;
    }

    private void Update()
    {
        if (isActivated)
        {
            greenlight.enabled = true;
            redlight.enabled = false;
        }
        else
        {
            greenlight.enabled = false;
            redlight.enabled = true;
        }

        if (!isActivated && isInRange && Input.GetKeyDown(KeyCode.E))
        {
            isActivated = true;
            GameObject.Find("/Objective Items/Fire Extinguisher").GetComponent<Collider>().enabled = true;
            GameObject.FindWithTag("Player").GetComponent<AudioSource>().PlayOneShot(switchOn);
            fire.SetActive(true);
            lights.SetActive(true);
            GameObject.FindWithTag("Player").GetComponent<HealthSystem>().SetObjective("Navigate to the second floor");
            Invoke("DelayObjective", 3f);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !isActivated)
        {
            hud.DisplayMessage("Press E to activate " + gameObject.name);
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

    private void DelayObjective()
    {
        GameObject.FindWithTag("Player").GetComponent<HealthSystem>().SetObjective("Find something to put out the fire");
    }
}
