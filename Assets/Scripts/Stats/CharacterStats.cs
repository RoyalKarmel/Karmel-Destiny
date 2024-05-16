using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth = 100;
    public int currentHealth { get; private set; }

    [Header("Combat")]
    public Stat armor;
    public Stat damage;
    public int criticalDamage { get; private set; }

    [Header("Speed")]
    public Stat speed;

    void Awake()
    {
        currentHealth = maxHealth;
        criticalDamage = damage.GetValue() * 2;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            TakeDamage(20);
    }

    public virtual void Heal(int amount)
    {
        currentHealth += amount;

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }

    public virtual void TakeDamage(int damage)
    {
        damage -= armor.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        currentHealth -= damage;
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
