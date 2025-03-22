
using Newtonsoft.Json;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UIElements;
using static SeasonManager;

public class RitualBook : MonoBehaviour
{

    [SerializeField] RitualPillar[] pillars;

    [SerializeField] List<GameObject> rewards;
    [SerializeField] List<GameObject> subRewards;
    [SerializeField] GameObject spawnPoint;

    float time;
    bool startTime;


    [SerializeField] Color32[] colorLibrary;
    [SerializeField] bool disableIceFamiliarSpawn;
    [SerializeField] bool disablespider;
    [SerializeField] bool disableCat;

    
    [SerializeField] bool[] subRewardClaimed;

    FamiliarLibrary famLib;

    bool unlockcat = false;
    bool unlockspider = false;
    bool unlockice = false;
    bool unlockstink = false;
    bool unlockheal1 = false;
   [SerializeField] bool crackedSite;
  
    [SerializeField] bool spawnFam;
    [SerializeField] GameObject famToSpawn;

  
    public bool UnlockCatBool
    {
        get { return unlockcat; }
        set { unlockcat = value; }

    }
    public bool UnlockSpiderBool
    {
        get { return unlockspider; }
        set { unlockspider = value; }

    }

    public bool UnlockStinkBool
    {
        get { return unlockstink; }
        set { unlockstink = value; }

    }
    public bool UnlockHeal1Bool
    {
        get { return unlockheal1; }
        set { unlockheal1 = value; }

    }
    
    public Color32[] ColorLibrary
    {
        get { return colorLibrary; }
        set { colorLibrary = value; }
    }
    /// <summary>
    /// Color lIbrary Color Coding
    /// 0: Normal = Yellow 
    /// 1: Ice  = Cool Blue
    /// 2: Fire = Red
    /// 3: Lightning = 
    /// 4: Poison / Toxic = Green
    /// 5: Bleed = Crimson
    /// 6: Cursed = Purple
    /// </summary>
    /// 

    /// <summary>
    /// Damage Types 
    /// 0 = Normal
    /// 1 = Ice
    /// 2 = Fire
    /// 3 = Lightning
    /// 4 = Poison / Toxic
    /// 5 = Bleeding
    /// 6 = Cursed (Unique Type)
    /// 7 = Light (Unique Type)
    /// </summary>

    /*
     * Familiar Type and Index
     *
     Cattrick
     *Bleeding 5

     */


    /*
     * Color coding Color Library Hexadecimals

        0-C6D953
        1-4CE9E5
        2-FF551C
        3-A82FA5
        4-317D08
        5-7B1414
        6-69A5CC
     */


    public RitualPillar[] RitualPillars
    {
        get { return pillars; }
        set { pillars = value; }
    }
    private void Start()
    {
        famLib = FindObjectOfType<FamiliarLibrary>();
        colorLibrary[0] = Color.yellow;
        for (int i = 0; i < pillars.Length; i++)
        {
            pillars[i].GemNode.GetComponent<SpriteRenderer>().color = colorLibrary[0];
        }
    }
    // Update is called once per frame
    void Update()
    {
        //familiarLibrary.familiarsDisplayed.Add(library[i], obj);

        /// Final Concept
        /// If you input the correct colors, look at the reward, if it's a familiar and you have not obtained it, instantiate the familiar
        /// If you have obtained it instantiate a sub reward. We will have to call the players inventory to check this?
        /// If you have claimed that color codes rewards / sub rewards nothing happens. 
        /// All pillars will have the same color codes
        /// We should block off certain color codes until we are ready to introduce them. 
        /// All pillars in this method will have the same subreward color codes as well (Gold,Etc) these should pull from an RNG list which we can use the sub reward library for
        /// 
        if (startTime)
        {
            time += Time.deltaTime;
        }
        if (famLib.library.Count <= 0)
        {
            #region IceFamiliarUnlock
            if (Vector3.Distance(this.gameObject.transform.position, GameObject.FindObjectOfType<PlayerCommand>().gameObject.transform.position) <= 4 && !disableIceFamiliarSpawn)
            {
                startTime = true;
                if (time >= 1)
                {
                    pillars[0].GemNode.GetComponent<SpriteRenderer>().color = Color.cyan;
                }
                if (time >= 2)
                {
                    pillars[1].GemNode.GetComponent<SpriteRenderer>().color = Color.cyan;
                }
                if (time >= 3)
                {
                    pillars[2].GemNode.GetComponent<SpriteRenderer>().color = Color.cyan;



                    time = 0;
                    startTime = false;

                    UnlockIce();
                }

            }

            #endregion
        }

       

        ///Special sites: can only be used for specific actions, such as opening dungeon doors, etc.
        
        ///Intact sites: can be used repeatedly
        if (!crackedSite)
        {
            for (int i = 0; i < famLib.library.Count; i++)
            {
                if (famLib.library[i] == null)
                {
                    return;
                }
                if (famLib.library[i])
                {
                    RewardLibrary();
                }
                if (!famLib.library[i])
                {
                    SubRewardLibrary();
                }
                if (famLib.library[0] != null)
                {
                    SubRewardLibrary();
                }
            }


        }

        ///Cracked sites: can be used once, then can’t be used again until the tile is reset
        if (crackedSite)
        {
            for (int i = 0; i < famLib.library.Count; i++)
            {
                if (famLib.library[i] == null)
                {
                    return;
                }
                if (famLib.library[i])
                {
                    RewardLibrary();
                    Destroy(this);

                    for (int j = 0; j < pillars.Length; j++)
                    {
                        pillars[j].GemNode.GetComponent<SpriteRenderer>().color = Color.black;

                    }
                }
             
            }
        }

       




    }

