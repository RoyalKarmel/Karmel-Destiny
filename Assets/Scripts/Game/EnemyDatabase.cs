using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyDatabase
{
    public List<CharacterStats> enemies;
    public GameObject enemyClonePrefab;

    public CharacterStats GetEnemyByName(string name)
    {
        return enemies.Find(enemy => enemy.name == name);
    }
}
