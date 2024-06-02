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

            item = ItemDatabase.instance.GetRandomItem();

            // Random item quantity
            if (item.type == ItemType.Consumable)
                item.quantity = Random.Range(minQuantity, maxQuantity);

            Inventory.instance.Add(item);

            // Play animation
            animator.SetTrigger("Open");
            isOpen = true;
        }
    }
}
