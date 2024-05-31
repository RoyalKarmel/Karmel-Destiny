using System.Collections.Generic;
using UnityEngine;

public class Chest : Interactable
{
    public Animator animator;
    bool isOpen = false;

    [Header("Item")]
    public int minQuantity = 3;
    public int maxQuantity = 5;

    Item item;

    public override void Interact()
    {
        if (!isOpen)
        {
            base.Interact();

            item = GetRandomItem();

            // Random item quantity
            if (item.type == Item.ItemType.Consumable)
                item.quantity = Random.Range(minQuantity, maxQuantity);

            Inventory.instance.Add(item);

            // Play animation
            animator.SetTrigger("Open");
            isOpen = true;
        }
    }

    // Random item from chest
    Item GetRandomItem()
    {
        List<Item> allItems = new List<Item>();

        allItems.AddRange(ItemDatabase.instance.items);
        allItems.AddRange(ItemDatabase.instance.weapons);
        allItems.AddRange(ItemDatabase.instance.armor);
        allItems.AddRange(ItemDatabase.instance.shields);
        allItems.AddRange(ItemDatabase.instance.amulets);

        int index = Random.Range(0, allItems.Count);
        return allItems[index];
    }
}
