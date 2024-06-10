using UnityEngine;

[System.Serializable]
public class Product
{
    public int price = 50;
    public bool isPurchased { get; private set; }

    // Buy item
    public void Purchase(Item itemToPurchase)
    {
        if (isPurchased)
            return;

        if (Currency.instance.money < price)
        {
            Debug.Log("Not enought money!");
            return;
        }

        if (itemToPurchase is Equipment)
            isPurchased = true;

        Inventory.instance.Add(itemToPurchase);
        Currency.instance.DecreaseCurrency(price);
        SoundManager.instance.PlayItemPurchase();

        Debug.Log("Purchased: " + itemToPurchase.name);
    }
}
