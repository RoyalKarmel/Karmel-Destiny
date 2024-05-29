using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
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

        if (item.type == Item.ItemType.Consumable)
        {
            quantityText.gameObject.SetActive(true);
            quantityText.text = item.quantity.ToString();
        }
        else
            quantityText.gameObject.SetActive(false);
    }

    // Click to use item
    public void UseItem()
    {
        if (item != null)
        {
            item.Use();

            // Remove slot when quantity is 0
            if (item.quantity <= 0)
                OnRemoveButton();

            // Remove slot when equipt
            if (item.type == Item.ItemType.Equipment)
                Destroy(gameObject);

            // Update item quantity UI
            if (item != null)
                quantityText.text = item.quantity.ToString();
        }
    }

    // Click to remove item from inventory
    public void OnRemoveButton()
    {
        Inventory.instance.Remove(item);
        Destroy(gameObject);
    }
}
