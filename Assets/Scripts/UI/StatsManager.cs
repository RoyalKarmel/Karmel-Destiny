using TMPro;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public TMP_Text timerText;

    [Header("Level text")]
    public TMP_Text levelText;
    public TMP_Text expText;
    public TMP_Text expToLevelUpText;

    [Header("Stats text")]
    public TMP_Text currentHealthText;
    public TMP_Text maxHealthText;
    public TMP_Text currentManaText;
    public TMP_Text maxManaText;
    public TMP_Text damageText;
    public TMP_Text speedText;

    public void SetTimerText(string gameTime)
    {
        timerText.text = gameTime;
    }

    #region Level Text
    public void SetLevelText(int level)
    {
        levelText.text = "Level: <color=#FF7402>" + level + "</color>";
    }

    public void SetExpText(int exp)
    {
        expText.text = exp.ToString();
    }

    public void SetExpToLevelUpText(int expToLevelUp)
    {
        expToLevelUpText.text = expToLevelUp.ToString();
    }
    #endregion

    #region Stats Text
    // HP
    public void SetCurrentHealthText(int currentHealth)
    {
        currentHealthText.text = "Current Health: <color=#FF7402>" + currentHealth + "</color>";
    }

    public void SetMaxHealthText(int maxHealth)
    {
        maxHealthText.text = "Max Health: <color=#FF7402>" + maxHealth + "</color>";

        SetCurrentHealthText(maxHealth);
    }

    // Mana
    public void SetCurrentManaText(int currentMana)
    {
        currentManaText.text = "Current Mana: <color=#FF7402>" + currentMana + "</color>";
    }

    public void SetMaxManaText(int maxMana)
    {
        currentManaText.text = "Max Mana: <color=#FF7402>" + maxMana + "</color>";

        SetCurrentManaText(maxMana);
    }

    // Damage
    public void SetDamageText(int damage)
    {
        damageText.text = "Damage: <color=#FF7402>" + damage + "</color>";
    }

    // Speed
    public void SetSpeedText(float speed)
    {
        speedText.text = "Speed: <color=#FF7402>" + speed + "</color>";
    }
    #endregion
}
