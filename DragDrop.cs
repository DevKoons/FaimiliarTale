using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private Vector3 origPosition; // Store the original position
    private Canvas parentCanvas; // Reference to the canvas
    private RectTransform rectTransform; // Reference to the RectTransform
    public string materialName; // Name of the material associated with this drag item

    [SerializeField] CookingSlot[] slots; // Array of slots to check against

    private void Awake()
    {
        slots = new CookingSlot[3];

        slots[0] = GameObject.Find("CookingSlot").GetComponent<CookingSlot>();
        slots[1] = GameObject.Find("CookingSlot (1)").GetComponent<CookingSlot>();

        slots[2] = GameObject.Find("CookingSlot (2)").GetComponent<CookingSlot>();



        rectTransform = GetComponent<RectTransform>();
        origPosition = rectTransform.anchoredPosition;
        
        // Get the parent canvas (used for proper UI positioning)
        parentCanvas = GetComponentInParent<Canvas>();
        if (parentCanvas == null)
        {
            Debug.LogError("DragDrop requires the object to be inside a Canvas!");
        }
    }
    void Update()
    {
    
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        rectTransform.transform.position = Input.mousePosition;
        Debug.Log($"Pointer down on item: {materialName}");
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.transform.position = Input.mousePosition;
        if (parentCanvas == null) return;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentCanvas.transform as RectTransform,
            eventData.position,
            parentCanvas.worldCamera,
            out Vector2 localPoint
        );

        //rectTransform.anchoredPosition = localPoint;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // Check if the pointer is over any of the slots
        foreach (CookingSlot slot in slots)
        {
            RectTransform slotRect = slot.GetComponent<RectTransform>();
            if (RectTransformUtility.RectangleContainsScreenPoint(slotRect, eventData.position, parentCanvas.worldCamera))
            {
                // If the pointer is over a valid slot, assign the item to the slot
                slot.AssignItem(materialName); // Assign material name (no sprite handling here as requested)

                // Log the action
                Debug.Log($"Item {materialName} dropped on slot {slot.name}.");
                break; // Exit the loop after assigning the item to the first valid slot
            }
        }

        // Return to original position if not over any slot
        rectTransform.anchoredPosition = origPosition;
    }
}
