using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    public Image icon;
    public Button removeButton;
    public TMP_Text quantityText;

    public Item item { get; private set; }

    public void AddItem(Item newItem)
    {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;

        removeButton.interactable = true;

        if (item.type == ItemType.Consumable)
        {
            quantityText.gameObject.SetActive(true);
            quantityText.text = item.consumable.quantity.ToString();
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
            if (item.consumable.quantity <= 0)
                RemoveItem();

            // Remove slot when equipt
            if (item.type == ItemType.Equipment)
                Destroy(gameObject);

            // Update item quantity UI
            if (item != null)
                quantityText.text = item.consumable.quantity.ToString();
        }
    }

    void RemoveItem()
    {
        Inventory.instance.Remove(item);
        Destroy(gameObject);
    }

    // Click to remove item from inventory
    public void OnRemoveButton()
    {
        // TODO: Fix this, for now when remove item by button, picked up items are not in ui
        Instantiate(
            item.prefab,
            PlayerManager.instance.player.transform.position,
            Quaternion.identity,
            Inventory.instance.itemsParent
        );

        RemoveItem();
    }
}
