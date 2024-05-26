using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    [Header("Item")]
    public new string name = "New Item";
    public Sprite icon = null;
    public bool isSpecialItem = false;
    public bool isCurrency = false;
    public bool isEquipment = false;
    public bool isConsumable = false;
    public int quantity = 1;

    [Header("Product")]
    public Product product;

    public virtual void Use()
    {
        // Play sounds
        if (!isEquipment)
            SoundManager.instance.PlayItemUse();
        else
            SoundManager.instance.PlayItemEquip();

        Debug.Log("Using " + name);

        if (isConsumable)
            DecreaseQuantity();
    }

    // Update item quantity
    public void IncreaseQuantity(int value)
    {
        quantity += value;
    }

    public void DecreaseQuantity()
    {
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
