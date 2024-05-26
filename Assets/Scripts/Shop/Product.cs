using UnityEngine;

[System.Serializable]
public class Product
{
    public int price = 50;

    // Buy item
    public void Purchase(Item itemToPurchase)
    {
        if (Currency.instance.money < price)
        {
            Debug.Log("Not enought money!");
            return;
        }

        Inventory.instance.Add(itemToPurchase);
        Currency.instance.DecreaseCurrency(price);
        SoundManager.instance.PlayItemPurchase();

        Debug.Log("Purchased: " + itemToPurchase.name);
    }
}
