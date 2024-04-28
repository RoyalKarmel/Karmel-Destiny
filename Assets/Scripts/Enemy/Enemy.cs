using UnityEngine;

[System.Serializable]
public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    public int level = 1;
    public int maxHealth = 50;
    private int currentHealth;
    public int damage = 10;
    public int criticalDamage;
    public float criticalChance = 0.1f;
    public float moveSpeed = 3f;

    [Header("UI")]
    public HealthBar healthBar;

    private Player player;

    void Start()
    {
        maxHealth = 50 + level * 2;
        currentHealth = maxHealth;

        damage = 10 + level * 2;
        criticalDamage = damage * 2;

        healthBar.SetMaxHealth(maxHealth);
        player = FindObjectOfType<Player>();
    }

    public void Attack()
    {
        float randomValue = Random.value;
        if (randomValue < criticalChance)
            player.TakeDamage(criticalDamage);
        else
            player.TakeDamage(damage);
        // Debug.Log(damage);
    }

    public void TakeDamage(int damageTaken)
    {
        currentHealth -= damageTaken;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
