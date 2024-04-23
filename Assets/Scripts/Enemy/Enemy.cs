using UnityEngine;

[System.Serializable]
public class Enemy
{
    [Header("Stats")]
    public int hp = 50;
    public int maxHp = 50;
    public int damage = 10;
    // public int criticalDamage = damage * 2;
    public float moveSpeed = 3f;

    [Header("Move variables")]
    public float moveDuration = 5f;
    public float randomMoveRange = 10f;
    public float changeDirectionInterval = 2f;

    [Header("Ranges")]
    public float returnRange = 15f;
    public float attackRange = 1.5f;
    public float detectionRange = 5f;

    public void CalculateStats(int enemyLevel)
    {
        maxHp = 50 + enemyLevel * 2;
        hp = maxHp;

        damage = 10 + enemyLevel * 2;
    }

    public void Attack()
    {
        Debug.Log(damage);
    }

    public void TakeDamage(int damageTaken)
    {
        hp -= damageTaken;
    }

    public bool isDead()
    {
        return hp <= 0;
    }
}
