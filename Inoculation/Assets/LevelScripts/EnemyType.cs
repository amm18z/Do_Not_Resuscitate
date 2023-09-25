using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "LevelData/EnemyType")]
public class EnemyType : ScriptableObject
{
    // Stores all the enemy prefabs made for this enemy type
    // Each prefab should contain an enemy script which stores the information about its speed, health, strength
    public List<GameObject> enemy_prefabs;

    public List<GameObject> getEnemyPrefabs()
    {
        return enemy_prefabs;
    }
}
