using UnityEngine;

public class ItemPickup : Interactable
{
    public Item item;
    public int quantity = 1;

    public override void Interact()
    {
        base.Interact();

        item.quantity = quantity;
        PickUp();
    }

    void PickUp()
    {
        Debug.Log("Picking up " + item.name);
        bool wasPickedUp = Inventory.instance.Add(item);

        if (wasPickedUp)
            Destroy(gameObject);
    }
}
