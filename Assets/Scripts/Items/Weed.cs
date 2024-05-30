using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weed", menuName = "Inventory/Weed")]
public class Weed : Item
{
    [Header("Weed")]
    public int bonusSpeed = 1;
    public float duration = 5f;

    public override void Use()
    {
        base.Use();

        PlayerManager.instance.StartCoroutine(ApplySpeedModifier());
    }

    IEnumerator ApplySpeedModifier()
    {
        PlayerManager.instance.playerStats.speed.AddModifier(bonusSpeed);
        yield return new WaitForSeconds(duration);
        PlayerManager.instance.playerStats.speed.RemoveModifier(bonusSpeed);
    }
}
