using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    // [Header("Level")]
    public int level = 1;

    [Header("Health")]
    public int maxHealth = 100;
    public int currentHealth { get; private set; }
    public ResourceBars healthBar;

    [Header("Combat")]
    public Stat armor;
    public Stat damage;
    public float criticalDamage { get; private set; }

    [Header("Speed")]
    public Stat speed;

    void Awake()
    {
        currentHealth = maxHealth;
        criticalDamage = damage.GetValue() * 2;

        healthBar.SetMaxValue(maxHealth);
    }

    public virtual void Heal(int amount)
    {
        currentHealth += amount;

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        healthBar.SetValue(currentHealth);
    }

    public virtual void TakeDamage(float damage)
    {
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
