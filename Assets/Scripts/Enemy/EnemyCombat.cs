using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [Header("Attack Settings")]
    public float attackCooldown = 1f;
    float timeSinceLastAttack = 0;
    public float attackRange { get; private set; }

    [HideInInspector]
    public bool isAttacking = false;

    [HideInInspector]
    public float cloneTimer = 0f;

    public EnemyStats enemyStats;
    public Transform player { get; private set; }

    EnemyAttack enemyAttack;

    void Start()
    {
        if (enemyStats.enemyType == EnemyType.Melee)
            attackRange = enemyStats.meleeRange;
        else
            attackRange = enemyStats.projectileRange;

        player = PlayerManager.instance.player.transform;
        enemyAttack = GetComponent<EnemyAttack>();
        enemyAttack.Initialize(this, PlayerManager.instance.playerStats, player);
    }

    void Update()
    {
        if (isAttacking)
            HandleAttackCooldown();
        else
            enemyAttack.Attack();
    }

    #region Handlers

    void HandleAttackCooldown()
    {
        timeSinceLastAttack += Time.deltaTime;
        if (timeSinceLastAttack >= attackCooldown)
        {
            isAttacking = false;
            timeSinceLastAttack = 0;
        }
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
            Gizmos.DrawWireSphere(transform.position, enemyStats.projectileRange);
    }
}
