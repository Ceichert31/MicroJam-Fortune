using System.Runtime.CompilerServices;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private EnemySpawnerSO spawnerInfo;

    public void TrySpawnEnemy()
    {
        if (Random.value < spawnerInfo.spawnChance)
        {
            Instantiate(spawnerInfo.enemyPrefab, transform.position, Quaternion.identity);
        }
    }
}