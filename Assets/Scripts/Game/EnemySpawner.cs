using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    [Header("Spawn variables")]
    public float spawnInterval = 1f;
    public int maxEnemies = 10;
    private int currentEnemyCount = 0;
    private float elapsedTime = 0f;

    void Update()
    {
        if (currentEnemyCount >= maxEnemies) return;

        elapsedTime += Time.deltaTime;

        if (elapsedTime >= spawnInterval)
        {
            elapsedTime = 0f;

            SpawnEnemy();

            currentEnemyCount++;
        }
    }

    // Spawn enemies
    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }
}
