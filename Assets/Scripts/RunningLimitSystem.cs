using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class RunningLimitSystem : MonoBehaviour
{
    [SerializeField]
    float energy;
    [SerializeField]
    float maxEnergy;
    [SerializeField]
    float regeneratingRate;
    [SerializeField]
    float losingRate;

    // Start is called before the first frame update
    void Start()
    {
        energy = maxEnergy;
    }

    // Update is called once per frame
    void Update()
    {
        RestrictEnergy();
        Regenerate();
    }

    private void Regenerate()
    {
        if (!Input.GetKey(KeyCode.LeftShift))
        {
            energy += Time.fixedDeltaTime * regeneratingRate;
        }
        else
        {
            energy -= Time.fixedDeltaTime * losingRate;
        }
    }

    private void RestrictEnergy()
    {
        if (energy > maxEnergy)
        {
            energy = maxEnergy;
        }

        if (energy <= 0)
        {
            energy = 0;
            GetComponent<FirstPersonController>().isTired = true;
        }
        else
        {
            GetComponent<FirstPersonController>().isTired = false;
        }
    }
}
