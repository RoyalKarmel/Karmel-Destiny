using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProductSlot : MonoBehaviour
{
    public Image productImage;

    [Header("Price elements")]
    public GameObject priceObject;
    public TMP_Text nameText;
    public TMP_Text priceText;

    [Header("Sold out")]
    public GameObject soldOut;

    Item item;

    public void GenerateProductSlots(Item newItem)
    {
        item = newItem;

        productImage.sprite = item.icon;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (!item.product.isPurchased)
        {
            priceObject.SetActive(true);

            nameText.text = item.name;
            priceText.text = item.product.price.ToString();
        }
        else
        {
            priceObject.SetActive(false);
            soldOut.SetActive(true);
        }
    }

    public void PurchaseProduct()
    {
        if (item != null)
        {
            item.product.Purchase(item);
            UpdateUI();
        }
    }
}
