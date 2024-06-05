using UnityEngine;

public class PlayerStats : CharacterStats
{
    [Header("Range Attack")]
    public Stat projectileDamage;

    public int exp { get; private set; }
    private int expToLevelUp = 100;

    private int healthIncrease = 20;

    public StatsManager statsManager { get; private set; }

    void Start()
    {
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
        statsManager = StatsManager.instance;

        exp = 0;

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
        maxHealth += healthIncrease;
        Heal(maxHealth);
    }
    #endregion

    void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if (newItem != null)
        {
            damage.AddModifier(newItem.damageModifier);
            statsManager.SetMeleeDamageText(damage.GetValue());

            projectileDamage.AddModifier(newItem.projectileDamageModifier);
            statsManager.SetRangeDamageText(projectileDamage.GetValue());

            armor.AddModifier(newItem.armorModifier);
            statsManager.SetArmorText(armor.GetValue());

            speed.AddModifier(newItem.speedModifier);
            statsManager.SetSpeedText(speed.GetValue());
        }

        if (oldItem != null)
        {
            damage.RemoveModifier(oldItem.damageModifier);
            statsManager.SetMeleeDamageText(damage.GetValue());

            projectileDamage.RemoveModifier(newItem.projectileDamageModifier);
            statsManager.SetRangeDamageText(projectileDamage.GetValue());

            armor.RemoveModifier(oldItem.armorModifier);
            statsManager.SetArmorText(armor.GetValue());

            speed.RemoveModifier(oldItem.speedModifier);
            statsManager.SetSpeedText(speed.GetValue());
        }
    }

    public override void Heal(int amount)
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
        healthBar.SetMaxValue(maxHealth);

        statsManager.SetLevelText(level);
        statsManager.SetExpText(exp);
        statsManager.SetExpToLevelUpText(expToLevelUp);

        statsManager.SetMaxHealthText(maxHealth);
        statsManager.SetMeleeDamageText(damage.GetValue());
        statsManager.SetRangeDamageText(projectileDamage.GetValue());
        statsManager.SetArmorText(armor.GetValue());
        statsManager.SetSpeedText(speed.GetValue());
    }
    #endregion
}
