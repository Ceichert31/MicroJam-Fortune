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
        if (spawnerInfo.enemyType == EnemyType.BAT_ENEMY)
        {
            Gizmos.color = Color.blue;
        }
        else if (spawnerInfo.enemyType == EnemyType.FIRE_ENEMY)
        {
            Gizmos.color = Color.red;
        }
        else if (spawnerInfo.enemyType == EnemyType.MUSHROOM_ENEMY)
        {
            Gizmos.color = Color.green;
        }
        else if (spawnerInfo.enemyType == EnemyType.ROCK_ENEMY)
        {
            Gizmos.color = Color.yellow;
        }
        // arbitrary 1 value just to display where spawners are
        Gizmos.DrawWireSphere(transform.position, 1);
    }
}