using UnityEngine;

// [CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    [Header("Item")]
    public new string name = "New Item";
    public Sprite icon = null;
    public bool isSpecialItem = false;
    public ItemType type;

    [Header("Consumable")]
    public int quantity = 1;

    [Header("Product")]
    public Product product;

    public virtual void Use()
    {
        if (type == ItemType.Consumable)
            DecreaseQuantity();

        Debug.Log("Using " + name);
    }

    // Update item quantity
    public void IncreaseQuantity(int value)
    {
        quantity += value;
    }

    public void DecreaseQuantity()
    {
        SoundManager.instance.PlayItemUse();
        quantity--;

        if (quantity <= 0)
            RemoveFromInventory();
    }

    // Remove item
    public void RemoveFromInventory()
    {
        Inventory.instance.Remove(this);
    }
}

// Item types
public enum ItemType
{
    Currency,
    Equipment,
    Consumable
}
