using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runestone : MonoBehaviour
{   //All activation is done in Runestone Class, and all other variables are handled through the ChildParentClasses (AbilityStone, FamiliarStone), when making a new script you only need to create a script make it derive from either stone category
    //Include your methods as an Ovverride (Varies per script check FamiliarStone and AbilityStone)
    //Include new commands ovverriden methods, no need to worry about activation as it is handled by the base Runestone Class which all of these scripts derive from.

    ///Runestone is going to use the new effect system so hold off on its rework.
    /// <summary>
    ///  Rework
    /// </summary>

    [SerializeField] public GameObject runestone;
    [SerializeField] float value;

    [SerializeField] public float maxTime;
    [SerializeField] public bool usingUses;
    [SerializeField] public bool[] uses;
    bool acquired;
    public bool familiarStone;
    public bool abilityStone;
    private bool starTime;
    public float abilityTimer;
    public EffectManager playerEffectManager;
    public PlayerCommand player;
    [SerializeField] EffectSO runestoneEffect;
    public EffectSO RunestoneEffect => runestoneEffect;

    public virtual void Start()
    {
        player = FindObjectOfType<PlayerCommand>();
        if (player != null)
        {
            playerEffectManager = player.GetComponent<EffectManager>();
        }
    }


    public bool StartTime
    {
        get { return starTime; }
        set { starTime = value; }
    }
   
    private void Awake()
    {
    }
    public virtual void Update()
    {
        //conditionals  go here
        //if timed
        //if uses


        

        if (starTime)
        {
            abilityTimer += Time.deltaTime;
            //the player should start calling time on activation. 

            if (abilityTimer >= maxTime) { Destroy(this); }

        }

        //functionality for multi use objects
        if (acquired)
        {

            runestone.GetComponent<SpriteRenderer>().enabled = false;
            runestone.GetComponent<Collider2D>().enabled = false;
            if (!uses[0] && Input.GetKeyDown(KeyCode.E))
            {
                RunestoneActivation(runestone);
                uses[0] = true;

            }
            if (!uses[1] && uses[0] && Input.GetKeyDown(KeyCode.E))
            {
                RunestoneActivation(runestone);
                uses[1] = true;
            }
            if (!uses[2] && uses[1] && Input.GetKeyDown(KeyCode.E))
            {
                RunestoneActivation(runestone);
                uses[2] = true;
            }
            if (uses[2])
            {
                Destroy(this.gameObject, maxTime);
            }
        }
    }
    public virtual GameObject RunestoneActivation(GameObject _runestone)
    {
        starTime = true;

        if (playerEffectManager != null && runestoneEffect != null)
        {
            playerEffectManager.ApplyEffect(runestoneEffect);
            Debug.Log($"Applied {runestoneEffect.EffectName} from runestone to Player's EffectManager.");
        }
        else
        {
            Debug.LogError("Effect is missing or Player's EffectManager not found!");
        }

        runestone = _runestone;
        return _runestone.gameObject;
    }

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6) // Assuming 6 is the player layer
        {
            starTime = true;
            this.gameObject.GetComponent<Collider2D>().enabled = false;
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;

            if (runestoneEffect != null)
            {
                RunestoneActivation(runestone);
            }
            else
            {
                Debug.LogError("Effect is null on Runestone!");
            }
        }

        if (starTime)
        {
            RunestoneActivation(runestone); // Ensures effect applies even if OnCollision is triggered another way
        }

        if (usingUses)
        {
            acquired = true;
        }
    }



    public void SellRunestone()
    {
        //PlayerCoins += Value
    }
}
