using System.Runtime.CompilerServices;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private EnemySpawnerSO spawnerInfo;

    public void TrySpawnEnemy()
    {
        float random = Random.value;

        if (random < spawnerInfo.spawnChance)
        {
            Instantiate(spawnerInfo.enemyPrefab, transform.position, Quaternion.identity);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        // arbitrary 1 value just to display where spawners are
        Gizmos.DrawWireSphere(transform.position, 1);
    }
}