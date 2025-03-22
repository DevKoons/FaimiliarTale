using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MaterialGO : MonoBehaviour
{
    [SerializeField] string materialName; // Name of the material to add to the bag
    [SerializeField] int materialAmount = 1; // Amount of material to collect

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if player is colliding and presses E
        if (collision.gameObject.layer == 6)
        {
            // Find the BagInventory script on the player
            BagInventory bag = collision.gameObject.GetComponent<BagInventory>();
            if (bag != null)
            {
                bag.AddMaterial(materialName, materialAmount); // Add material to the bag
                Destroy(gameObject); // Destroy the material game object
            }
        }
    }   
}
