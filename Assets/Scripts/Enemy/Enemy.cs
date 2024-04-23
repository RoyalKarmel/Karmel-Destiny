using UnityEngine;

[System.Serializable]
public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    public int level = 1;
    public int maxHp = 50;
    private int hp;
    public int damage = 10;
    private int criticalDamage;
    public float moveSpeed = 3f;

    [Header("Move variables")]
    public float moveDuration = 5f;
    public float randomMoveRange = 10f;
    public float changeDirectionInterval = 2f;

    [Header("Ranges")]
    public float returnRange = 15f;
    public float attackRange = 1.5f;
    public float detectionRange = 5f;

    [Header("UI")]
    public HealthBar healthBar;

    void Start()
    {
        maxHp = 50 + level * 2;
        hp = maxHp;

        healthBar.SetMaxHealth(maxHp);

        damage = 10 + level * 2;
        criticalDamage = damage * 2;
    }

    public void Attack()
    {
        Debug.Log(damage);
    }

    public void TakeDamage(int damageTaken)
    {
        hp -= damageTaken;
        healthBar.SetHealth(hp);
    }

    public bool isDead()
    {
        return hp <= 0;
    }
}
