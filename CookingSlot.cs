using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class CookingSlot : MonoBehaviour
{
    [SerializeField] private string materialName;
    [SerializeField] private Image[] imageToDisplay;
    private BagInventory inventory;

    // Delegate alternative using Action
    private Dictionary<string, Action> assignActions;

    public string MaterialName
    {
        get { return materialName; }
        set { materialName = value; }
    }

    public Image[] ImageToDisplay
    {
        get { return imageToDisplay; }
        set { imageToDisplay = value; }
    }

    private void Start()
    {
        inventory = GameObject.FindObjectOfType<BagInventory>();

        // Initialize the dictionary of actions
        assignActions = new Dictionary<string, Action>
        {
            { "PhoenixFeather", () =>
                {
                    imageToDisplay[0].gameObject.SetActive(true);
                    inventory.RemoveMaterial("PhoenixFeather", 1);
                    imageToDisplay[1].gameObject.SetActive(false);
                    imageToDisplay[2].gameObject.SetActive(false);
                }
            },
            { "GleamingRemnants", () =>
                {
                    imageToDisplay[1].gameObject.SetActive(true);
                    inventory.RemoveMaterial("GleamingRemnants", 1);
                    imageToDisplay[0].gameObject.SetActive(false);
                    imageToDisplay[2].gameObject.SetActive(false);
                }
            },
            { "BloodRose", () =>
                {
                    imageToDisplay[2].gameObject.SetActive(true);
                    inventory.RemoveMaterial("BloodRose", 1);
                    imageToDisplay[0].gameObject.SetActive(false);
                    imageToDisplay[1].gameObject.SetActive(false);
                }
            }
        };
    }

    public void AssignItem(string _materialName)
    {
        materialName = _materialName;

        if (assignActions.TryGetValue(materialName, out Action action))
        {
            // Execute the specific assignment logic
            action.Invoke();
        }
        else
        {
            Debug.LogWarning("No assignment action defined for material: " + materialName);
        }
    }

    public void ClearSlot()
    {
        // Reset material name to an empty string.
        materialName = "";
        // Disable all images in this slot.
        foreach (Image img in imageToDisplay)
        {
            img.gameObject.SetActive(false);
        }
    }
}
