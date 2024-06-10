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

    [Header("Items")]
    public List<Item> items;
    public List<Equipment> weapons;
    public List<Equipment> armor;
    public List<Equipment> spells;
    public List<Equipment> amulets;

    [Header("Rarity")]
    public List<Rarity> itemsRarity;

    // Random item from all items
    public Item GetRandomItem()
    {
        List<Item> allItems = new List<Item>();

        allItems.AddRange(items);
        allItems.AddRange(weapons);
        allItems.AddRange(armor);
        allItems.AddRange(spells);
        allItems.AddRange(amulets);

        return GetRandomItemFromList(allItems);
    }

    // Random consumable
    public Item GetRandomConsumable()
    {
        return GetRandomItemFromList(items);
    }

    #region Random Equipment

    // Random equipment item (weapons, armor, spells, amulets)
    public Equipment GetRandomEquipment()
    {
        List<Equipment> equipmentList = new List<Equipment>();

        equipmentList.AddRange(weapons);
        equipmentList.AddRange(armor);
        equipmentList.AddRange(spells);
        equipmentList.AddRange(amulets);

        return GetRandomItemFromList(equipmentList);
    }

    // Random weapon
    public Equipment GetRandomWeapon()
    {
        return GetRandomItemFromList(weapons);
    }

    // Random armor
    public Equipment GetRandomArmor()
    {
        return GetRandomItemFromList(armor);
    }

    // Random spell
    public Equipment GetRandomSpell()
    {
        return GetRandomItemFromList(spells);
    }

    // Random amulet
    public Equipment GetRandomAmulet()
    {
        return GetRandomItemFromList(amulets);
    }

    #endregion

    #region Utils

    // Get random item from a list of items considering rarity chances
    private T GetRandomItemFromList<T>(List<T> itemList)
        where T : Item
    {
        if (itemList == null || itemList.Count == 0)
            return null;

        T item = null;

        while (item == null)
        {
            int index = Random.Range(0, itemList.Count);
            var tempItem = itemList[index];

            float randomValue = Random.value;

            if (GetChanceForRarity(tempItem.rarity) <= randomValue)
                item = tempItem;
        }

        return item;
    }

    // Get Color for item rarity
    public Color GetColorForRarity(ItemRarity rarity)
    {
        foreach (var rarityItem in itemsRarity)
        {
            if (rarityItem.rarity == rarity)
            {
                return rarityItem.particleColor;
            }
        }
        return Color.white;
    }

    // Get chance for item rarity
    float GetChanceForRarity(ItemRarity rarity)
    {
        foreach (var rarityItem in itemsRarity)
        {
            if (rarityItem.rarity == rarity)
                return rarityItem.chance;
        }
        return 0f;
    }

    #endregion
}

[System.Serializable]
public class Rarity
{
    public ItemRarity rarity;
    public float chance;
    public Color particleColor;
}
