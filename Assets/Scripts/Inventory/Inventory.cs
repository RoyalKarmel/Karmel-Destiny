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

    public bool Add(Item newItem)
    {
        if (!newItem.isDefaultItem)
        {
            if (newItem.isConsumable)
            {
                Item existingItem = items.Find(item => item.name == newItem.name);
                if (existingItem != null)
                {
                    int newQuantity = newItem.quantity + existingItem.quantity;
                    existingItem.IncreaseQuantity(newQuantity);

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
        }

        return true;
    }

    public void Remove(Item item)
    {
        items.Remove(item);

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }
}
