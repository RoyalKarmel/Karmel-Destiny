using TMPro;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public TMP_Text timerText;
    public TMP_Text levelText;
    public TMP_Text currentHealthText;
    public TMP_Text maxHealthText;
    public TMP_Text damageText;
    public TMP_Text speedText;

    public void SetTimerText(string gameTime)
    {
        timerText.text = gameTime;
    }

    public void SetLevelText(int level)
    {
        levelText.text = "Level: <color=blue>" + level + "</color>";
    }

    public void SetCurrentHealthText(int currentHealth)
    {
        currentHealthText.text = "Current Health: <color=blue>" + currentHealth + "</color>";
    }

    public void SetMaxHealthText(int maxHealth)
    {
        maxHealthText.text = "Max Health: <color=blue>" + maxHealth + "</color>";

        SetCurrentHealthText(maxHealth);
    }

    public void SetDamageText(int damage)
    {
        damageText.text = "Damage: <color=blue>" + damage + "</color>";
    }

    public void SetSpeedText(float speed)
    {
        speedText.text = "Speed: <color=blue>" + speed + "</color>";
    }
}
