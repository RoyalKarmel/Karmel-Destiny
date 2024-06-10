[System.Serializable]
public class Consumable
{
    public int quantity = 1;
    public ConsumableType consumableType;
    public Item item;

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
            item.RemoveFromInventory();
    }
}

public enum ConsumableType
{
    None,
    HealthPotion,
    Weed
}
