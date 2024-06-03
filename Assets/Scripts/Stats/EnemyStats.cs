using UnityEngine;

[System.Serializable]
public class EnemyStats : CharacterStats
{
    [Header("Damage Popup")]
    public Transform damagePopup;

    [Header("Experience")]
    public int minExp = 10;
    public int maxExp = 30;

    [Header("Money")]
    public int minMoney = 10;
    public int maxMoney = 50;

    private PlayerStats player;

    void Start()
    {
        player = PlayerManager.instance.playerStats;
    }

    public override void TakeDamage(float damage, bool isCriticalHit = false)
    {
        base.TakeDamage(damage, isCriticalHit);

        DamagePopup.Create(damagePopup.position, damage, isCriticalHit);
    }

    public override void Die()
    {
        base.Die();

        // Get XP
        int expGained = Random.Range(minExp, maxExp + 1);
        player.GainExperience(expGained);

        // Get money
        int moneyGained = Random.Range(minMoney, maxMoney + 1);
        Currency.instance.AddCurrency(moneyGained);

        // Spawn random item on the ground
        float itemChance = Random.Range(0f, 1f);
        if (itemChance <= 0.5f)
            Inventory.instance.SpawnItem(transform.position);

        Destroy(gameObject);
    }
}