    void RewardLibrary()
    {
        if(crackedSite && spawnFam)
        {
            for(int i = 0;i < colorLibrary.Length;i++)
            {
                if (pillars[i].GemNode.GetComponentInChildren<SpriteRenderer>().color == colorLibrary[i])
                {
                    Instantiate(famToSpawn,spawnPoint.gameObject.transform.position, Quaternion.identity);
                }
            }
        }

      if(!crackedSite)
        {
            if (pillars[0].GemNode.GetComponentInChildren<SpriteRenderer>().color == colorLibrary[1] && pillars[1].GemNode.GetComponentInChildren<SpriteRenderer>().color == colorLibrary[1] && pillars[2].GemNode.GetComponentInChildren<SpriteRenderer>().color == colorLibrary[1] && !unlockspider)
            {

                UnlockSpider();
                unlockspider = true;
                disablespider = true;
                if (crackedSite)
                    //if(unlockspider)
                    //{
                    //    usingMainRewardLibrary = false;
                    //}
                    for (int i = 0; i < pillars.Length; i++)
                    {
                        pillars[i].GemNode.GetComponent<SpriteRenderer>().color = colorLibrary[0];
                    }

            }
            if (pillars[0].GemNode.GetComponentInChildren<SpriteRenderer>().color == colorLibrary[6] && pillars[1].GemNode.GetComponentInChildren<SpriteRenderer>().color == colorLibrary[6] && pillars[2].GemNode.GetComponentInChildren<SpriteRenderer>().color == colorLibrary[6] && !unlockcat)
            {

                UnlockCat();
                unlockcat = true;
                disableCat = true;
                for (int i = 0; i < pillars.Length; i++)
                {
                    pillars[i].GemNode.GetComponent<SpriteRenderer>().color = colorLibrary[0];
                }


            }

            //Spider,Cat, Ice
            //Unlock Stink

            if (pillars[0].GemNode.GetComponentInChildren<SpriteRenderer>().color == colorLibrary[6] && pillars[1].GemNode.GetComponentInChildren<SpriteRenderer>().color == colorLibrary[5] && pillars[2].GemNode.GetComponentInChildren<SpriteRenderer>().color == colorLibrary[1] && !unlockstink)
            {
                UnlockStink();
                unlockstink = true;
                for (int i = 0; i < pillars.Length; i++)
                {
                    pillars[i].GemNode.GetComponent<SpriteRenderer>().color = colorLibrary[0];
                }
            }

            //Stink, Stink, Stink
            //Heal1 //Small heal
            //* Light 6
            if (pillars[0].GemNode.GetComponentInChildren<SpriteRenderer>().color == colorLibrary[4] && pillars[1].GemNode.GetComponentInChildren<SpriteRenderer>().color == colorLibrary[4] && pillars[2].GemNode.GetComponentInChildren<SpriteRenderer>().color == colorLibrary[4] && !unlockheal1)
            {
                UnlockHeal1();
                unlockheal1 = true;
                for (int i = 0; i < pillars.Length; i++)
                {
                    pillars[i].GemNode.GetComponent<SpriteRenderer>().color = colorLibrary[0];
                }
            }

           

            //    Heal0 Big Heal
            //    * Light 6


            //Stink, Stink, Cat
            //MoveFam
            //* Normal 0

            //Spider,Ice, Cat
            //Sprinting
            //* Normal 0

            //Spider, Spider, Cat
            //Stinkbug(Stinkypete)
            //* Poision / Toxic 4
        }
      
    }

