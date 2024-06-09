using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField]
    private PlayerStats playerStats;

    // public Animator animator;

    [Header("Components")]
    public Transform attackPoint;
    public LayerMask enemyLayers;

    public float maxDistanceFromPlayer = 1f;

    // Update is called once per frame
    void Update()
    {
        UpdateAttackPointPosition();

        if (Input.GetMouseButtonDown(0))
            MeleeAttack();

        if (Input.GetMouseButtonDown(1))
            RangeAttack();
    }

    #region Combat
    void MeleeAttack()
    {
        // Play an attack animation
        // animator.SetTrigger("attack");

        // Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(
            attackPoint.position,
            playerStats.meleeRange,
            enemyLayers
        );

        // Damage enemies
        foreach (Collider2D enemy in hitEnemies)
        {
            EnemyStats enemyStats = enemy.GetComponent<EnemyStats>();

            float randomValue = Random.value;
            if (randomValue <= playerStats.criticalChance)
                enemyStats.TakeDamage(playerStats.damage.GetValue(), true);
            else
                enemyStats.TakeDamage(playerStats.damage.GetValue());

            // Decrease durability of equipped weapon
            if (playerStats.currentEquipment.ContainsKey(EquipmentSlot.Weapon))
                playerStats.currentEquipment[EquipmentSlot.Weapon].DecreaseDurability();
        }
    }

    void RangeAttack()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        Vector3 direction = (mousePosition - transform.position).normalized;

        GameObject projectile = Instantiate(
            GameAssets.instance.playerProjectile,
            transform.position,
            Quaternion.identity
        );
        projectile.transform.SetParent(transform);

        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = direction * playerStats.projectileSpeed;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        projectile.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    #endregion

    #region Utils
    void UpdateAttackPointPosition()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        Vector3 direction = mousePosition - transform.position;

        if (direction.magnitude > maxDistanceFromPlayer)
            direction = direction.normalized * maxDistanceFromPlayer;

        attackPoint.position = transform.position + direction;
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        // Melee attack
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, playerStats.meleeRange);

        // Range attack
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, playerStats.projectileRange);
    }
    #endregion
}
