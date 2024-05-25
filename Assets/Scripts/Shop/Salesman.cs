using System.Collections.Generic;
using UnityEngine;

public class Salesman : Interactable
{
    [Header("Products")]
    public List<Item> products = new List<Item>();

    [Header("Special Products")]
    public List<Item> specialProducts = new List<Item>();

    bool productsInitialized = false;

    public override void Interact()
    {
        base.Interact();

        if (!productsInitialized)
            InitializeProducts();

        if (!Shop.instance.shopUI.activeSelf)
            DisplayUI();

        Shop.instance.ToggleShopUI();
    }

    void InitializeProducts()
    {
        InitializeRandomProducts(products, Shop.instance.productsCount, GetRandomItem);
        InitializeRandomProducts(
            specialProducts,
            Shop.instance.specialProductsCount,
            GetRandomEquipment
        );

        productsInitialized = true;
    }

    // Initialize random products
    void InitializeRandomProducts<T>(List<T> items, int slotCount, System.Func<T> getRandomItem)
    {
        for (int i = 0; i < slotCount; i++)
        {
            items.Add(getRandomItem());
        }
    }

    #region Random products
    // Get random item from item list
    Item GetRandomItem()
    {
        int index = Random.Range(0, ItemDatabase.instance.items.Count);
        return ItemDatabase.instance.items[index];
    }

    // Get random equipment from both weapons and armor lists
    Equipment GetRandomEquipment()
    {
        List<Equipment> combinedList = new List<Equipment>();

        combinedList.AddRange(ItemDatabase.instance.weapons);
        combinedList.AddRange(ItemDatabase.instance.armor);

        int index = Random.Range(0, combinedList.Count);
        return combinedList[index];
    }
    #endregion

    #region Products UI

    void DisplayUI()
    {
        GenerateUI(Shop.instance.productsSlots, products);
        GenerateUI(Shop.instance.specialProductsSlots, specialProducts);
    }

    // Generate product UI
    void GenerateUI(ProductSlot[] slots, List<Item> items)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].GenerateProductSlots(items[i]);
        }
    }

    #endregion
}
