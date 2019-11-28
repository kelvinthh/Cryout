using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crowbar : ItemBase
{
    GameObject vase;
    GameObject basementKey;
    GameObject player;
    [SerializeField]
    AudioClip breakSound;

    private void Start()
    {
        vase = GameObject.Find("/Objectives/Vase");
        basementKey = GameObject.Find("/Objective Items/Basement Key");
        player = GameObject.FindWithTag("Player");
    }

    public override void OnGrab()
    {
        base.OnGrab();
    }

    public override void OnDrop()
    {
        base.OnDrop();
    }

    public override void OnUse()
    {
        base.OnUse();
        vase.SetActive(false);
        player.GetComponent<AudioSource>().PlayOneShot(breakSound);
        basementKey.GetComponent<Collider>().enabled = true;

    }
}
