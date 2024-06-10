using UnityEngine;

public class CloneStats : MonoBehaviour
{
    public bool canClone = false;
    public float cloneInterval = 5f;
    public float cloneLifetime = 15f;

    [HideInInspector]
    public float cloneTimer = 0f;

    GameObject clonePrefab;

    void Start()
    {
        clonePrefab = GameAssets.instance.enemies.enemyClonePrefab;
    }

    public void Clone()
    {
        if (clonePrefab != null)
        {
            GameObject clone = Instantiate(clonePrefab, transform.position, Quaternion.identity);
            clone.transform.SetParent(transform);

            canClone = false;

            Destroy(clone, cloneLifetime);
        }
    }
}
