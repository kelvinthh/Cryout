using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key_Tutorial : ItemBase
{
    GameObject door;
    GameObject trigger;

    private void Start()
    {
        door = GameObject.Find("BrickHouse_Door_A");
        trigger = GameObject.Find("/Tutorial Triggers/EnableEnemy");
        trigger.SetActive(false);
    }
    public override void OnGrab()
    {
        base.OnGrab();
        GameObject.FindWithTag("Player").GetComponent<HealthSystem>().SetObjective("Return to the brick house");
        trigger.SetActive(true);
        Invoke("ShowTutorial", 2);
    }

    public override void OnDrop()
    {
        base.OnDrop();
    }

    public override void OnUse()
    {
        base.OnUse();
        door.transform.rotation = Quaternion.Euler(0, 90, 0);
        door.GetComponent<KeyDoor>().isActivated = true;
        GameObject.FindWithTag("Player").GetComponent<HealthSystem>().SetObjective("Enter the brick house.");
    }

    void ShowTutorial()
    {
        GameObject.Find("/Tutorial HUD").GetComponent<TutorialHUD>().ShowTutoral(3);
    }
}
