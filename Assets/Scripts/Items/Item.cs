using UnityEngine;

public class Item : ScriptableObject
{
    [Header("Item prefab")]
    public GameObject prefab;

    [Header("Item")]
    public new string name = "New Item";
    public Sprite icon = null;
    public ItemRarity rarity;
    public ItemType type;

    [Header("Consumable")]
    public Consumable consumable;

    [Header("Product")]
    public Product product;

    public virtual void Use()
    {
        if (type == ItemType.Consumable)
            consumable.DecreaseQuantity();

        Debug.Log("Using " + name);
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

// Item rarity
public enum ItemRarity
{
    Common,
    Rare,
    Epic,
    Legendary
}
