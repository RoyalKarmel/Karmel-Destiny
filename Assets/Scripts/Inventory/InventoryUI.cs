using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    #region Singleton

    public static InventoryUI instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one instance of InventoryUI found!");
            return;
        }

        instance = this;
    }

    #endregion

    public Transform itemsParent;
    Inventory inventory;
    InventorySlot[] slots;

    [HideInInspector]
    public List<Item> itemsInUI = new List<Item>();

    [SerializeField]
    private GameObject inventoryAndStatsUI;

    // Start is called before the first frame update
    void Start()
    {
        inventoryAndStatsUI.SetActive(false);

        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        inventory.space = slots.Length;
    }

    // Update is called once per frame
    void Update()
    {
        // Open / Close Inventory & Stats page
        if (Input.GetButtonDown("Inventory"))
            inventoryAndStatsUI.SetActive(!inventoryAndStatsUI.activeSelf);
    }

    void UpdateUI()
    {
        foreach (var item in inventory.items)
        {
            if (!itemsInUI.Contains(item))
            {
                for (int i = 0; i < slots.Length; i++)
                {
                    if (slots[i].transform.childCount == 0)
                    {
                        slots[i].AddItem(item);
                        itemsInUI.Add(item);
                        break; // Exit the loop once the item is added
                    }
                }
            }
        }
    }
}
