using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    //Node should be branched out into Node Base and Node children
    //Puzzle Node
    //Lock Node
    //Resource Node
    // Start is called before the first frame update
    [SerializeField] GameObject gemNode;
    [SerializeField] Color[] colorsToSwitch;
    [SerializeField] public Color baseColor;
    [SerializeField] int colorIndex;
    public bool hit;
    [SerializeField] bool multiHit;
    public bool ritualNode;
    //When a node is hit it should change the isometric diamonds color. 

    public GameObject GemNode { get { return gemNode; } set { gemNode = value; } }
    public Color[] ColorsToSwitchTo { get { return colorsToSwitch; } set { colorsToSwitch = value; } }
    public int ColorIndex
    {
        get { return colorIndex; }
        set { colorIndex = value; }
    }
    //Each node will have a list of colors it can be changed to. 
    //When all the colors match to the corresponding puzzle something can happen. Maybe we need a Node Puzzle Manager for this?

    //The can also be used as Lock and Keys

    //Nodes also can be used as resource rewards. 

    //the three types of nodes should be aesthetically different from each other. 

    //We got the foundation setup tonight tomorrow we will create the variations of nodes.
    // Update is called once per frame
    public virtual void Update()
    {
        if (multiHit)
        {
            if (colorIndex == 0)
            {
                gemNode.GetComponent<SpriteRenderer>().color = baseColor;
            }
            if (colorIndex == 1)
            {
                gemNode.GetComponent<SpriteRenderer>().color = colorsToSwitch[0];
            }
            if (colorIndex == 2)
            {
                gemNode.GetComponent<SpriteRenderer>().color = colorsToSwitch[1];
            }
            if (colorIndex == 3)
            {
                gemNode.GetComponent<SpriteRenderer>().color = colorsToSwitch[2];

            }
            if (colorIndex >= 4)
            {
                colorIndex = 0;
            }
        }
        if (!multiHit)
        {
            if (colorIndex >= 0)
            {
                gemNode.GetComponent<SpriteRenderer>().color = baseColor;
            }
            if (colorIndex >= 1)
            {
                gemNode.GetComponent<SpriteRenderer>().color = colorsToSwitch[0];
            }
            if (colorIndex >= 2)
            {
                colorIndex = 1;
            }
        }

    }
    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
       
        if (collision.gameObject.layer == 8)
        {
            colorIndex++;
            hit = true;
        }
    }
}
