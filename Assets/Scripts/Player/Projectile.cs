using UnityEngine;

public class Projectile : MonoBehaviour
{
    bool isPlayerProjectile = false;
    float projectileRange;
    float damage;
    float criticalChance;

    CharacterStats characterStats;
    PlayerStats player;
    Vector2 startPosition;

    void Start()
    {
        startPosition = transform.position;
        characterStats = GetComponentInParent<CharacterStats>();
        player = PlayerManager.instance.playerStats;

        isPlayerProjectile = transform.parent.CompareTag("Player");
        InitializeStats();
    }

    void Update()
    {
        if (Vector2.Distance(startPosition, transform.position) > projectileRange)
            Destroy(gameObject);
    }

    void InitializeStats()
    {
        if (characterStats != null)
        {
            projectileRange = characterStats.projectileRange;
            damage = characterStats.damage.GetValue();
            criticalChance = characterStats.criticalChance;
        }
    }

    #region Collision
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && isPlayerProjectile)
            HandleCollisionWithEnemy(collision);
        else if (collision.CompareTag("Player") && !isPlayerProjectile)
            HandleCollisionWithPlayer(collision);
    }

    void HandleCollisionWithEnemy(Collider2D collision)
    {
        EnemyStats enemy = collision.GetComponent<EnemyStats>();
        if (enemy != null)
        {
            float randomValue = Random.value;
            bool isCriticalHit = randomValue <= criticalChance;
            enemy.TakeDamage(damage, isCriticalHit);

            // Decrease durability of equipped spell
            if (player.currentEquipment.ContainsKey(EquipmentSlot.Armor))
                player.currentEquipment[EquipmentSlot.Armor].DecreaseDurability();

            Destroy(gameObject);
        }
    }

    void HandleCollisionWithPlayer(Collider2D collision)
    {
        if (player != null)
        {
            float randomValue = Random.value;
            bool isCriticalHit = randomValue <= criticalChance;
            player.TakeDamage(damage, isCriticalHit);

            Destroy(gameObject);
        }
    }
    #endregion
}
