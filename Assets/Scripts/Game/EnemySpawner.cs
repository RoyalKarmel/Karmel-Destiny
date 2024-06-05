using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn variables")]
    public float spawnInterval = 1f;
    public int maxEnemies = 10;
    private int currentEnemyCount = 0;
    private float elapsedTime = 0f;

    void Update()
    {
        if (currentEnemyCount >= maxEnemies)
            return;

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
        var enemyPrefab = GetRandomEnemyPrefab();

        if (enemyPrefab != null)
            Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }

    GameObject GetRandomEnemyPrefab()
    {
        var enemies = GameAssets.instance.enemyDatabase.enemies;
        if (enemies == null || enemies.Count == 0)
            return null;

        int randomIndex = Random.Range(0, enemies.Count);
        return enemies[randomIndex].gameObject;
    }
}
