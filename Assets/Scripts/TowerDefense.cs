using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDefense : MonoBehaviour
{
    [Header("Spawner Settings")]
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject batPrefab;
    [SerializeField] private GameObject firePrefab;
    [SerializeField] private GameObject rockPrefab;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float waveDuration = 60f;
    [SerializeField] private float spawnTimeVariation = 0.5f;

    private float waveTimer = 0f;
    private float nextSpawnTime = 0f;
    private bool isSpawning = false;

    private void Update()
    {
        if (!isSpawning) return;

        waveTimer += Time.deltaTime;

        if (waveTimer >= waveDuration)
        {
            Debug.Log("Wave ended");
            StopSpawning();
            return;
        }

        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();

            float variation = Random.Range(-spawnTimeVariation, spawnTimeVariation);
            nextSpawnTime = Time.time + spawnInterval + variation;
        }
    }

    private void StartSpawning()
    {
        isSpawning = true;
        waveTimer = 0f;
        nextSpawnTime = Time.time;
    }

    private void StopSpawning()
    {
        isSpawning = false;
    }

    private void SpawnEnemy()
    {
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        Transform selectedSpawn = spawnPoints[spawnIndex];
        Debug.Log("Spawner: " + selectedSpawn.name);

        int enemyChoice = Random.Range(0, 3);

        switch (enemyChoice)
        {
            case 0:
                GameObject fireEnemy = Instantiate(firePrefab, selectedSpawn.position, selectedSpawn.rotation);
                fireEnemy.GetComponent<EnemyFollowAndShoot>().SetDetectionRadius(50f);
                break;
            case 1:
                GameObject batEnemy = Instantiate(batPrefab, selectedSpawn.position, selectedSpawn.rotation);
                batEnemy.GetComponent<EnemyFollow>().SetDetectionRadius(50f);
                break;
            case 2:
                GameObject rockEnemy = Instantiate(rockPrefab, selectedSpawn.position, selectedSpawn.rotation);
                rockEnemy.GetComponent<EnemyCharge>().SetDetectionRadius(50f);
                break;
        }
    }

    public void ManualStartWave()
    {
        if (!isSpawning)
        {
            StartSpawning();
        }
    }

    public void ManualStopWave()
    {
        if (isSpawning)
        {
            StopSpawning();
        }
    }
}