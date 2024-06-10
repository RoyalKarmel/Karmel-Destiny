using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDetailsUI : MonoBehaviour
{
    public Image itemIcon;
    public TMP_Text itemName;
    public TMP_Text itemDetails;

    Item item;
    const string colorCode = "#FF7402";

    // Initial variables
    string itemNameInitialText;
    string itemDetailsInitialText;
    int initialChildCount;

    void Start()
    {
        itemNameInitialText = itemName.text;
        itemDetailsInitialText = itemDetails.text;
        initialChildCount = itemIcon.transform.childCount;
    }

    void Update()
    {
        if (itemIcon.transform.childCount > initialChildCount)
        {
            item = GetComponentInChildren<ItemButton>().item;
            SetItemDetails(item);
        }
        else
            ClearItemDetails();
    }

    void SetItemDetails(Item newItem)
    {
        item = newItem;

        itemName.text = item.name;
        itemDetails.text = GetItemDetails();
    }

    void ClearItemDetails()
    {
        item = null;

        itemName.text = itemNameInitialText;
        itemDetails.text = itemDetailsInitialText;
    }

    string GetItemDetails()
    {
        var details = string.Empty;

        switch (item.type)
        {
            case ItemType.Consumable:
                details = GetConsumableDetails(item);
                break;

            case ItemType.Equipment:
                details = GetEquipmentDetails(item as Equipment);
                break;

            default:
                details += "No additional details available.\n";
                break;
        }

        return details;
    }

    #region Utils
    string GetConsumableDetails(Item consumableItem)
    {
        var details = string.Empty;
        var consumable = consumableItem.consumable;

        switch (consumable.consumableType)
        {
            case ConsumableType.HealthPotion:
                HealthPotion healthPotion = item as HealthPotion;
                details += $"Heals <color={colorCode}>{healthPotion.health}</color> HP\n";
                break;
            case ConsumableType.Weed:
                Weed weed = item as Weed;
                details += $"Speed Bonus: <color={colorCode}>{weed.bonusSpeed}</color>\n";
                details += $"Duration: <color={colorCode}>{weed.duration}</color>\n";
                break;
        }

        return details;
    }

    string GetEquipmentDetails(Equipment equipment)
    {
        if (equipment == null)
            return "Invalid equipment item.";

        var details = string.Empty;

        if (equipment.healthModifier > 0)
            details += $"Health Modifier: <color={colorCode}>{equipment.healthModifier}</color>\n";
        if (equipment.damageModifier > 0)
            details += $"Damage Modifier: <color={colorCode}>{equipment.damageModifier}</color>\n";
        if (equipment.projectileDamageModifier > 0)
            details +=
                $"Projectile Damage Modifier: <color={colorCode}>{equipment.projectileDamageModifier}</color>\n";
        if (equipment.speedModifier > 0)
            details += $"Speed Modifier: <color={colorCode}>{equipment.speedModifier}</color>\n";

        details += $"Durability: <color={colorCode}>{equipment.durability}</color>\n";

        return details;
    }
    #endregion
}
