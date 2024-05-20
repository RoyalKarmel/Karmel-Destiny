using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public float attackRange = 1.5f;
    public float criticalChance = 0.1f;
    public float attackCooldown = 1f;
    float timeSinceLastAttack = 0;
    bool isAttacking = false;

    Transform player;
    PlayerStats playerStats;
    EnemyStats enemyStats;

    void Start()
    {
        enemyStats = GetComponent<EnemyStats>();

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
            if (Vector3.Distance(transform.position, player.position) <= attackRange)
                Attack();
        }
    }

    public void Attack()
    {
        if (!isAttacking)
        {
            float randomValue = Random.value;
            if (randomValue < criticalChance)
                playerStats.TakeDamage(enemyStats.criticalDamage);
            else
                playerStats.TakeDamage(enemyStats.damage.GetValue());

            isAttacking = true;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
