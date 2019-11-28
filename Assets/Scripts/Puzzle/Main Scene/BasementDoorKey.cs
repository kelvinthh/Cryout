using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BasementDoorKey : ItemBase
{
    private GameObject door;
    private GameObject trigger;
    private GameObject blackpanel;
    [SerializeField]
    private AudioClip doorOpen;

    private void Start()
    {
        door = GameObject.Find("/Objectives/Basement Door");
        blackpanel = GameObject.Find("/Inventory System/Inventory HUDv2/Black Panel");
    }
    public override void OnGrab()
    {
        base.OnGrab();
        GameObject.FindWithTag("Player").GetComponent<HealthSystem>().SetObjective("Unlock the basement door");
    }

    public override void OnDrop()
    {
        base.OnDrop();
    }

    public override void OnUse()
    {
        base.OnUse();
        blackpanel.GetComponent<Animation>().Play();
        GameObject.FindWithTag("Player").GetComponent<AudioSource>().PlayOneShot(doorOpen);
        Invoke("SwitchToBasement", 2);
    }

    void SwitchToBasement()
    {
        SceneManager.LoadScene(3);
    }
}
