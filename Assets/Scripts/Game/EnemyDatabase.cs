using System.Collections.Generic;

[System.Serializable]
public class EnemyDatabase
{
    public List<CharacterStats> enemies;

    public CharacterStats GetEnemyByName(string name)
    {
        return enemies.Find(enemy => enemy.name == name);
    }
}
