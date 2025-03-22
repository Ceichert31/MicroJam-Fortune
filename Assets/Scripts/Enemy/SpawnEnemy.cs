using System.Runtime.CompilerServices;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private EnemySpawnerSO spawnerInfo;
    private bool spawnEnemy = true;

    public void TrySpawnEnemy()
    {
        float random = Random.value;

        if (random < spawnerInfo.spawnChance || spawnerInfo.isMushroom)
        {
            if (spawnEnemy)
            {
                Instantiate(spawnerInfo.enemyPrefab, transform.position, Quaternion.identity);

                if (spawnerInfo.isMushroom)
                {
                    spawnEnemy = false;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        // arbitrary 1 value just to display where spawners are
        Gizmos.DrawWireSphere(transform.position, 1);
    }
}