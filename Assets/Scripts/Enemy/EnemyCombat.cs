using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public float criticalChance = 0.1f;
    public float attackCooldown = 1f;
    private float timeSinceLastAttack = 0;
    private bool isAttacking = false;

    private PlayerStats player;
    private EnemyStats enemyStats;

    void Start()
    {
        enemyStats = GetComponent<EnemyStats>();

        player = PlayerManager.instance.playerStats;
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
    }

    public void Attack()
    {
        if (!isAttacking)
        {
            float randomValue = Random.value;
            if (randomValue < criticalChance)
                player.TakeDamage(enemyStats.criticalDamage);
            else
                player.TakeDamage(enemyStats.damage.GetValue());

            isAttacking = true;
        }
    }
}
