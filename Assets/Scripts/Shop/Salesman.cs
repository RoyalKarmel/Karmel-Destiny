using System.Collections.Generic;
using UnityEngine;

public class Salesman : Interactable
{
    [Header("Products")]
    public List<Item> products = new List<Item>();

    [Header("Special Products")]
    public List<Item> specialProducts = new List<Item>();

    public override void Interact()
    {
        base.Interact();

        if (!Shop.instance.shopUI.activeSelf)
        {
            GenerateUI(Shop.instance.productsSlots, products);
            GenerateUI(Shop.instance.specialProductsSlots, specialProducts);
        }

        Shop.instance.ToggleShopUI();
    }

    void GenerateUI(ProductSlot[] slots, List<Item> items)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].GenerateProductSlots(items[i]);
        }
    }
}
