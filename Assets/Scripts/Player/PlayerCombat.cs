using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    // public Animator animator;

    public Transform attackPoint;
    public Player player;
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
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, player.attackRange, enemyLayers);

        // Damage enemies
        foreach (Collider2D enemy in hitEnemies)
        {
            float randomValue = Random.value;
            if (randomValue < player.criticalChance)
                enemy.GetComponent<Enemy>().TakeDamage(player.criticalDamage);
            else
                enemy.GetComponent<Enemy>().TakeDamage(player.damage);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, player.attackRange);
    }
}
