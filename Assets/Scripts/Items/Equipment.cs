using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    [Header("Equipment")]
    public EquipmentSlot equipSlot;
    public int armorModifier;
    public int damageModifier;
    public int speedModifier;

    public override void Use()
    {
        base.Use();
        EquipmentManager.instance.Equip(this);
        RemoveFromInventory();
    }
}

public enum EquipmentSlot
{
    Armor,
    Weapon,
    Companion,
    Spell,
}
