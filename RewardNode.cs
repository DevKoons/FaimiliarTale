using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class RewardNode : Node
{
    int timesSpawned;
    [SerializeField] int timesToSpawnMax;
    [SerializeField] Resource resource;
    [SerializeField] GameObject reward;
    [SerializeField] GameObject[] spawnPoint;
    Light2D light;

    [SerializeField]bool isReward;
    // Start is called before the first frame update
    void Start()
    {
        light = GetComponentInChildren<Light2D>();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        base.Update();

        if (collision.gameObject.layer == 8 || collision.gameObject.layer == 6)
        {
            light.color = Color.green; 
            Spawn();
            ColorIndex++;
            
        }
    }

    public virtual void Spawn()
    {
        if(!isReward)
        {
            for (int i = 0; i < spawnPoint.Length; i++)
            {
                if (timesSpawned <= timesToSpawnMax) { Instantiate(resource.ResourceGO, spawnPoint[i].transform); timesSpawned++; }
            }
        }
        if(isReward)
        {
            Instantiate(reward, spawnPoint[0].gameObject.transform.position, Quaternion.identity);
        }
      
      
    }
}
