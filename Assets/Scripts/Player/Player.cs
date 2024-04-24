using UnityEngine;

public class Player : MonoBehaviour
{
    private int level = 1;

    [Header("HP")]
    public int maxHealth = 100;
    public int health;

    [Header("Damage")]
    public int damage = 10;
    private int criticalDamage;

    [Header("Speed")]
    public float speed = 5f;

    [Header("UI")]
    public HealthBar healthBar;
    public GameObject inventory;
    public StatsManager statsManager;

    void Start()
    {
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        statsManager.SetLevelText(level);
        statsManager.SetMaxHealthText(maxHealth);
        statsManager.SetDamageText(damage);
        statsManager.SetSpeedText(speed);

        criticalDamage = damage * 2;
    }

    // Heal player
    public void Heal(int amount)
    {
        health += amount;

        if (health > maxHealth)
            health = maxHealth;

        healthBar.SetHealth(health);
        statsManager.SetCurrentHealthText(health);
    }

    // Take damage
    public void TakeDamage(int amount)
    {
        health -= amount;
        healthBar.SetHealth(health);
        statsManager.SetCurrentHealthText(health);

        if (health <= 0)
            Die();
    }

    // Open / Close inventory
    public void ToggleInventory()
    {
        if (inventory.activeSelf)
            inventory.SetActive(false);
        else
            inventory.SetActive(true);
    }

    // Yu ded!
    void Die()
    {
        Debug.Log("YU DED.");
    }
}
