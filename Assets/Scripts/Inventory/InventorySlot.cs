using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Button removeButton;
    public TMP_Text quantityText;

    Item item;

    public void AddItem(Item newItem)
    {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;

        removeButton.interactable = true;

        if (item.isConsumable)
        {
            quantityText.gameObject.SetActive(true);
            quantityText.text = item.quantity.ToString();
        }
        else
            quantityText.gameObject.SetActive(false);
    }

    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;

        removeButton.interactable = false;

        quantityText.gameObject.SetActive(false);
    }

    // Click to use item
    public void UseItem()
    {
        if (item != null)
        {
            item.Use();

            // Update item quantity UI
            if (item != null)
                quantityText.text = item.quantity.ToString();
        }
    }

    // Click to remove item from inventory
    public void OnRemoveButton()
    {
        Inventory.instance.Remove(item);
    }
}
