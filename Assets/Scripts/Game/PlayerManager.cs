using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Singleton
    public static PlayerManager instance;
    public Dictionary<EquipmentSlot, SpriteRenderer> equipmentSlotToRenderer;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one instance of Player Manager found!");
            return;
        }

        instance = this;

        // Initialize the dictionary
        equipmentSlotToRenderer = new Dictionary<EquipmentSlot, SpriteRenderer>
        {
            { EquipmentSlot.Armor, playerArmor },
            { EquipmentSlot.Weapon, playerWeapon }
        };
    }

    #endregion

    [Header("Player Object")]
    public GameObject player;

    [Header("Player Equipment")]
    public SpriteRenderer playerArmor;
    public SpriteRenderer playerWeapon;

    [Header("Player Stats")]
    public PlayerStats playerStats;
}
