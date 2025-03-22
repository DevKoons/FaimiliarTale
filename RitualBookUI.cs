using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static Unity.VisualScripting.Member;
public class RitualBookUI : MonoBehaviour
{

    public bool bookOpen = false;  //is inventory open?
    [SerializeField] PauseManager pauseMan; //for pausing when inv is open
    [SerializeField] CanvasGroup ritualBookInventory;  //the canvas group for the inventory
    RitualBook ritualBook;

  
    [SerializeField]public Image[] spiderRitual;
    [SerializeField] public Image[] catRitual;
    [SerializeField] public Image[] stinkRitual;
    [SerializeField] public Image[] healRitual;


    [SerializeField] public Sprite[] imageLibrary;

    //Ritual sprites Library
    //If we get a piece it unlocks part of that ritual, by imprinting sprite in the library slot
    //to a hidden inventory slot and then setting it active. 

    private void Start()
    {
        ritualBook = FindObjectOfType<RitualBook>();
    }


    void Update()
    {
        if (ritualBook.UnlockCatBool)
        {
            catRitual[0].GetComponent<Image>().sprite = imageLibrary[1];
            catRitual[1].GetComponent<Image>().sprite = imageLibrary[1];
            catRitual[2].GetComponent<Image>().sprite = imageLibrary[1];
        }
   
        if (ritualBook.UnlockHeal1Bool)
        {
            healRitual[0].GetComponent<Image>().sprite = imageLibrary[3];
            healRitual[1].GetComponent<Image>().sprite = imageLibrary[3];
            healRitual[2].GetComponent<Image>().sprite = imageLibrary[3];
            //create an image and parent it to the canvas.then set the images ‘Source Image’ Property to your sprite
        }
        if (ritualBook.UnlockSpiderBool)
        {
            //show the ritual for spider
            spiderRitual[0].GetComponent<Image>().sprite = imageLibrary[0];
            spiderRitual[1].GetComponent<Image>().sprite = imageLibrary[0];
            spiderRitual[2].GetComponent<Image>().sprite = imageLibrary[0];
        }
        if (ritualBook.UnlockStinkBool)
        {
            stinkRitual[0].GetComponent<Image>().sprite = imageLibrary[1];
            stinkRitual[1].GetComponent<Image>().sprite = imageLibrary[2];
            stinkRitual[2].GetComponent<Image>().sprite = imageLibrary[0];
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            InvOpen();
        }

        if (Input.GetKeyUp(KeyCode.F))
        {
            InvClose();
        }
    }

    public void InvOpen()
    {
        ritualBookInventory.alpha = 1f;
        ritualBookInventory.interactable = true;
        bookOpen = true;
    }

    public void InvClose()
    {
        ritualBookInventory.alpha = 0f;
        ritualBookInventory.interactable = false;
        bookOpen = false;
    }
}
