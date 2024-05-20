using UnityEngine;

[System.Serializable]
public class EnemyDetection
{
    public float detectionRange = 7f;

    Transform enemy;
    Transform target;

    public void Initialize(Transform enemy)
    {
        this.enemy = enemy;
        target = PlayerManager.instance.player.transform;
    }

    public bool PlayerInSight()
    {
        return Vector2.Distance(enemy.position, target.position) <= detectionRange;
    }
}
