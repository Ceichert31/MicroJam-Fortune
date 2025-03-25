using System.IO;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager Instance;
    //public PlayerStats playerStats;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Saves player game data
    /// </summary>
    public void Save()
    {
        //playerStats = GameManager.Instance.PlayerStats;
        ////Paths
        //string json = JsonUtility.ToJson(playerStats);
        //string path = Application.persistentDataPath + "/playerData.json";

        ////write
        //System.IO.File.WriteAllText(path, json);

        //playerStats = GameManager.Instance.PlayerStats;
        //PlayerPrefs.SetInt("Health", playerStats.maxHealth);
        //PlayerPrefs.SetInt("Speed", playerStats.movementSpeed);
        //PlayerPrefs.SetInt("Damage", playerStats.attackDamage);
        //PlayerPrefs.SetInt("Capacity", playerStats.carryingCapacity);

        //PlayerPrefs.SetInt("Vision", playerStats.vision);
        //PlayerPrefs.SetInt("Confidence", playerStats.confidence);
        //PlayerPrefs.SetInt("Luck", playerStats.luck);
        //PlayerPrefs.SetInt("Swag", playerStats.swag);
        //PlayerPrefs.SetInt("Agility", playerStats.agility);

        //PlayerPrefs.Save();
    }

    public void Load()
    {
        //string path = Application.persistentDataPath + "/playerData.json";

        //if (File.Exists(path))
        //{
        //    string json = System.IO.File.ReadAllText(path);
        //    PlayerStats loadedStats = JsonUtility.FromJson<PlayerStats>(json);

        //    //Set stats
        //    GameManager.Instance.SetStats(loadedStats);
        //}

        //PlayerStats stats;

        //stats.maxHealth = PlayerPrefs.GetInt("Health");
        //stats.movementSpeed = PlayerPrefs.GetInt("Speed");
        //stats.attackDamage = PlayerPrefs.GetInt("Damage");
        //stats.carryingCapacity = PlayerPrefs.GetInt("Capacity");

        //stats.vision = PlayerPrefs.GetInt("Vision");
        //stats.confidence = PlayerPrefs.GetInt("Confidence");
        //stats.luck = PlayerPrefs.GetInt("Luck");
        //stats.swag = PlayerPrefs.GetInt("Swag");
        //stats.agility = PlayerPrefs.GetInt("Agility");

       // GameManager.Instance.SetStats(playerStats);
    }

    private void OnEnable()
    {
        Debug.Log("PLAYER DATA ENABLED");
    }
    private void OnDisable()
    {
        Debug.Log("PLAYER DATA DISABLED");
    }
}