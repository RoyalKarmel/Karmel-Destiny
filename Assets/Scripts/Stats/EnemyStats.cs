using UnityEngine;

[System.Serializable]
public class EnemyStats : CharacterStats
{
    [Header("Enemy Type")]
    public EnemyType enemyType;

    [Header("Damage Popup")]
    public Transform damagePopup;

    [Header("Loot")]
    public EnemyLoot loot;

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

        loot.DropLoot(player, transform.position);
        Destroy(gameObject);
    }
}

public enum EnemyType
{
    Melee,
    Range,
    Necromancer
}
