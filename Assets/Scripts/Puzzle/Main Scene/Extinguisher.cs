using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extinguisher : ItemBase
{
    GameObject door;
    GameObject fire;
    GameObject player;
    [SerializeField]
    AudioClip putOutFireSound;

    private void Start()
    {
        door = GameObject.Find("/Objectives/House Main Door");
        fire = GameObject.Find("/Objectives").transform.GetChild(4).gameObject;
        player = GameObject.FindWithTag("Player");
    }
    public override void OnGrab()
    {
        base.OnGrab();
        player.GetComponent<HealthSystem>().SetObjective("Put out the fire");
    }

    public override void OnDrop()
    {
        base.OnDrop();
    }

    public override void OnUse()
    {
        base.OnUse();
        //TODO: Put out the fire
        player.GetComponent<AudioSource>().PlayOneShot(putOutFireSound);
        fire.transform.GetChild(1).gameObject.SetActive(false);
        Invoke("PutOutFire", 2f);

    }

    private void PutOutFire()
    {
        fire.transform.GetChild(0).GetComponent<AudioSource>().Stop();
        fire.SetActive(false);
        player.GetComponent<HealthSystem>().SetObjective("Get upstairs and find the basement door key");
    }
}
