using TMPro;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    #region Singleton

    public static StatsManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one instance of Stats Manager found!");
            return;
        }

        instance = this;
    }

    #endregion


    [Header("Level text")]
    public TMP_Text levelText;
    public TMP_Text expText;
    public TMP_Text expToLevelUpText;

    [Header("Stats text")]
    public TMP_Text currentHealthText;
    public TMP_Text maxHealthText;
    public TMP_Text meleeDamageText;
    public TMP_Text rangeDamageText;
    public TMP_Text armorText;
    public TMP_Text speedText;

    #region Level Text
    public void SetLevelText(int level)
    {
        SetText(levelText, level);
    }

    public void SetExpText(int exp)
    {
        SetText(expText, exp);
    }

    public void SetExpToLevelUpText(int expToLevelUp)
    {
        SetText(expToLevelUpText, expToLevelUp);
    }
    #endregion

    #region Stats Text
    // HP
    public void SetCurrentHealthText(int currentHealth)
    {
        SetText(currentHealthText, currentHealth);
    }

    public void SetMaxHealthText(int maxHealth)
    {
        SetText(maxHealthText, maxHealth);
        SetCurrentHealthText(maxHealth); // Update current health as well
    }

    // Damage
    public void SetMeleeDamageText(float damage)
    {
        SetText(meleeDamageText, damage);
    }

    public void SetRangeDamageText(float damage)
    {
        SetText(rangeDamageText, damage);
    }

    // Armor
    public void SetArmorText(float armor)
    {
        SetText(armorText, armor);
    }

    // Speed
    public void SetSpeedText(float speed)
    {
        SetText(speedText, speed);
    }
    #endregion

    private void SetText(TMP_Text textComponent, float value)
    {
        textComponent.text = value.ToString();
    }
}
