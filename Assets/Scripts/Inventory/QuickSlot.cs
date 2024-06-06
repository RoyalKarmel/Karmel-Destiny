using TMPro;
using UnityEngine;

public class QuickSlot : MonoBehaviour
{
    public string quickSlotInput;
    public GameObject itemSlot;
    public TMP_Text quickSlotInputText;

    ItemButton itemButton;
    KeyCode keyCodeInput;

    void Start()
    {
        quickSlotInputText.text = quickSlotInput;

        GetKeyCodeFromInput();
    }

    void Update()
    {
        // Add / Remove item to/from quick slot
        if (itemSlot.transform.childCount > 0)
            itemButton = itemSlot.GetComponentInChildren<ItemButton>();
        else
            itemButton = null;

        // Use Quick Slot Item
        if (Input.GetKeyDown(keyCodeInput))
            UseQuickSlot();
    }

    void UseQuickSlot()
    {
        if (itemButton != null)
            itemButton.UseItem();
    }

    void GetKeyCodeFromInput()
    {
        switch (quickSlotInput)
        {
            case "1":
                keyCodeInput = KeyCode.Alpha1;
                break;
            case "2":
                keyCodeInput = KeyCode.Alpha2;
                break;
            case "3":
                keyCodeInput = KeyCode.Alpha3;
                break;

            default:
                Debug.LogError("Invalid quick slot input: " + quickSlotInput);
                break;
        }
    }
}
