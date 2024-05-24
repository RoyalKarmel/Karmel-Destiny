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
    public int money { get; private set; }

    public void AddCurrency(int value)
    {
        money += value;
        UpdateUI();
    }

    public void DecreaseCurrency(int value)
    {
        money -= value;
        if (money < 0)
            money = 0;

        UpdateUI();
    }

    void UpdateUI()
    {
        currencyText.text = money.ToString();
    }
}
