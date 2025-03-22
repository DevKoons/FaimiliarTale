using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IncreasedHealthStone : AbilityStone
{
    //All activation is done in Runestone Class, and all other variables are handled through the ChildParentClasses (AbilityStone, FamiliarStone), when making a new script you only need to create a script make it derive from either stone category
    //Include your methods as an Ovverride (Varies per script check FamiliarStone and AbilityStone)
    //Include new commands ovverriden methods, no need to worry about activation as it is handled by the base Runestone Class which all of these scripts derive from.

    float orighealth = 10;
    float rateOfIncrease = 2;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
       

    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        if (abilityTimer >= maxTime)
        {
            //call method from player with these commands;
            player.GetComponent<StatsManager>().MyResources.MaxHealth = orighealth;
            player.GetComponent<StatsManager>().MyResources.CurrentHealth = orighealth;
        }
    }
    public override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);

    }
    public override GameObject RunestoneActivation(GameObject _runestone)
    {
       
        player.Resources.MaxHealth = 100;
        player.Resources.CurrentHealth += 100;
        base.RunestoneActivation(runestone);

        return runestone;
    }

    //private void OnLevelWasLoaded(int level)
    //{
    //    if(SceneManager.GetSceneByBuildIndex(level) != SceneManager.GetActiveScene())
    //    {
    //        player.ResetHealth();
    //    }
    //}
}

