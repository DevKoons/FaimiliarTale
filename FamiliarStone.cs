using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FamiliarStone : Runestone
{   //All activation is done in Runestone Class, and all other variables are handled through the ChildParentClasses (AbilityStone, FamiliarStone), when making a new script you only need to create a script make it derive from either stone category
    //Include your methods as an Ovverride (Varies per script check FamiliarStone and AbilityStone)
    //Include new commands ovverriden methods, no need to worry about activation as it is handled by the base Runestone Class which all of these scripts derive from.
    public Familiars familiarToChange;
    bool familiarAbilityStart;


    public override void Update()
    {
        base.Update();

    }

    public override GameObject RunestoneActivation(GameObject _runestone)
    {

        base.RunestoneActivation(_runestone);

        return _runestone;
        //Familiar web shots enlarge
        // Rapid Fire
        /// Etc....

    }
    public override void OnCollisionEnter2D(Collision2D collision)
    {

        base.OnCollisionEnter2D(collision);
    }
    public void FamiliarAbilityTimerStart()
    {
        if (familiarAbilityStart)
        {
            abilityTimer += Time.deltaTime;
        }

        if (abilityTimer >= maxTime)
        {

            familiarAbilityStart = false;
            abilityTimer = 0;
        }
        if (abilityTimer >= maxTime - 1)

        {
            //playerCurrentStone.familiarToChange.spell.projectile.gameObject.GetComponent<Transform>().localScale = new Vector3(.1f, .1f, .1F);

        }
        //if (SceneManager.GetActiveScene() != currScene)
        //{
        //    //playerCurrentStone.familiarToChange.spell.projectile.gameObject.GetComponent<Transform>().localScale = new Vector3(.1f, .1f, .1F);

        //}
    }
}

