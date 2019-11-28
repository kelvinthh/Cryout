using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseMainDoorKey : ItemBase
{
    GameObject mainDoor;
    GameObject enemy;
    [HideInInspector]
    public bool isGrabbed;

    private void Start()
    {
        mainDoor = GameObject.Find("/Objectives/House Main Door");
        enemy = GameObject.FindWithTag("Enemy");
        enemy.SetActive(false);
        isGrabbed = false;
    }
    public override void OnGrab()
    {
        base.OnGrab();
        GameObject.FindWithTag("Player").GetComponent<HealthSystem>().SetObjective("Open the House Main Door");
        isGrabbed = true;
    }

    public override void OnDrop()
    {
        base.OnDrop();
    }

    public override void OnUse()
    {
        base.OnUse();
        mainDoor.transform.rotation = Quaternion.Euler(0, 90, 0);
        mainDoor.GetComponent<HouseMainDoor>().isActivated = true;
        GameObject.FindWithTag("Player").GetComponent<HealthSystem>().SetObjective("Search the house for fuel.");
        Invoke("EnableEnemy", 6);
    }

    private void EnableEnemy()
    {
        enemy.SetActive(true);
    }
}
