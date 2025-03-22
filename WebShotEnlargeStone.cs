using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WebShotEnlargeStone : FamiliarStone
{
    //All activation is done in Runestone Class, and all other variables are handled through the ChildParentClasses (AbilityStone, FamiliarStone), when making a new script you only need to create a script make it derive from either stone category
    //Include your methods as an Ovverride (Varies per script check FamiliarStone and AbilityStone)
    //Include new commands ovverriden methods, no need to worry about activation as it is handled by the base Runestone Class which all of these scripts derive from.
    Vector3 origScale;


   public override void Start()
    {
        base.Start();
    }
    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        if (abilityTimer >= maxTime - 1)
        {
            //familiarToChange.spell.projectile.gameObject.GetComponent<Transform>().localScale = new Vector3(.1f, .1f, .1F);

        }
        //if (SceneManager.GetActiveScene() != currScene)
        //{
        //    //familiarToChange.spell.projectile.gameObject.GetComponent<Transform>().localScale = new Vector3(.1f, .1f, .1F);

        //}

    }
    public override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);


        maxTime = 8;
        RunestoneActivation(runestone);
    }
    public override GameObject RunestoneActivation(GameObject _runestone)
    {
        base.RunestoneActivation(runestone);
        FamiliarAbilityTimerStart();
        //familiarToChange.spell.projectile.gameObject.GetComponent<Transform>().localScale = new Vector3(0.5f, 0.5f, 0.5f);

        return runestone;
    }
}
