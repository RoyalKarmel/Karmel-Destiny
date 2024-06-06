using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public float attackCooldown = 1f;
    float timeSinceLastAttack = 0;

    [HideInInspector]
    public bool isAttacking;

    public EnemyStats enemyStats;
    public Transform player { get; private set; }

    EnemyAttack enemyAttack;

    void Start()
    {
        isAttacking = false;

        player = PlayerManager.instance.player.transform;
        enemyAttack = GetComponent<EnemyAttack>();
        enemyAttack.Initialize(this, PlayerManager.instance.playerStats);
    }

    void Update()
    {
        if (isAttacking)
            HandleAttackCooldown();
        else
            HandleCombat();
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

    void HandleCombat()
    {
        switch (enemyStats.enemyType)
        {
            case EnemyType.Melee:
                HandleMeleeCombat();
                break;
            case EnemyType.Range:
                HandleRangeCombat();
                break;
            case EnemyType.Necromancer:
                HandleNecromancerCombat();
                break;
        }
    }

    // Handle Melee combat
    void HandleMeleeCombat()
    {
        if (Vector3.Distance(transform.position, player.position) <= enemyStats.meleeRange)
            enemyAttack.MeleeAttack();
    }

    // Handle Range combat
    void HandleRangeCombat()
    {
        if (Vector3.Distance(transform.position, player.position) <= enemyStats.projectileRange)
            enemyAttack.RangeAttack();
    }

    // Handle Necromancer combat
    void HandleNecromancerCombat()
    {
        if (Vector3.Distance(transform.position, player.position) <= enemyStats.projectileRange)
            enemyAttack.SummonEnemies();
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
