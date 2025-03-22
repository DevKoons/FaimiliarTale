using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SeasonManager;

public class RitualPillar : Node
{
    [SerializeField] bool seasonChangeSite;
    SeasonManager seasonManager;

    public bool SeasonsChangeSite
    {
        get { return seasonChangeSite; }
        set { seasonChangeSite = value; }
    }

    void Start()
    {
        seasonManager = FindObjectOfType<SeasonManager>();

        ritualNode = true;
    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (seasonChangeSite)
        {
            //Change this to something like Selected Objects Season
            seasonManager.seasons = SeasonType.Winter;

        }
        if (ritualNode)
        {
            if (collision.gameObject.layer == 8)
            { 

                if(collision.gameObject.GetComponent<ProjectileSimple>() != null)
                {
                    GemNode.GetComponent<SpriteRenderer>().color = collision.gameObject.GetComponent<ProjectileSimple>().SelectedColor;
                }
                if(collision.gameObject.GetComponent<ProjectileComplex>() != null)
                {
                    GemNode.GetComponent<SpriteRenderer>().color = collision.gameObject.GetComponent<ProjectileComplex>().SelectedColor;
                }
            }           
        }
    }
  
    public override void Update()
    {     

    }
}
