using UnityEngine;

[System.Serializable]
public class Item
{
    public int level;
    public string itemName;
    public Sprite icon;
    public string description;
}

[System.Serializable]
public class Weapon : Item
{
    public int damage;
    public float attackRange;
    public float criticalChance;

    public Weapon(int itemLevel)
    {
        level = itemLevel;
        damage = CalculateDamage();
        attackRange = CalculateAttackRange();
        criticalChance = CalculateCriticalChance();
    }

    int CalculateDamage()
    {
        return level * 5;
    }

    float CalculateAttackRange()
    {
        return 0.5f + (level * 0.1f);
    }

    float CalculateCriticalChance()
    {
        return 0.1f + (level * 0.01f);
    }
}

[System.Serializable]
public class Armor : Item
{
    public int defense;

    public Armor(int itemLevel)
    {
        level = itemLevel;
        defense = CalculateDefense();
    }

    int CalculateDefense()
    {
        return level * 3;
    }
}

[System.Serializable]
public class Amulet : Item
{
    public int healthBonus;
    public int manaBonus;
    public int damageBonus;
    public int speedBonus;

    public Amulet(int itemLevel)
    {
        level = itemLevel;
        healthBonus = CalculateHealthBonus();
        manaBonus = CalculateManaBonus();
        damageBonus = CalculateDamageBonus();
        speedBonus = CalculateSpeedBonus();
    }

    int CalculateHealthBonus()
    {
        return level * 2;
    }
    int CalculateManaBonus()
    {
        return level * 2;
    }
    int CalculateDamageBonus()
    {
        return 1 + (level * 2);
    }
    int CalculateSpeedBonus()
    {
        return (level * 2) - 1;
    }
}

[System.Serializable]
public class ConsumableItem : Item
{
    public int healthRestore;
    public int manaRestore;
    public float durationInSeconds;
}
