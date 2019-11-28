using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadableActionsBasement : MonoBehaviour
{
    [SerializeField]
    private Readable[] readables;
    [SerializeField]
    private GameObject[] objectives;
    [SerializeField]
    private GameObject[] enemies;
    private HealthSystem healthsystem;
    private bool generatorObjective;
    private bool enemiesSpawned;
    private bool gateOpened;
    private void Start()
    {
        healthsystem = GameObject.FindWithTag("Player").GetComponent<HealthSystem>();
        Invoke("FirstObjective", 1);
        generatorObjective = false;
        enemiesSpawned = false;
        gateOpened = false;
    }

    private void Update()
    {
        //Once first two readables are read
        if (readables[0].isRead && readables[1].isRead && !generatorObjective)
        {
            healthsystem.SetObjective("Turn off all 4 generators at each corner of the basement");
            generatorObjective = true;
            EnableGenerators();
        }

        //When any one of the generators is disabled
        if(Generators.disabledGenerators >= 1 && !enemiesSpawned)
        {
            for(int i = 0; i < enemies.Length; i++)
            {
                enemies[i].SetActive(true);
                enemiesSpawned = true;
            }
        }

        //When 4 generators are disabled
        if(Generators.disabledGenerators >= 4 && !gateOpened)
        {
            gateOpened = true;
            objectives[0].SetActive(false);
            healthsystem.SetObjective("Grab those oil drums");
            GameObject.Find("/Objective Items/Fuel").GetComponent<Collider>().enabled = true;
        }

        //For debugging: Enable/Remove all enemies in the scene
        if (Input.GetKeyDown(KeyCode.F11))
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].SetActive(true);
            }
        }
        if (Input.GetKeyDown(KeyCode.F12))
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].SetActive(false);
            }
        }
    }

    private void EnableGenerators()
    {
        for (int i = 0; i < 4; i++)
        {
            objectives[2 + i].GetComponent<Collider>().enabled = true;
        }
    }

    void FirstObjective()
    {
        healthsystem.SetObjective("Find fuel for your van");
    }
}
