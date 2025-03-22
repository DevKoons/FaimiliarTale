using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityStone : Runestone
{   //All activation is done in Runestone Class, and all other variables are handled through the ChildParentClasses (AbilityStone, FamiliarStone), when making a new script you only need to create a script make it derive from either stone category
    //Include your methods as an Ovverride (Varies per script check FamiliarStone and AbilityStone)
    //Include new commands ovverriden methods, no need to worry about activation as it is handled by the base Runestone Class which all of these scripts derive from.

    bool abilityTimeStart;
    public override void Start()
    {
        base.Start();
    }
    public override void Update()
    {
        base.Update();
        //we can move this activation up to the update function and call base update across all scripts
        //check if timedstone if so
        //time update
        //check if time is less than max time
        

        //if not timed stone, count uses come up with a use key here
        //bool array uses, when we hit the key we add a use and use the effect (activate the stone)
    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        //we want to pick it up and add it to our HUD if slots are available;
    }

    public override GameObject RunestoneActivation(GameObject _runestone)
    {
     

        base.RunestoneActivation(_runestone);
        return _runestone;

        //player.getcomp
        //instantiate from player pos for X
        // Create shield around player for X

    }
    public void AbilityTimerStart()
    {
        if (abilityTimeStart)
        {
            abilityTimer += Time.deltaTime;
            if (abilityTimer >= maxTime)
            {
                ResetHealth();
                abilityTimeStart = false;
                abilityTimer = 0;
            }
        }
    }
    public void ResetHealth()
    {
        GetComponent<StatsManager>().MyResources.CurrentHealth = 10;
        GetComponent<StatsManager>().MyResources.MaxHealth = 10;
    }

}
