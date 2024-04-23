using UnityEngine;

public class Player : MonoBehaviour
{
    private int level = 1;

    [Header("HP")]
    public int maxHealth = 100;
    public int health;

    [Header("Damage")]
    public int damage = 10;

    [Header("Speed")]
    public float speed = 5f;

    [Header("UI")]
    public HealthBar healthBar;

    void Start()
    {
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Heal player
    public void Heal(int amount)
    {
        health += amount;

        if (health > maxHealth)
            health = maxHealth;
    }

    // Take damage
    public void TakeDamage(int amount)
    {
        health -= amount;
        healthBar.SetHealth(health);

        if (health <= 0)
            Die();
    }

    // Yu ded!
    void Die()
    {
        Debug.Log("YU DED.");
    }
}
