using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weed", menuName = "Inventory/Weed")]
public class Weed : Item
{
    [Header("Weed")]
    public float bonusSpeed = 1;
    public float duration = 5f;

    PlayerStats playerStats;

    public override void Use()
    {
        base.Use();

        playerStats = PlayerManager.instance.playerStats;

        PlayerManager.instance.StartCoroutine(ApplySpeedModifier());
    }

    IEnumerator ApplySpeedModifier()
    {
        playerStats.speed.AddModifier(bonusSpeed);
        StatsManager.instance.SetSpeedText(playerStats.speed.GetValue());

        yield return new WaitForSeconds(duration);

        playerStats.speed.RemoveModifier(bonusSpeed);
        StatsManager.instance.SetSpeedText(playerStats.speed.GetValue());
    }
}
