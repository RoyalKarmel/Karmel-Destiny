using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Level")]
    private int level = 1;
    public int exp = 0;
    public int expToLevelUp = 100;

    [Header("HP")]
    public int maxHealth = 100;
    public int health;
    private int healthIncrease = 20;

    [Header("Attack")]
    public int damage = 10;
    public float attackRange = 0.5f;
    public int criticalDamage;
    public float criticalChance = 0.1f;
    private int dmgIncrease = 5;

    [Header("Speed")]
    public float speed = 5f;

    [Header("UI")]
    public HealthBar healthBar;
    public GameObject inventory;
    public StatsManager statsManager;

    void Start()
    {
        health = maxHealth;

        SetUI();
        CalculateCriticalDamage();
    }

    void Update()
    {
        if (exp >= expToLevelUp)
            LevelUp();
    }

    #region Level
    public void SetExp(int amount)
    {
        exp += amount;
        statsManager.SetExpText(exp);
    }

    void LevelUp()
    {
        level++;
        exp -= expToLevelUp;
        expToLevelUp += 50;

        IncreaseStats();
        SetUI();

        Debug.Log("LEVEL UP!");
    }

    void IncreaseStats()
    {
        maxHealth += healthIncrease;
        health = maxHealth;

        damage += dmgIncrease;

        CalculateCriticalDamage();
    }
    #endregion

    #region HP
    // Heal player
    public void Heal(int amount)
    {
        health += amount;

        if (health > maxHealth)
            health = maxHealth;

        healthBar.SetHealth(health);
        statsManager.SetCurrentHealthText(health);
    }
    #endregion

    #region Damage
    // Take damage
    public void TakeDamage(int amount)
    {
        health -= amount;
        healthBar.SetHealth(health);
        statsManager.SetCurrentHealthText(health);

        if (health <= 0)
            Die();
    }

    void CalculateCriticalDamage()
    {
        criticalDamage = damage * 2;
    }

    // Yu ded!
    void Die()
    {
        Debug.Log("YU DED.");
    }
    #endregion

    #region Utils
    // Open / Close inventory
    public void ToggleInventory()
    {
        if (inventory.activeSelf)
            inventory.SetActive(false);
        else
            inventory.SetActive(true);
    }

    void SetUI()
    {
        healthBar.SetMaxHealth(maxHealth);

        statsManager.SetLevelText(level);
        statsManager.SetExpText(exp);
        statsManager.SetExpToLevelUpText(expToLevelUp);

        statsManager.SetMaxHealthText(maxHealth);
        statsManager.SetDamageText(damage);
        statsManager.SetSpeedText(speed);
    }
    #endregion
}
