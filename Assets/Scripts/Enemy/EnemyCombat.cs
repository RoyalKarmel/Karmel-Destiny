using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public float attackCooldown = 1f;
    float timeSinceLastAttack = 0;
    bool isAttacking = false;

    Transform player;
    PlayerStats playerStats;

    [SerializeField]
    EnemyStats enemyStats;

    void Start()
    {
        player = PlayerManager.instance.player.transform;
        playerStats = PlayerManager.instance.playerStats;
    }

    void Update()
    {
        if (isAttacking)
        {
            timeSinceLastAttack += Time.deltaTime;
            if (timeSinceLastAttack >= attackCooldown)
            {
                isAttacking = false;
                timeSinceLastAttack = 0;
            }
        }
        else
        {
            if (enemyStats.combatType == CombatType.Melee)
            {
                if (Vector3.Distance(transform.position, player.position) <= enemyStats.meleeRange)
                    MeleeAttack();
            }
            else
            {
                if (
                    Vector3.Distance(transform.position, player.position)
                    <= enemyStats.projectileRange
                )
                    RangeAttack();
            }
        }
    }

    #region Combat
    void MeleeAttack()
    {
        if (!isAttacking)
        {
            float randomValue = Random.value;
            if (randomValue <= enemyStats.criticalChance)
                playerStats.TakeDamage(enemyStats.damage.GetValue(), true);
            else
                playerStats.TakeDamage(enemyStats.damage.GetValue());

            isAttacking = true;
        }
    }

    void RangeAttack()
    {
        Vector3 direction = (player.position - transform.position).normalized;

        GameObject projectile = Instantiate(
            GameAssets.instance.enemyProjectile,
            transform.position,
            Quaternion.identity
        );
        projectile.transform.SetParent(transform);

        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = direction * enemyStats.projectileSpeed;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        projectile.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        isAttacking = true;
    }

    #endregion

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        // Gizmos on melee
        if (enemyStats.meleeRange > 0)
            Gizmos.DrawWireSphere(transform.position, enemyStats.meleeRange);
        // Gizmos on range attack
        else if (enemyStats.projectileRange > 0)
            Gizmos.DrawWireSphere(transform.position, enemyStats.meleeRange);
    }
}
