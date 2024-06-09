using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    [Header("Equipment")]
    public EquipmentSlot equipSlot;
    public int healthModifier;
    public int damageModifier;
    public int projectileDamageModifier;
    public int speedModifier;

    public override void Use()
    {
        base.Use();
        EquipmentManager.instance.Equip(this);
        SoundManager.instance.PlayItemEquip();

        RemoveFromInventory();
    }
}

public enum EquipmentSlot
{
    Armor,
    Weapon,
    Spell,
    Amulet,
}
