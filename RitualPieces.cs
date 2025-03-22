using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RitualPieces : MonoBehaviour
{
    //We have a ritual piece, it will decide
    //the piece will decide to unlock a certain part of a certain ritual. 
    RitualBookUI ritualBookUI;
    [SerializeField] int ritualIndex;

    // Start is called before the first frame update
    void Start()
    {
        ritualBookUI = FindObjectOfType<RitualBookUI>();
    }
    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(ritualIndex);
        if (collision.gameObject.layer == 6 && ritualIndex == 0)
        {
            ritualBookUI.spiderRitual[0].GetComponent<Image>().sprite = ritualBookUI.imageLibrary[0];
            Destroy(this.gameObject);
        }
        if (collision.gameObject.layer == 6 && ritualIndex == 1)
        {
            ritualBookUI.spiderRitual[1].GetComponent<Image>().sprite = ritualBookUI.imageLibrary[0];
            Destroy(this.gameObject);
        }
        if (collision.gameObject.layer == 6 && ritualIndex == 2)
        {
            ritualBookUI.spiderRitual[2].GetComponent<Image>().sprite = ritualBookUI.imageLibrary[0];
            Destroy(this.gameObject);
        }


        if (collision.gameObject.layer == 6 && ritualIndex == 3)
        {
            ritualBookUI.catRitual[0].GetComponent<Image>().sprite = ritualBookUI.imageLibrary[1];
            Destroy(this.gameObject);
        }
        if (collision.gameObject.layer == 6 && ritualIndex == 4)
        {
            ritualBookUI.catRitual[1].GetComponent<Image>().sprite = ritualBookUI.imageLibrary[1];
            Destroy(this.gameObject);
        }
        if (collision.gameObject.layer == 6 && ritualIndex == 5)
        {
            ritualBookUI.catRitual[2].GetComponent<Image>().sprite = ritualBookUI.imageLibrary[1];
            Destroy(this.gameObject);
        }


        if (collision.gameObject.layer == 6 && ritualIndex == 6)
        {
            ritualBookUI.stinkRitual[0].GetComponent<Image>().sprite = ritualBookUI.imageLibrary[1];
            Destroy(this.gameObject);

        }
        if (collision.gameObject.layer == 6 && ritualIndex == 7)
        {
            ritualBookUI.stinkRitual[1].GetComponent<Image>().sprite = ritualBookUI.imageLibrary[2];
                Destroy(this.gameObject);

        }
        if (collision.gameObject.layer == 6 && ritualIndex == 8)
        {
            ritualBookUI.stinkRitual[2].GetComponent<Image>().sprite = ritualBookUI.imageLibrary[0];
            Destroy(this.gameObject);

        }


        if (collision.gameObject.layer == 6 && ritualIndex == 9)
        {
            ritualBookUI.healRitual[0].GetComponent<Image>().sprite = ritualBookUI.imageLibrary[3];
            Destroy(this.gameObject);
        }

        if (collision.gameObject.layer == 6 && ritualIndex == 10)
        {
            ritualBookUI.healRitual[1].GetComponent<Image>().sprite = ritualBookUI.imageLibrary[3];
            Destroy(this.gameObject);

        }
        if (collision.gameObject.layer == 6 && ritualIndex == 11)
        {
            ritualBookUI.healRitual[2].GetComponent<Image>().sprite = ritualBookUI.imageLibrary[3];
            Destroy(this.gameObject);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
    }
}
