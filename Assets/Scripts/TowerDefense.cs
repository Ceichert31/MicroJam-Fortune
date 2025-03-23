using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TowerDefense : MonoBehaviour
{
    [Header("Spawner Settings")]
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject batPrefab;
    [SerializeField] private GameObject firePrefab;
    [SerializeField] private GameObject rockPrefab;
    [SerializeField] private GameObject radiusObject;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float waveDuration = 60f;
    [SerializeField] private float spawnTimeVariation = 0.5f;

    [SerializeField] private SceneEventChannel eventChannel;
    [SerializeField] private SceneEvent value;

    [Header("UI References")]
    [SerializeField] private GameObject quotaUI;
    [SerializeField] private GameObject chargeMeterGO;
    [SerializeField] private Image chargeMeter;

    private GameManager.GameStates gameState => GameManager.Instance.CurrentGameState;

    private SceneEvent theEvent;

    public void SendScene()
    {
        theEvent.SceneType = value.SceneType;
        eventChannel.CallEvent(theEvent);
    }

    private float waveTimer = 0f;
    private float nextSpawnTime = 0f;
    private bool isSpawning = false;

    private void Update()
    {
        if (!isSpawning) return;

        waveTimer += Time.deltaTime;

        if (waveTimer >= waveDuration)
        {
            SendScene();
            StopSpawning();
            return;
        }

        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();

            float variation = Random.Range(-spawnTimeVariation, spawnTimeVariation);
            nextSpawnTime = Time.time + spawnInterval + variation;
        }

        if (gameState == GameManager.GameStates.Defense)
        {
            UpdateChargeMeter();
        }
    }

    private void UpdateChargeMeter()
    {
        quotaUI.SetActive(false);
        chargeMeterGO.SetActive(true);

        chargeMeter.fillAmount = waveTimer / waveDuration;
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

    [ContextMenu("Start Wave")]
    public void ManualStartWave()
    {
        if (!isSpawning)
        {
            StartSpawning();
            radiusObject.SetActive(true);
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