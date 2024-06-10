using System.Collections;
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
                item.consumable.quantity = Random.Range(minQuantity, maxQuantity);

            // Play animation
            animator.SetTrigger("Open");
            isOpen = true;

            SoundManager.instance.PlayOpenChest();

            StartCoroutine(DropItem());
        }
    }

    IEnumerator DropItem()
    {
        yield return new WaitForSeconds(1f); // animation time

        Inventory.instance.Add(item);
        DroppedItemUI.instance.ShowDroppedItem(item);
    }
}
