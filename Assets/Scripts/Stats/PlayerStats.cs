using UnityEngine;

public class PlayerStats : CharacterStats
{
    [Header("Range Attack")]
    public Stat projectileDamage;

    public int exp { get; private set; } = 0;
    private int expToLevelUp = 100;

    private int healthIncrease = 20;

    public StatsManager statsManager { get; private set; }

    void Start()
    {
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
        statsManager = StatsManager.instance;

        SetUI();
    }

    void Update()
    {
        if (exp >= expToLevelUp)
            LevelUp();
    }

    #region Level
    public void GainExperience(int amount)
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
        maxHealth.AddModifier(healthIncrease);
        Heal(maxHealth.GetValue());
    }
    #endregion

    void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if (newItem != null)
        {
            damage.AddModifier(newItem.damageModifier);
            maxHealth.AddModifier(newItem.healthModifier);
            projectileDamage.AddModifier(newItem.projectileDamageModifier);
            speed.AddModifier(newItem.speedModifier);

            healthBar.SetMaxValue(maxHealth.GetValue());
            healthBar.SetValue(currentHealth);
            SetEquipmentStatsUI();
        }

        if (oldItem != null)
        {
            damage.RemoveModifier(oldItem.damageModifier);
            maxHealth.RemoveModifier(oldItem.healthModifier);
            projectileDamage.RemoveModifier(oldItem.projectileDamageModifier);
            speed.RemoveModifier(oldItem.speedModifier);

            healthBar.SetMaxValue(maxHealth.GetValue());
            healthBar.SetValue(currentHealth);
            SetEquipmentStatsUI();
        }
    }

    public override void Heal(float amount)
    {
        base.Heal(amount);

        statsManager.SetCurrentHealthText(currentHealth);
    }

    public override void TakeDamage(float damage, bool isCriticalHit = false)
    {
        base.TakeDamage(damage, isCriticalHit);

        statsManager.SetCurrentHealthText(currentHealth);
    }

    #region Utils
    void SetUI()
    {
        healthBar.SetMaxValue(maxHealth.GetValue());

        statsManager.SetLevelText(level);
        statsManager.SetExpText(exp);
        statsManager.SetExpToLevelUpText(expToLevelUp);

        SetEquipmentStatsUI();
    }

    void SetEquipmentStatsUI()
    {
        statsManager.SetMeleeDamageText(damage.GetValue());
        statsManager.SetRangeDamageText(projectileDamage.GetValue());
        statsManager.SetMaxHealthText(maxHealth.GetValue());
        statsManager.SetCurrentHealthText(currentHealth);
        statsManager.SetSpeedText(speed.GetValue());
    }
    #endregion
}
