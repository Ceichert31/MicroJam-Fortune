using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public enum EnemyType
{
    BAT_ENEMY,
    MUSHROOM_ENEMY,
    FIRE_ENEMY,
    ROCK_ENEMY
}

[CreateAssetMenu(menuName = "Enemy Spawner")]
public class EnemySpawnerSO : ScriptableObject
{
    public GameObject enemyPrefab;
    [Range(0, 1)]
    public float spawnChance;
    public bool isMushroom = true;
    public EnemyType enemyType;
}