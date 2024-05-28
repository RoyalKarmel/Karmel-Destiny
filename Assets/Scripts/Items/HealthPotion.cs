using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/HealthPotion")]
public class HealthPotion : Item
{
    [Header("Healt Potion")]
    public int health = 10;
    // PlayerStats player = PlayerManager.instance.playerStats;

    public override void Use()
    {
        base.Use();

        PlayerManager.instance.playerStats.Heal(health);
    }
}
