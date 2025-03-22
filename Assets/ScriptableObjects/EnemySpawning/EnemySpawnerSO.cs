using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Spawner")]
public class EnemySpawnerSO : ScriptableObject
{
    public GameObject enemyPrefab;
    [Range(0, 1)]
    public float spawnChance;
}