using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceBars : MonoBehaviour
{
    [Header("Health")]
    public Slider healthSlider;
    public Gradient healthGradient;
    public Image healthFill;
    public TMP_Text healthText;

    [Header("Mana")]
    public Slider manaSlider;
    public Image manaFill;
    public TMP_Text manaText;

    #region Health
    public void SetMaxHealth(int health)
    {
        healthSlider.maxValue = health;
        healthSlider.value = health;

        healthFill.color = healthGradient.Evaluate(1f);

        if (healthText != null)
            healthText.text = "hp: " + health;
    }

    public void SetHealth(int health)
    {
        healthSlider.value = health;

        healthFill.color = healthGradient.Evaluate(healthSlider.normalizedValue);

        if (healthText != null)
            healthText.text = "hp: " + health;
    }
    #endregion

    #region Mana
    public void SetMaxMana(int mana)
    {
        manaSlider.maxValue = mana;
        manaSlider.value = mana;

        if (manaText != null)
            manaText.text = "mana: " + mana;
    }

    public void SetMana(int mana)
    {
        manaSlider.value = mana;

        if (manaText != null)
            manaText.text = "mana: " + mana;
    }
    #endregion
}
