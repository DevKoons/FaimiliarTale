using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public delegate void PotionRecipes();
public class CookingPot : MonoBehaviour
{
    [SerializeField] CookingSlot[] slots;
    [SerializeField] GameObject cookingPotUI;

    [SerializeField] GameObject[] rewards;
    [SerializeField] Transform spawnPoint;
    // Assuming MaterialName is a string; if it's another type, adjust accordingly.
    public string MaterialName;
    // Assuming ImageToDisplay is a Sprite; adjust if it’s a different type.
    public Sprite ImageToDisplay;

    public enum PotionRecipeTypes
    {
        health,
        shield,

    }

    [SerializeField] private PotionRecipeTypes potionRecipeTypes;
    private Dictionary<PotionRecipeTypes, PotionRecipes> potionRecipes;
    
    public CookingSlot[] Slots
    {
        get { return slots; }
        set { slots = value; }
    }

    private void Start()
    {
        potionRecipes = new Dictionary<PotionRecipeTypes, PotionRecipes>
        {
            {PotionRecipeTypes.health, HealthRecipe }
        };

        cookingPotUI = GameObject.Find("CookingPotUI");
        //This works we can update it though to have CookingSlot directly connect and talk.
        slots[0] = GameObject.Find("CookingSlot").GetComponent<CookingSlot>();
        slots[1] = GameObject.Find("CookingSlot (1)").GetComponent<CookingSlot>();
        slots[2] = GameObject.Find("CookingSlot (2)").GetComponent<CookingSlot>();
        cookingPotUI.gameObject.SetActive(false);

        
    }
    private void Update()
    {
        if(potionRecipes.TryGetValue(potionRecipeTypes, out PotionRecipes recipe))
        {
            recipe();
        } 
        else
        {
            Debug.LogWarning("Unhandled move type: " + potionRecipeTypes);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            Debug.Log("Player Hit Pot");
        }
        if (collision.gameObject.layer == 6 && Input.GetKeyDown(KeyCode.F))
        {
            cookingPotUI.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        cookingPotUI.gameObject.SetActive(false);

    }
    void HealthRecipe()
    {
        if (slots[0].MaterialName.ToString() == "PhoenixFeather" && slots[1].MaterialName.ToString() == "PhoenixFeather" && slots[2].MaterialName.ToString() == "PhoenixFeather")
        {
            Debug.Log(slots[0].MaterialName);
            Debug.Log(slots[1].MaterialName);
            Debug.Log(slots[2].MaterialName);

            Instantiate(rewards[0], spawnPoint.gameObject.transform.position, Quaternion.identity);

            


            slots[0].ClearSlot();
            slots[1].ClearSlot();
            slots[2].ClearSlot();


            Debug.Log("Reward Spawned" + rewards[0].name);
        }
       
    }
 
}
