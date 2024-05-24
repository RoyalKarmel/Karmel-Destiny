using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProductSlot : MonoBehaviour
{
    public Image productImage;
    public TMP_Text nameText;
    public TMP_Text priceText;

    Item item;

    public void GenerateProductSlots(Item newItem)
    {
        item = newItem;

        productImage.sprite = item.icon;
        nameText.text = item.name;
        priceText.text = item.product.price.ToString();
    }

    public void PurchaseProduct()
    {
        item.product.Purchase(item);
    }
}
