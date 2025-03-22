using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LockNode : Node
{
   [SerializeField] LockManager lockManager;
    Light2D light;

    private void Start()
    {
        light = GetComponentInChildren<Light2D>();

    }
    public override void Update()
    {
        base.Update();
        if (hit)
        {
            light.color = Color.green;
            lockManager.lockreleaseCount++;
            this.enabled = false;
        }
    }
}

//i like kitty cats
//don't you?