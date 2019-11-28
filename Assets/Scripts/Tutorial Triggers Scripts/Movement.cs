using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    TutorialHUD tutorial;
    bool triggered;
    // Start is called before the first frame update
    void Start()
    {
        tutorial = GameObject.Find("/Tutorial HUD").GetComponent<TutorialHUD>();
        triggered = false;
        Invoke("Show", 1);
    }

    private void Show()
    {
        if (!triggered)
        {
            GameObject.FindWithTag("Player").GetComponent<HealthSystem>().SetObjective("Enter the brick house");
            tutorial.ShowTutoral(0);
            triggered = true;
        }

    }
}
