using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    // [Header("Level")]
    public int level = 1;
    public new string name { get; private set; }

    [Header("Health & Armor")]
    public int maxHealth = 100;
    public int currentHealth { get; private set; }
    public ResourceBars healthBar;
    public Stat armor;

    [Header("Speed")]
    public Stat speed;

    [Header("Melee Combat")]
    public Stat damage;
    public float meleeRange;

    [Header("Range Combat")]
    public float projectileRange;
    public float projectileSpeed;

    [Header("Combat Modifiers")]
    public float criticalHitMultiplier = 1.5f;
    public float criticalChance = 0.1f;

    void Awake()
    {
        currentHealth = maxHealth;

        healthBar.SetMaxValue(maxHealth);

        if (string.IsNullOrEmpty(name))
            name = gameObject.name;
    }

    public virtual void Heal(int amount)
    {
        currentHealth += amount;

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        healthBar.SetValue(currentHealth);
    }

    public virtual void TakeDamage(float damage, bool isCriticalHit = false)
    {
        if (isCriticalHit)
            damage *= criticalHitMultiplier;

        damage -= armor.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        currentHealth -= (int)damage;
        healthBar.SetValue(currentHealth);

        Debug.Log(transform.name + " takes " + damage + " damage.");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Debug.Log(transform.name + " died.");
    }
}
