using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    PlayerStats playerStats;
    Transform player;
    EnemyCombat enemyCombat;
    EnemyStats enemyStats;
    CloneStats cloneStats;

    public void Initialize(EnemyCombat combat, PlayerStats playerStats, Transform player)
    {
        this.enemyCombat = combat;
        this.playerStats = playerStats;
        this.enemyStats = combat.enemyStats;
        this.player = player;
        this.cloneStats = GetComponent<CloneStats>();
    }

    public void Attack()
    {
        if (Vector3.Distance(transform.position, player.position) <= enemyCombat.attackRange)
        {
            switch (enemyStats.enemyType)
            {
                case EnemyType.Melee:
                    MeleeAttack();
                    break;

                case EnemyType.Range:
                    RangeAttack();
                    break;

                case EnemyType.Necromancer:
                    SummonEnemies();
                    break;
            }
        }

        if (cloneStats != null)
        {
            if (cloneStats.canClone)
            {
                cloneStats.cloneTimer += Time.deltaTime;
                if (cloneStats.cloneTimer >= cloneStats.cloneInterval)
                {
                    cloneStats.Clone();
                    cloneStats.cloneTimer = 0f;
                }
            }
        }
    }

    #region Melee
    void MeleeAttack()
    {
        if (!enemyCombat.isAttacking)
        {
            float randomValue = Random.value;
            if (randomValue <= enemyStats.criticalChance)
                playerStats.TakeDamage(enemyStats.damage.GetValue(), true);
            else
                playerStats.TakeDamage(enemyStats.damage.GetValue());

            enemyCombat.isAttacking = true;
        }
    }
    #endregion

    #region Range
    void RangeAttack()
    {
        Vector3 direction = (enemyCombat.player.position - transform.position).normalized;

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

        enemyCombat.isAttacking = true;
    }
    #endregion


    #region Necromancer
    void SummonEnemies()
    {
        if (!enemyCombat.isAttacking)
        {
            var enemyPrefab = GetRandomEnemyPrefab();

            if (enemyPrefab != null)
                Instantiate(enemyPrefab, transform.position, Quaternion.identity);

            enemyCombat.isAttacking = true;
        }
    }

    GameObject GetRandomEnemyPrefab()
    {
        var enemies = GameAssets.instance.enemies.enemies;
        if (enemies == null || enemies.Count == 0)
            return null;

        List<GameObject> validEnemies = new List<GameObject>();

        foreach (var enemy in enemies)
        {
            EnemyStats enemyStats = enemy.GetComponent<EnemyStats>();
            if (enemyStats != null && enemyStats.enemyType != EnemyType.Necromancer)
                validEnemies.Add(enemy.gameObject);
        }

        if (validEnemies.Count == 0)
            return null;

        int randomIndex = Random.Range(0, validEnemies.Count);
        return validEnemies[randomIndex];
    }
    #endregion
}
