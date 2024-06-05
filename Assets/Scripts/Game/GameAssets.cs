using UnityEngine;

public class GameAssets : MonoBehaviour
{
    #region Singleton
    public static GameAssets instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one instance of Game Assets found!");
            return;
        }

        instance = this;
    }

    #endregion

    [Header("UI")]
    public Transform damagePopupPrefab;
    public GameObject itemButtonPrefab;

    [Header("Projectile")]
    public GameObject playerProjectile;
    public GameObject enemyProjectile;

    [Header("Enemy Database")]
    public EnemyDatabase enemies;
}
