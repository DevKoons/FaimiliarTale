using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static Unity.VisualScripting.Member;
public class RecipeBookUI : MonoBehaviour
{

    public bool bookOpen = false;  //is inventory open?
    [SerializeField] PauseManager pauseMan; //for pausing when inv is open
    [SerializeField] CanvasGroup recipeBookInventory;  //the canvas group for the inventory

    [SerializeField] public Image[] healthPotion;


    [SerializeField] public Sprite[] imageLibrary;

    [SerializeField] CookingPot cookinPot;

    [SerializeField] public Image[] healthPotionRecipe;



    //Ritual sprites Library
    //If we get a piece it unlocks part of that ritual, by imprinting sprite in the library slot
    //to a hidden inventory slot and then setting it active. 

    private void Start()
    {
        
        
    
    }


    void Update()
    {
        cookinPot = FindObjectOfType<CookingPot>();
       //if(cookinPot.Potion)

        if (Input.GetKeyDown(KeyCode.E))
        {
            InvOpen();
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            InvClose();
        }

        for (int i = 0; i < cookinPot.Slots.Length; i++)
        {
            if (cookinPot.Slots[i] != null)
            {
                if (cookinPot.Slots[0].MaterialName.ToString() == "PhoenixFeather" && cookinPot.Slots[1].MaterialName.ToString() == "PhoenixFeather" && cookinPot.Slots[2].MaterialName.ToString() == "PhoenixFeather")
                {
                    healthPotionRecipe[0].GetComponent<Image>().sprite = imageLibrary[0];
                    healthPotionRecipe[1].GetComponent<Image>().sprite = imageLibrary[0];
                    healthPotionRecipe[2].GetComponent<Image>().sprite = imageLibrary[0];
                }
                else
                {
                    return;
                }
            }
        }
        //Null reference
        
    }

    public void InvOpen()
    {
        recipeBookInventory.alpha = 1f;
        recipeBookInventory.interactable = true;
        bookOpen = true;
    }

    public void InvClose()
    {
        recipeBookInventory.alpha = 0f;
        recipeBookInventory.interactable = false;
        bookOpen = false;
    }
}
