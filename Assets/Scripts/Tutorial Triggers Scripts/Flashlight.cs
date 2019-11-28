using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    TutorialHUD tutorial;
    bool triggered;
    // Start is called before the first frame update
    void Start()
    {
        tutorial = GameObject.Find("/Tutorial HUD").GetComponent<TutorialHUD>();
        triggered = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!triggered)
        {
            tutorial.ShowTutoral(1);
            triggered = true;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        tutorial.ShowTutoral(2);
        Destroy(gameObject);
    }
}
