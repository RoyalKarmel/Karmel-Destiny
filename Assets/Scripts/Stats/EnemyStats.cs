using UnityEngine;

[System.Serializable]
public class EnemyStats : CharacterStats
{
    [Header("Enemy Type")]
    public EnemyType enemyType;
    public bool canClone = false;

    [Header("Damage Popup")]
    public Transform damagePopup;

    [Header("Clone Settings")]
    public float cloneInterval = 5f;
    public float cloneLifetime = 15f;

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
