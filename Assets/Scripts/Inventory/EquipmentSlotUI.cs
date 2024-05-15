using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlotUI : MonoBehaviour
{
    public Image icon;

    Equipment equipment;

    public void AddEquipment(Equipment newEquipment)
    {
        equipment = newEquipment;

        icon.sprite = equipment.icon;
        icon.enabled = true;
    }

    public void RemoveEquipment()
    {
        equipment = null;

        icon.sprite = null;
        icon.enabled = false;
    }
}
