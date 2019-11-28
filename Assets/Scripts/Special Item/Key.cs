using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : ItemBase
{
    GameObject stone;
    GameObject Floor;
    public AudioClip clip;

    private void Start()
    {
        stone = GameObject.Find("KeyCube");
        Floor = GameObject.Find("Floor2");
        Floor.SetActive(false);
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

        Transform target = GameObject.Find("KeyHolder").transform;
        gameObject.transform.position = target.position;
        gameObject.transform.parent = stone.transform;

        stone.GetComponent<Animation>().Play();
        stone.GetComponent<AudioSource>().Play();
        stone.GetComponent<KeyCube>().isActivated = true;
        Invoke("Disappear", 2.2f);
    }

    void Disappear()
    {
        gameObject.SetActive(false);
        Floor.SetActive(true);
        GameObject.FindWithTag("Player").GetComponent<AudioSource>().PlayOneShot(clip);

    }
}
