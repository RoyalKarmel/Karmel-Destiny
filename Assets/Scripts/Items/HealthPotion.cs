using UnityEngine;

[CreateAssetMenu(fileName = "New Health Potion", menuName = "Inventory/HealthPotion")]
public class HealthPotion : Item
{
    [Header("Healt Potion")]
    public int health = 10;

    public override void Use()
    {
        base.Use();

        PlayerManager.instance.playerStats.Heal(health);
    }
}
