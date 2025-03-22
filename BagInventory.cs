using System.Collections.Generic;
using UnityEngine;

public class BagInventory : MonoBehaviour
{
    [SerializeField] CanvasGroup bagUI; // Reference to the inventory UI
    [SerializeField] CanvasGroup ingredientsUI;
    [SerializeField] Transform materialListContainer; // Parent object to hold UI elements for materials
    [SerializeField] List<MaterialUIPrefab> materialUIPrefabs; // List of material type to prefab mappings
    [SerializeField] GameObject ingredientsDragable;

    private bool open; // Tracks if the bag is open
    public Dictionary<string, int> materials = new Dictionary<string, int>(); // Stores collected materials and their amounts
    public Dictionary<string, GameObject> materialPrefabLookup = new Dictionary<string, GameObject>(); // Lookup for material type to prefab




    [System.Serializable]
    public class MaterialUIPrefab
    {
        public string materialType; // Type of material (e.g., "Gold", "Iron")
        public GameObject prefab; // Corresponding prefab for the material type
    }

    void Awake()
    {
        // Populate the materialPrefabLookup dictionary
        foreach (var materialUIPrefab in materialUIPrefabs)
        {
            materialPrefabLookup[materialUIPrefab.materialType] = materialUIPrefab.prefab;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !open) // Press E to toggle inventory UI
        {
            OpenBag();
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            CloseBag();
        }
    }

    void OpenBag()
    {
        bagUI.alpha = 1;
        ingredientsUI.alpha = 1;
        open = true;
        UpdateBagUI();

        if(ingredientsDragable != null)
        {
            ingredientsDragable.SetActive(true);

        }
    }

    void CloseBag()
    {
        bagUI.alpha = 0;
        ingredientsUI.alpha = 0;
        open = false;
        ingredientsDragable.SetActive(false);
    }

    public void AddMaterial(string materialName, int amount)
    {
        // Add material to the dictionary or update its amount
        if (materials.ContainsKey(materialName))
        {
            materials[materialName] += amount;
        }
        else
        {
            materials[materialName] = amount;
        }

        if (open)
        {
            UpdateBagUI(); // Refresh UI if bag is open
        }
    }
    public void RemoveMaterial(string materialName, int amount)
    {
        // Add material to the dictionary or update its amount
        if (materials.ContainsKey(materialName))
        {
            materials[materialName] -= amount;
        }
        else
        {
            materials[materialName] = amount;
        }

        if (open)
        {
            UpdateBagUI(); // Refresh UI if bag is open
        }
    }
    void UpdateBagUI()
    {
        // Clear existing UI
        foreach (Transform child in materialListContainer)
        {
            Destroy(child.gameObject);
        }

        // Populate UI with current materials and their amounts
        foreach (KeyValuePair<string, int> material in materials)
        {
            if (materialPrefabLookup.TryGetValue(material.Key, out GameObject prefab))
            {
                GameObject uiElement = Instantiate(prefab, materialListContainer);
                uiElement.GetComponentInChildren<UnityEngine.UI.Text>().text = $"{material.Key}: {material.Value}";
            }
            else
            {
                Debug.LogWarning($"No prefab found for material type: {material.Key}");
            }
        }
    }
}
