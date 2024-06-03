using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField]
    private CharacterStats characterStats;

    // public Animator animator;

    [Header("Attack")]
    public float attackRange = 0.5f;
    public float criticalChance = 0.1f;

    public Transform attackPoint;
    public LayerMask enemyLayers;

    public float maxDistanceFromPlayer = 1f;

    // Update is called once per frame
    void Update()
    {
        UpdateAttackPointPosition();

        if (Input.GetMouseButtonDown(0))
            Attack();
    }

    void UpdateAttackPointPosition()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        Vector3 direction = mousePosition - transform.position;

        if (direction.magnitude > maxDistanceFromPlayer)
            direction = direction.normalized * maxDistanceFromPlayer;

        attackPoint.position = transform.position + direction;
    }

    void Attack()
    {
        // Play an attack animation
        // animator.SetTrigger("attack");

        // Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(
            attackPoint.position,
            attackRange,
            enemyLayers
        );

        // Damage enemies
        foreach (Collider2D enemy in hitEnemies)
        {
            EnemyStats enemyStats = enemy.GetComponent<EnemyStats>();

            float randomValue = Random.value;
            if (randomValue <= criticalChance)
                enemyStats.TakeDamage(characterStats.damage.GetValue(), true);
            else
                enemyStats.TakeDamage(characterStats.damage.GetValue());
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
