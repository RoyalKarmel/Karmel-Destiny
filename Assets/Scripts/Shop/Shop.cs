using UnityEngine;

public class Shop : MonoBehaviour
{
    #region Singleton

    public static Shop instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Shop found!");
            return;
        }

        instance = this;
    }

    #endregion

    [Header("Products")]
    public Transform productsUI;
    public ProductSlot[] productsSlots { get; private set; }

    [Header("Special Products")]
    public Transform specialProductsUI;
    public ProductSlot[] specialProductsSlots { get; private set; }

    [Header("Shop UI")]
    public GameObject shopUI;

    void Start()
    {
        shopUI.SetActive(false);

        productsSlots = productsUI.GetComponentsInChildren<ProductSlot>();
        specialProductsSlots = specialProductsUI.GetComponentsInChildren<ProductSlot>();
    }

    public void ToggleShopUI()
    {
        shopUI.SetActive(!shopUI.activeSelf);
    }
}
