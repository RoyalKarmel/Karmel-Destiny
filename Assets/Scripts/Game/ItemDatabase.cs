using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    #region Singleton
    public static ItemDatabase instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Item Database found!");
            return;
        }

        instance = this;
    }
    #endregion

    public Item currency;
    public List<Item> items;
    public List<Equipment> weapons;
    public List<Equipment> armor;
    public List<Equipment> spells;
    public List<Equipment> amulets;

    // Random item from chest
    public Item GetRandomItem()
    {
        List<Item> allItems = new List<Item>();

        allItems.AddRange(items);
        allItems.AddRange(weapons);
        allItems.AddRange(armor);
        allItems.AddRange(spells);
        allItems.AddRange(amulets);

        int index = Random.Range(0, allItems.Count);
        return allItems[index];
    }
}
