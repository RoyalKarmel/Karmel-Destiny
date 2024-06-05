using UnityEngine;

public class Projectile : MonoBehaviour
{
    public bool isPlayerProjectile = false;

    PlayerStats playerStats;
    EnemyStats enemyStats;

    Vector2 startPosition;

    void Start()
    {
        playerStats = PlayerManager.instance.playerStats;
        startPosition = transform.position;

        if (!isPlayerProjectile)
            enemyStats = GetComponentInParent<EnemyStats>();
    }

    void Update()
    {
        if (isPlayerProjectile)
        {
            if (
                Vector2.Distance(startPosition, transform.position)
                > playerStats.projectileRange.GetValue()
            )
                Destroy(gameObject);
        }
        else
        {
            if (
                Vector2.Distance(startPosition, transform.position)
                > enemyStats.projectileRange.GetValue()
            )
                Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // If player procetile
        if (isPlayerProjectile && collision.CompareTag("Enemy"))
        {
            enemyStats = collision.GetComponent<EnemyStats>();

            float randomValue = Random.value;
            if (randomValue <= playerStats.criticalChance)
                enemyStats.TakeDamage(playerStats.projectileDamage.GetValue(), true);
            else
                enemyStats.TakeDamage(playerStats.projectileDamage.GetValue());

            Destroy(gameObject);
        }

        // If enemy procetile
        if (!isPlayerProjectile && collision.CompareTag("Player"))
        {
            float randomValue = Random.value;
            if (randomValue <= enemyStats.criticalChance)
                playerStats.TakeDamage(enemyStats.damage.GetValue(), true);
            else
                playerStats.TakeDamage(enemyStats.damage.GetValue());

            Destroy(gameObject);
        }
    }
}
