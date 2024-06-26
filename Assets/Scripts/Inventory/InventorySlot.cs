using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public void AddItem(Item newItem)
    {
        GameObject itemButtonObject = Instantiate(GameAssets.instance.itemButtonPrefab, transform);
        ItemButton itemButton = itemButtonObject.GetComponent<ItemButton>();

        itemButton.AddItem(newItem);
    }

    // Dragging item
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            GameObject dropped = eventData.pointerDrag;
            Draggable draggable = dropped.GetComponent<Draggable>();
            draggable.parentAfterDrag = transform;
        }
    }
}
