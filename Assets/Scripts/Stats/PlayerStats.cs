using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    // Start is called before the first frame update
    void Start()
    {
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
    }

    void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if (newItem != null)
        {
            damage.AddModifier(newItem.damageModifier);
            armor.AddModifier(newItem.armorModifier);
        }

        if (oldItem != null)
        {
            damage.RemoveModifier(oldItem.damageModifier);
            armor.RemoveModifier(newItem.armorModifier);
        }
    }
}
