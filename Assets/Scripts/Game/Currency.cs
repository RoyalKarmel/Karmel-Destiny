using TMPro;
using UnityEngine;

public class Currency : MonoBehaviour
{
    #region Singleton
    public static Currency instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Currency found!");
            return;
        }

        instance = this;
    }

    #endregion

    public TMP_Text currencyText;
    public int amount { get; private set; }

    public void AddCurrency(int value)
    {
        amount += value;
        UpdateUI();
    }

    public void RemoveCurrency(int value)
    {
        amount -= value;
        if (amount < 0)
            amount = 0;

        UpdateUI();
    }

    void UpdateUI()
    {
        currencyText.text = amount.ToString();
    }
}
