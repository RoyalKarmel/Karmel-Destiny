using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    // [Header("Level")]
    public int level = 1;
    public new string name { get; private set; }

    [Header("Health")]
    public int maxHealth = 100;
    public int currentHealth { get; private set; }
    public ResourceBars healthBar;

    [Header("Combat")]
    public Stat armor;
    public Stat damage;
    public float criticalHitMultiplier { get; private set; }

    [Header("Speed")]
    public Stat speed;

    void Awake()
    {
        currentHealth = maxHealth;
        criticalHitMultiplier = 1.5f;

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
