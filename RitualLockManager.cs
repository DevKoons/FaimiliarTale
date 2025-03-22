using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RitualLockManager : MonoBehaviour
{

    /// <summary>
    /// Color lIbrary Color Coding
    /// 0: Normal = Yellow 
    /// 1: Ice  = Cool Blue
    /// 2: Fire = Red
    /// 3: Lightning = Purple
    /// 4: Poison / Toxic = Green
    /// 5: Bleed = Crimson
    /// 6: Cursed = Stone Gray
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


    [SerializeField] RitualPillar[] pillars;

   
   
    [SerializeField] Color32[] colorLibrary;

    [SerializeField] GameObject doorToUnlock;
  


    FamiliarLibrary famLib;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Initial unlock door for this scene, we will have to choose the color and type in the proper hexa
        if (pillars[0].GemNode.GetComponentInChildren<SpriteRenderer>().color == colorLibrary[4] && pillars[1].GemNode.GetComponentInChildren<SpriteRenderer>().color == colorLibrary[6] && pillars[2].GemNode.GetComponentInChildren<SpriteRenderer>().color == colorLibrary[4])
        {
            Destroy(doorToUnlock.gameObject);
        }

    }
}
