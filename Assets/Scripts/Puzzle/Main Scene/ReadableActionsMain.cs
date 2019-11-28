using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadableActionsMain : MonoBehaviour
{
    [SerializeField]
    private Readable[] readables;
    [SerializeField]
    private GameObject[] objectives;
    private HealthSystem healthsystem;
    private GameObject enemy;
    private void Start()
    {

        healthsystem = GameObject.FindWithTag("Player").GetComponent<HealthSystem>();
        Invoke("FirstObjective", 1);
        enemy = GameObject.FindWithTag("Enemy");
    }

    private void Update()
    {
        //Van
        if (readables[0].isRead && !readables[0].isObjectiveChanged)
        {
            if(!GameObject.Find("/Objective Items/House Main Door Key").GetComponent<HouseMainDoorKey>().isGrabbed)
            {
                healthsystem.SetObjective("Enter the house to find fuel");
            }
            readables[0].isObjectiveChanged = true;
        }

        //Local News Today
        if (readables[1].isRead && !readables[1].isObjectiveChanged)
        {
            //Invoke("EnableEnemy", 10);
        }

        //Mr. Vence's Note
        if (readables[2].isRead && !readables[2].isObjectiveChanged)
        {
            healthsystem.SetObjective("Get to the basement");
            readables[2].isObjectiveChanged = true;
            readables[3].enabled = true;
            GameObject.Find("/Objectives/Basement Door").GetComponent<BasementDoor>().enabled = true;
        }

        //Mrs. Vence's Note
        if (readables[3].isRead && objectives[1].GetComponent<BasementDoor>().isVisited && !readables[3].isObjectiveChanged)
        {
            healthsystem.SetObjective("Navigate to the second floor");
            readables[3].isObjectiveChanged = true;
            GameObject.Find("Objectives/Second Floor Door").GetComponent<SecondFloorDoor>().enabled = true;
        }

        //E-lock User Manual
        if (readables[4].isRead && objectives[3].GetComponent<SecondFloorDoor>().isVisited && !readables[4].isObjectiveChanged)
        {
            healthsystem.SetObjective("Find a way to turn the power back on");
            readables[4].isObjectiveChanged = true;
            GameObject.Find("Objectives/ElectricalBox").GetComponent<ElectricBox>().enabled = true;
        }

        //For Debugging: Enable/Disable Enemy
        if (Input.GetKeyDown(KeyCode.F11))
        {
            enemy.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.F12))
        {
            enemy.SetActive(false);
        }

    }

    void FirstObjective()
    {
        healthsystem.SetObjective("Check out your van");
    }
}
