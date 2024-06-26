using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton
    public static EquipmentManager instance;

    void Awake()
    {
        instance = this;
    }
    #endregion

    Equipment[] currentEquipment;

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;

    [Header("Equipment UI")]
    public Transform equipmentParent;
    EquipmentSlotUI[] equipmentSlots;

    Inventory inventory;
    InventoryUI inventoryUI;

    void Start()
    {
        inventory = Inventory.instance;
        inventoryUI = InventoryUI.instance;

        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];

        equipmentSlots = equipmentParent.GetComponentsInChildren<EquipmentSlotUI>();
    }

    // Equip Item
    public void Equip(Equipment newItem)
    {
        int slotIndex = (int)newItem.equipSlot;

        Equipment oldItem = null;

        if (currentEquipment[slotIndex] != null)
        {
            oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);
        }

        if (onEquipmentChanged != null)
            onEquipmentChanged.Invoke(newItem, oldItem);

        // Equip item and remove from Inventory UI
        currentEquipment[slotIndex] = newItem;
        equipmentSlots[slotIndex].AddEquipment(newItem);

        // Equip in player model
        if (
            PlayerManager.instance.equipmentSlotToRenderer.TryGetValue(
                newItem.equipSlot,
                out var renderer
            )
        )
        {
            renderer.sprite = newItem.icon;
        }

        inventoryUI.itemsInUI.Remove(newItem);
    }

    // Unequip item
    public void Unequip(int slotIndex)
    {
        if (currentEquipment[slotIndex] != null)
        {
            Equipment oldItem = currentEquipment[slotIndex];

            // Check if item is destroyed
            if (oldItem.durability <= 0)
                inventory.Remove(oldItem);
            else
                inventory.Add(oldItem);

            currentEquipment[slotIndex] = null;
            equipmentSlots[slotIndex].RemoveEquipment();

            if (onEquipmentChanged != null)
                onEquipmentChanged.Invoke(null, oldItem);

            // Unequip in player model
            if (
                PlayerManager.instance.equipmentSlotToRenderer.TryGetValue(
                    oldItem.equipSlot,
                    out var renderer
                )
            )
            {
                renderer.sprite = null;
            }
        }
    }

    // Unequip all items
    public void UnequipAll()
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            Unequip(i);
        }
    }

    void Update()
    {
        // Unequip all items
        if (Input.GetKeyDown(KeyCode.U))
            UnequipAll();
    }
}
