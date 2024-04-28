using UnityEngine;

[System.Serializable]
public class Enemy : MonoBehaviour
{
    [Header("Level")]
    public int level = 1;

    [Header("HP")]
    public int maxHealth = 50;
    private int currentHealth;

    [Header("Attack")]
    public int damage = 10;
    public int criticalDamage;
    public float criticalChance = 0.1f;
    public float attackCooldown = 1f;
    private float timeSinceLastAttack = 0;
    private bool isAttacking = false;

    [Header("Speed")]
    public float moveSpeed = 3f;

    [Header("UI")]
    public ResourceBars resourceBars;

    private Player player;

    void Start()
    {
        maxHealth = 50 + level * 2;
        currentHealth = maxHealth;

        damage = 10 + level * 2;
        criticalDamage = damage * 2;

        resourceBars.SetMaxHealth(maxHealth);
        player = FindObjectOfType<Player>();
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
                player.TakeDamage(criticalDamage);
            else
                player.TakeDamage(damage);

            isAttacking = true;
        }
    }

    public void TakeDamage(int damageTaken)
    {
        currentHealth -= damageTaken;
        resourceBars.SetHealth(currentHealth);

        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
