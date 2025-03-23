using System.IO;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager Instance;
    public PlayerStats playerStats;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    /// <summary>
    /// Saves player game data
    /// </summary>
    public void Save()
    {
        playerStats = GameManager.Instance.PlayerStats;
        //Paths
        string json = JsonUtility.ToJson(playerStats);
        string path = Application.persistentDataPath + "/playerData.json";
        
        //write
        System.IO.File.WriteAllText(path, json);
    }

    public void Load()
    {
        string path = Application.persistentDataPath + "/playerData.json";

        if (File.Exists(path))
        {
            string json = System.IO.File.ReadAllText(path);
            PlayerStats loadedStats = JsonUtility.FromJson<PlayerStats>(json);

            //Set stats
            GameManager.Instance.SetStats(loadedStats);
        }
    }
}