    //Using this later
    void SubRewardLibrary()
    {
        
        

            if (pillars[0].GemNode.GetComponentInChildren<SpriteRenderer>().color == colorLibrary[1] && pillars[1].GemNode.GetComponentInChildren<SpriteRenderer>().color == colorLibrary[1] && pillars[2].GemNode.GetComponentInChildren<SpriteRenderer>().color == colorLibrary[1] /*Check here to see if player has unlocking familiar*/ && !subRewardClaimed[0])
            {
                RNGRewards();
                subRewardClaimed[0] = true;
                ;
            }

            if (pillars[0].GemNode.GetComponentInChildren<SpriteRenderer>().color == colorLibrary[6] && pillars[1].GemNode.GetComponentInChildren<SpriteRenderer>().color == colorLibrary[6] && pillars[2].GemNode.GetComponentInChildren<SpriteRenderer>().color == colorLibrary[6] && !subRewardClaimed[1])
            {
                RNGRewards();
                subRewardClaimed[1] = true;
                for (int i = 0; i < pillars.Length; i++)
                {
                    pillars[i].GemNode.GetComponent<SpriteRenderer>().color = colorLibrary[0];
                }


            }
            if (pillars[0].GemNode.GetComponentInChildren<SpriteRenderer>().color == colorLibrary[6] && pillars[1].GemNode.GetComponentInChildren<SpriteRenderer>().color == colorLibrary[5] && pillars[2].GemNode.GetComponentInChildren<SpriteRenderer>().color == colorLibrary[1] && !subRewardClaimed[2])
            {
                RNGRewards();
                subRewardClaimed[2] = true;
                for (int i = 0; i < pillars.Length; i++)
                {
                    pillars[i].GemNode.GetComponent<SpriteRenderer>().color = colorLibrary[0];
                }

            }

            //Spider,Cat, Ice
            //Unlock Stink

            if (pillars[0].GemNode.GetComponentInChildren<SpriteRenderer>().color == colorLibrary[4] && pillars[1].GemNode.GetComponentInChildren<SpriteRenderer>().color == colorLibrary[4] && pillars[2].GemNode.GetComponentInChildren<SpriteRenderer>().color == colorLibrary[4] && !subRewardClaimed[3])
            {
                RNGRewards();
                subRewardClaimed[3] = true;
                for (int i = 0; i < pillars.Length; i++)
                {
                    pillars[i].GemNode.GetComponent<SpriteRenderer>().color = colorLibrary[0];
                }

            }
        

    }

    void RNGRewards()
    {
        for (int i = 0; i < subRewards.Count; i++)
        {
            int myRewardIndex = Random.Range(0, 5);
            Instantiate(subRewards[myRewardIndex].gameObject, spawnPoint.gameObject.transform.position, Quaternion.identity);

            //Health Potion
            //Coin Pouch Small
            //Coin Pouch Huge
            //Runestone placeholder
            //Blank
            //Health upgrade

        }
    }



    void UnlockIce()
    {
        for (int i = 0; i < pillars.Length; i++)
        {
            pillars[i].GemNode.GetComponentInChildren<SpriteRenderer>().color = Color.green;

        }
        disableIceFamiliarSpawn = true;

        Instantiate(rewards[0], spawnPoint.gameObject.transform.position, Quaternion.identity);

    }
    void UnlockSpider()
    {
        for (int i = 0; i < pillars.Length; i++)
        {
            pillars[i].GemNode.GetComponentInChildren<SpriteRenderer>().color = Color.green;

        }
        //we need to find the selected color from the pillar, if it's the correct color then unlock cat
        Instantiate(rewards[1], spawnPoint.gameObject.transform.position, Quaternion.identity);
    }
    public void UnlockCat()
    {
        for (int i = 0; i < pillars.Length; i++)
        {
            pillars[i].GemNode.GetComponentInChildren<SpriteRenderer>().color = Color.green;
        }
        //we need to find the selected color from the pillar, if it's the correct color then unlock cat
        Instantiate(rewards[2], spawnPoint.gameObject.transform.position, Quaternion.identity);
    }
    public void UnlockStink()
    {
        for (int i = 0; i < pillars.Length; i++)
        {
            pillars[i].GemNode.GetComponentInChildren<SpriteRenderer>().color = Color.green;
        }
        //we need to find the selected color from the pillar, if it's the correct color then unlock cat
        Instantiate(rewards[3], spawnPoint.gameObject.transform.position, Quaternion.identity);
    }
    void UnlockHeal1()
    {
        for (int i = 0; i < pillars.Length; i++)
        {
            pillars[i].GemNode.GetComponentInChildren<SpriteRenderer>().color = Color.green;
        }
        //we need to find the selected color from the pillar, if it's the correct color then unlock cat
        Instantiate(rewards[4], spawnPoint.gameObject.transform.position, Quaternion.identity);
    }
}




