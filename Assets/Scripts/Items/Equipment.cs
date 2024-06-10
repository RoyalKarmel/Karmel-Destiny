using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    [Header("Equipment")]
    public EquipmentSlot equipSlot;
    public int durability = 25;

    [Header("Modifiers")]
    public float healthModifier;
    public float damageModifier;
    public float projectileDamageModifier;
    public float speedModifier;

    public override void Use()
    {
        base.Use();
        EquipmentManager.instance.Equip(this);
        SoundManager.instance.PlayItemEquip();

        RemoveFromInventory();
    }

    public void DecreaseDurability()
    {
        durability--;

        if (durability <= 0)
        {
            EquipmentManager.instance.Unequip((int)equipSlot);
            // RemoveFromInventory();
        }

        Debug.Log("Decreased durability of " + name);
    }
}

public enum EquipmentSlot
{
    Armor,
    Weapon,
    Spell,
    Amulet,
}
