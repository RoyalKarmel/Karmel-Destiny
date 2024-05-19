using UnityEngine;

[System.Serializable]
public class EnemyStats : CharacterStats
{
    [Header("Experience")]
    public int minExp = 10;
    public int maxExp = 30;

    private PlayerStats player;

    void Start()
    {
        player = PlayerManager.instance.playerStats;
    }

    public override void Die()
    {
        base.Die();

        int expGained = Random.Range(minExp, maxExp + 1);
        player.GainExperience(expGained);

        Destroy(gameObject);
    }
}
