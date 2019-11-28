using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel : ItemBase
{
    private HealthSystem healthsystem;
    private void Start()
    {
        healthsystem = GameObject.FindWithTag("Player").GetComponent<HealthSystem>();
    }
    public override void OnDrop()
    {
        base.OnDrop();
    }

    public override void OnGrab()
    {
        base.OnGrab();
        healthsystem.SetObjective("Get upstair and leave this horrible place");
    }

    public override void OnUse()
    {
        base.OnUse();
    }

}
