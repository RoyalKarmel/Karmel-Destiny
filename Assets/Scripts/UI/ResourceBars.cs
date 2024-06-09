using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceBars : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public TMP_Text resourceText;

    public void SetMaxValue(float maxAmount)
    {
        slider.maxValue = maxAmount;
        slider.value = maxAmount;

        fill.color = gradient.Evaluate(1f);

        if (resourceText != null)
            resourceText.text = maxAmount.ToString();
    }

    public void SetValue(float amount)
    {
        slider.value = amount;

        fill.color = gradient.Evaluate(slider.normalizedValue);

        if (resourceText != null)
            resourceText.text = amount.ToString();
    }
}
