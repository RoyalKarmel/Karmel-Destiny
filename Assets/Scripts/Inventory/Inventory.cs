using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }
        instance = this;
    }
    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public int space = 20;
    public List<Item> items = new List<Item>();

    public Transform itemsParent;

    public bool Add(Item newItem)
    {
        // Check if new item is currency
        if (newItem.type == ItemType.Currency)
        {
            Currency.instance.AddCurrency(newItem.consumable.quantity);
            return true;
        }

        // If item is consumable, check if already exist in inventory and then increase quantity
        if (newItem.type == ItemType.Consumable)
        {
            Item existingItem = items.Find(item => item.name == newItem.name);
            if (existingItem != null)
            {
                // Increase existing item's quantity
                int newQuantity = newItem.consumable.quantity + existingItem.consumable.quantity;
                existingItem.consumable.IncreaseQuantity(newQuantity);

                if (onItemChangedCallback != null)
                    onItemChangedCallback.Invoke();
                return true;
            }
        }

        if (items.Count >= space)
        {
            Debug.Log("Not enough room");
            return false;
        }

        items.Add(newItem);

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();

        return true;
    }

    // Remove item
    public void Remove(Item item)
    {
        items.Remove(item);

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

    // Spawn item on the ground
    public void SpawnItem(Vector3 spawnPosition)
    {
        Item item = ItemDatabase.instance.GetRandomItem();

        Instantiate(item.prefab, spawnPosition, Quaternion.identity, itemsParent);
    }
}
