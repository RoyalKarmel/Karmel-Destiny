using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DroppedItemUI : MonoBehaviour
{
    #region Singleton

    public static DroppedItemUI instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one instance of DroppedItemUI found!");
            return;
        }

        instance = this;
    }

    #endregion

    public float displayTime = 3f;

    [Header("UI Elements")]
    public GameObject droppedItemUI;
    public Image image;
    public TMP_Text itemName;

    Item item;

    void Start()
    {
        droppedItemUI.SetActive(false);
    }

    public void ShowDroppedItem(Item droppedItem)
    {
        item = droppedItem;

        droppedItemUI.SetActive(true);

        image.sprite = item.icon;
        itemName.text = item.name;

        // Time.timeScale = 0;
        StartCoroutine(HideDroppedItem());
    }

    IEnumerator HideDroppedItem()
    {
        yield return new WaitForSecondsRealtime(displayTime);
        // Time.timeScale = 1;

        item = null;

        image.sprite = null;
        itemName.text = null;
        droppedItemUI.SetActive(false);
    }
}
