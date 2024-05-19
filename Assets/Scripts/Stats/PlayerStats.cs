using UnityEngine;

public class PlayerStats : CharacterStats
{
    [Header("Level")]
    public int exp = 0;
    private int expToLevelUp = 100;

    [Header("UI")]
    public StatsManager statsManager;

    private int healthIncrease = 20;

    // private int dmgIncrease = 5;

    // Start is called before the first frame update
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

        // damage += dmgIncrease;
    }
    #endregion

    void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if (newItem != null)
        {
            damage.AddModifier(newItem.damageModifier);
            statsManager.SetDamageText(damage.GetValue());

            armor.AddModifier(newItem.armorModifier);
            statsManager.SetArmorText(armor.GetValue());

            speed.AddModifier(newItem.speedModifier);
            statsManager.SetSpeedText(speed.GetValue());
        }

        if (oldItem != null)
        {
            damage.RemoveModifier(oldItem.damageModifier);
            statsManager.SetDamageText(damage.GetValue());

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

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

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
        statsManager.SetDamageText(damage.GetValue());
        statsManager.SetArmorText(armor.GetValue());
        statsManager.SetSpeedText(speed.GetValue());
    }
    #endregion
}
