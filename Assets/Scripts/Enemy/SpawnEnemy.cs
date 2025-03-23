using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private EnemySpawnerSO spawnerInfo;

    private int enemiesSpawned = 0;
    private int activeEnemies = 0;
    private bool spawnEnemy = true;

    [SerializeField] private int maxActiveEnemies = 3;

    private List<GameObject> spawnedEnemies = new List<GameObject>();

    [SerializeField] private float checkInterval = 0.5f;

    public int EnemiesSpawned => enemiesSpawned;
    public int ActiveEnemies => activeEnemies;

    private void Start()
    {
        StartCoroutine(CheckDestroyedEnemies());
    }

    public void TrySpawnEnemy()
    {
        if (activeEnemies >= maxActiveEnemies)
        {
            return;
        }

        float random = Random.value;
        if (random < spawnerInfo.spawnChance || spawnerInfo.isMushroom)
        {
            if (spawnEnemy)
            {
                GameObject obj = Instantiate(spawnerInfo.enemyPrefab, transform.position, Quaternion.identity);

                spawnedEnemies.Add(obj);

                enemiesSpawned++;
                activeEnemies++;

                if (spawnerInfo.isMushroom)
                {
                    spawnEnemy = false;
                }
            }
        }
    }

    private IEnumerator CheckDestroyedEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(checkInterval);

            for (int i = spawnedEnemies.Count - 1; i >= 0; i--)
            {
                if (spawnedEnemies[i] == null)
                {
                    spawnedEnemies.RemoveAt(i);
                    activeEnemies--;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (spawnerInfo != null)
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

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}