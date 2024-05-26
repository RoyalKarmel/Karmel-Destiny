using UnityEngine;

public class PlayerStats : CharacterStats
{
    [Header("Level")]
    public int exp = 0;
    private int expToLevelUp = 100;

    private int healthIncrease = 20;

    void Start()
    {
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;

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
        StatsManager.instance.SetExpText(exp);
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

        // damage += dmgIncrease;
    }
    #endregion

    void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if (newItem != null)
        {
            damage.AddModifier(newItem.damageModifier);
            StatsManager.instance.SetDamageText(damage.GetValue());

            armor.AddModifier(newItem.armorModifier);
            StatsManager.instance.SetArmorText(armor.GetValue());

            speed.AddModifier(newItem.speedModifier);
            StatsManager.instance.SetSpeedText(speed.GetValue());
        }

        if (oldItem != null)
        {
            damage.RemoveModifier(oldItem.damageModifier);
            StatsManager.instance.SetDamageText(damage.GetValue());

            armor.RemoveModifier(oldItem.armorModifier);
            StatsManager.instance.SetArmorText(armor.GetValue());

            speed.RemoveModifier(oldItem.speedModifier);
            StatsManager.instance.SetSpeedText(speed.GetValue());
        }
    }

    public override void Heal(int amount)
    {
        base.Heal(amount);

        StatsManager.instance.SetCurrentHealthText(currentHealth);
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        StatsManager.instance.SetCurrentHealthText(currentHealth);
    }

    #region Utils
    void SetUI()
    {
        healthBar.SetMaxValue(maxHealth);

        StatsManager.instance.SetLevelText(level);
        StatsManager.instance.SetExpText(exp);
        StatsManager.instance.SetExpToLevelUpText(expToLevelUp);

        StatsManager.instance.SetMaxHealthText(maxHealth);
        StatsManager.instance.SetDamageText(damage.GetValue());
        StatsManager.instance.SetArmorText(armor.GetValue());
        StatsManager.instance.SetSpeedText(speed.GetValue());
    }
    #endregion
}
