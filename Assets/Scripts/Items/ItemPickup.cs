using UnityEngine;

public class ItemPickup : Interactable
{
    public Item item;
    public int quantity = 1;
    public new ParticleSystem particleSystem;

    protected override void Start()
    {
        base.Start();

        Color rarityColor = ItemDatabase.instance.GetColorForRarity(item.rarity);
        var main = particleSystem.main;
        main.startColor = new ParticleSystem.MinMaxGradient(rarityColor);
    }

    public override void Interact()
    {
        base.Interact();

        item.consumable.quantity = quantity;
        PickUp();
    }

    void PickUp()
    {
        Debug.Log("Picking up " + item.name);
        bool wasPickedUp = Inventory.instance.Add(item);

        if (wasPickedUp)
        {
            SoundManager.instance.PlayItemPickUp();
            Destroy(gameObject);
        }
    }
}
