using UnityEngine;

[System.Serializable]
public class EnemyPatrol
{
    public float patrolRadius = 10f; // Radius in which the enemy will generate patrol points
    public int numberOfPatrolPoints = 5; // Number of patrol points to generate

    private Transform enemy;

    public void Initialize(Transform enemy)
    {
        this.enemy = enemy;
    }

    public Transform[] GeneratePatrolPoints()
    {
        Transform[] patrolPoints = new Transform[numberOfPatrolPoints];

        for (int i = 0; i < numberOfPatrolPoints; i++)
        {
            GameObject patrolPoint = new GameObject("PatrolPoint" + i);
            patrolPoint.transform.position = GetRandomPointWithinRadius();
            patrolPoint.transform.parent = enemy;
            patrolPoints[i] = patrolPoint.transform;
        }

        return patrolPoints;
    }

    private Vector3 GetRandomPointWithinRadius()
    {
        Vector2 randomPoint = Random.insideUnitCircle * patrolRadius;
        return new Vector3(
            enemy.position.x + randomPoint.x,
            enemy.position.y + randomPoint.y,
            enemy.position.z
        );
    }
}
