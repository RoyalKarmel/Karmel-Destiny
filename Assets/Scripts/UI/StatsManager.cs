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

    public TMP_Text timerText;

    [Header("Level text")]
    public TMP_Text levelText;
    public TMP_Text expText;
    public TMP_Text expToLevelUpText;

    [Header("Stats text")]
    public TMP_Text currentHealthText;
    public TMP_Text maxHealthText;
    public TMP_Text damageText;
    public TMP_Text armorText;
    public TMP_Text speedText;

    private const string colorCode = "#FF7402";

    public void SetTimerText(string gameTime)
    {
        timerText.text = gameTime;
    }

    #region Level Text
    public void SetLevelText(int level)
    {
        SetFormattedText(levelText, "Level: ", level);
    }

    public void SetExpText(int exp)
    {
        SetText(expText, exp.ToString());
    }

    public void SetExpToLevelUpText(int expToLevelUp)
    {
        SetText(expToLevelUpText, expToLevelUp.ToString());
    }
    #endregion

    #region Stats Text
    // HP
    public void SetCurrentHealthText(int currentHealth)
    {
        SetFormattedText(currentHealthText, "Current Health: ", currentHealth);
    }

    public void SetMaxHealthText(int maxHealth)
    {
        SetFormattedText(maxHealthText, "Max Health: ", maxHealth);
        SetCurrentHealthText(maxHealth); // Update current health as well
    }

    // Damage
    public void SetDamageText(int damage)
    {
        SetFormattedText(damageText, "Damage: ", damage);
    }

    public void SetArmorText(int armor)
    {
        SetFormattedText(armorText, "Armor: ", armor);
    }

    // Speed
    public void SetSpeedText(float speed)
    {
        SetFormattedText(speedText, "Speed: ", speed);
    }
    #endregion

    private void SetText(TMP_Text textComponent, string text)
    {
        textComponent.text = text;
    }

    private void SetFormattedText(TMP_Text textComponent, string label, int value)
    {
        textComponent.text = $"{label}<color={colorCode}>{value}</color>";
    }

    private void SetFormattedText(TMP_Text textComponent, string label, float value)
    {
        textComponent.text = $"{label}<color={colorCode}>{value}</color>";
    }
}
