using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "LevelData/EnemyType")]
public class EnemyType : ScriptableObject
{
    public List<GameObject> enemy_prefabs;

    public List<GameObject> getEnemyPrefabs()
    {
        return enemy_prefabs;
    }
}
