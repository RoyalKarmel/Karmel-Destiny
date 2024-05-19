using UnityEngine;

[System.Serializable]
public class EnemyStats : CharacterStats
{
    [Header("Experience")]
    public int minExp = 10;
    public int maxExp = 30;

    [Header("Move variables")]
    public float moveDuration = 5f;
    public float randomMoveRange = 10f;
    public float changeDirectionInterval = 2f;

    [Header("Ranges")]
    public float returnRange = 15f;
    public float attackRange = 1.5f;
    public float detectionRange = 5f;

    private PlayerStats player;

    void Start()
    {
        player = FindObjectOfType<PlayerStats>();
    }

    public override void Die()
    {
        base.Die();

        int expGained = Random.Range(minExp, maxExp + 1);
        player.GainExperience(expGained);

        Destroy(gameObject);
    }
}
