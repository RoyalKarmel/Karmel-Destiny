using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public new string name = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;
    public bool isConsumable = false;
    public int quantity = 1;

    public virtual void Use()
    {
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
