using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform player;

    public static GameManager Instance;

    private bool isPaused = false;

    //[Header("Current Stats")]
    //[SerializeField] private PlayerStats playerStats;

    [Header("Scriptable Object Reference")]
    [SerializeField] private PlayerBaseStats baseStats;

    [Header("Stats References")]
    [SerializeField] private int savedHealth;
    [SerializeField] private int savedMovementSpeed;
    [SerializeField] private int savedAttackDamage;
    [SerializeField] private int savedCarryingCapacity;
    [SerializeField] private int savedVision;
    [SerializeField] private int savedConfidence;
    [SerializeField] private int savedLuck;
    [SerializeField] private int savedSwag;
    [SerializeField] private int savedAgility;

    [Header("Event Channel")]
    [SerializeField] private VoidEventChannel oreRespawn_Event;
    [SerializeField] private VoidEventChannel gameTickUpdate_Event;
    [SerializeField] private VoidEventChannel defenseStarted_Event;
    [SerializeField] private int maxTotalOres;
    [SerializeField] private int currentTotalOres;

    private VoidEvent gameTickEvent;

    private float encumbrance;

    [Header("Biome Settings")]
    [SerializeField] private float defaultBiomeRadius = 15f;
    private Biomes currentBiome = Biomes.DEFAULT;

    [Header("Game Tick Settings")]
    [SerializeField] private float gameTick = 5f;

    private float gameTickTimer;

    private GameStates currentGameState = GameStates.Exploration;
    private bool reachedSapphireQuota;
    private bool reachedRubyQuota;
    private bool reachedEmeraldQuota;
    private bool reachedTopazQuota;

    private bool firstLoad = true;

    public bool ReachedSapphireQuota { get { return reachedSapphireQuota; } }
    public bool ReachedRubyQuota { get { return reachedRubyQuota; } }
    public bool ReachedEmeraldQuota { get { return reachedEmeraldQuota; } }
    public bool ReachedTopazQuota { get { return reachedTopazQuota; } }

    public enum CoreStats
    {
        Health,
        MovementSpeed,
        AttackDamage,
        CarryingCapacity
    }

    public enum SideStats
    {
        Vision,
        Confidence,
        Luck,
        Swag,
        Agility
    }

    public enum GameStates
    {
        Paused,
        Exploration,
        Defense,
        Death,
    }

    public enum Biomes
    {
        YELLOW,
        RED,
        GREEN,
        BLUE,
        DEFAULT
    }

    //Getters
    public Transform PlayerTransform { get { return player; } }
    //Core stat getters
    public int MaxHealth { get { return savedHealth; } }
    public int MovementSpeed { get { return savedMovementSpeed; } }
    public float Encumbrance { get { return encumbrance; } set { encumbrance = value; } }
    public int AttackDamage { get { return savedAttackDamage; } }
    public int CarryingCapacity { get { return savedCarryingCapacity; } }
    //Side stat getters
    public int Vision { get { return savedVision; } }
    public int Confidence { get { return savedConfidence; } }
    public int Luck { get { return savedLuck; } }
    public int Swag { get { return savedSwag; } }
    public int Agility { get { return savedAgility; } }
    //public PlayerStats PlayerStats { get { return playerStats; } }
    public GameStates CurrentGameState { get { return currentGameState; } }
    public Biomes CurrentBiome { get { return currentBiome; } }
    // getter for pause state
    public bool IsPaused { get { return isPaused; } }
    // setter just toggles pause state
    public void SetPauseState()
    {
        isPaused = !isPaused;
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;

        //Init player stats
        //playerStats = new PlayerStats(baseStats);

        SetPlayerPrefStats();

        gameTickTimer = gameTick;
    }

    /*
    /// <summary>
    /// Sets stats
    /// </summary>
    /// <param name="stats"></param>
    public void SetStats(PlayerStats stats)
    {
        playerStats = stats;
    }
    */

    public void SetPlayerPrefStats()
    {
        if (firstLoad && SceneManager.GetActiveScene().name == "UITestScene")
        {
            savedHealth = baseStats.maxHealth;
            savedMovementSpeed = baseStats.movementSpeed;
            savedAttackDamage = baseStats.attackDamage;
            savedCarryingCapacity = baseStats.carryingCapacity;
            savedVision = baseStats.vision;
            savedConfidence = baseStats.confidence;
            savedLuck = baseStats.luck;
            savedSwag = baseStats.swag;
            savedAgility = baseStats.agility;
            firstLoad = false;
        }
        else
        {
            savedHealth = PlayerPrefs.GetInt("Health");
            savedMovementSpeed = PlayerPrefs.GetInt("Speed");
            savedAttackDamage = PlayerPrefs.GetInt("Damage");
            savedCarryingCapacity = PlayerPrefs.GetInt("Capacity");
            savedVision = PlayerPrefs.GetInt("Vision");
            savedConfidence = PlayerPrefs.GetInt("Confidence");
            savedLuck = PlayerPrefs.GetInt("Luck");
            savedSwag = PlayerPrefs.GetInt("Swag");
            savedAgility = PlayerPrefs.GetInt("Agility");
        }
    }

    private void Update()
    {
        //Game tick logic
        gameTickTimer -= Time.deltaTime;
        if (gameTickTimer <= 0)
        {
            gameTickTimer = gameTick;
            //Call event channel
            gameTickUpdate_Event.CallEvent(gameTickEvent);
        }

        if (Vector3.Distance(Vector3.zero, PlayerTransform.position) <= defaultBiomeRadius)
        {
            currentBiome = Biomes.DEFAULT;
        }
        else if (PlayerTransform.position.x >= 0 && PlayerTransform.position.y >= 0)
        {
            currentBiome = Biomes.YELLOW;
        }
        else if (PlayerTransform.position.x < 0 && PlayerTransform.position.y >= 0)
        {
            currentBiome = Biomes.GREEN;
        }
        else if (PlayerTransform.position.x < 0 && PlayerTransform.position.y < 0)
        {
            currentBiome = Biomes.RED;
        }
        else if (PlayerTransform.position.x >= 0 && PlayerTransform.position.y < 0)
        {
            currentBiome = Biomes.BLUE;
        }

        if (currentTotalOres < 20)
        {
            RestartDay();
            currentTotalOres += 5;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(Vector3.zero, defaultBiomeRadius);
    }

    private VoidEvent theEvent;
    [ContextMenu("Restart Day")]
    public void RestartDay()
    {
        oreRespawn_Event.CallEvent(theEvent);
    }
    public void ResetPlayerPosition()
    {
        PlayerTransform.position = Vector3.zero;
    }
    public void SetGameState(GameStates state)
    {
        currentGameState = state;
    }
    // Updating player stats
    public void UpdateCoreStat(CoreStats coreStats, int value)
    {
        switch (coreStats)
        {
            case CoreStats.Health:
                savedHealth += value;
                PlayerPrefs.SetInt("Health", savedHealth);
                break;
            case CoreStats.MovementSpeed:
                savedMovementSpeed += value;
                PlayerPrefs.SetInt("Speed", savedMovementSpeed);
                break;
            case CoreStats.AttackDamage:
                savedAttackDamage += value;
                PlayerPrefs.SetInt("Damage", savedAttackDamage);
                break;
            case CoreStats.CarryingCapacity:
                savedCarryingCapacity += value;
                PlayerPrefs.SetInt("Capacity", savedCarryingCapacity);
                break;
        }
    }

    public void UpdateSideStat(SideStats sideStats, int value)
    {
        switch (sideStats)
        {
            case SideStats.Vision:
                savedVision += value;
                PlayerPrefs.SetInt("Vision", savedVision);
                break;
            case SideStats.Confidence:
                savedConfidence += value;
                PlayerPrefs.SetInt("Confidence", savedConfidence);
                break;
            case SideStats.Luck:
                savedLuck += value;
                PlayerPrefs.SetInt("Luck", savedLuck);
                break;
            case SideStats.Swag:
                savedSwag += value;
                PlayerPrefs.SetInt("Swag", savedSwag);
                break;
            case SideStats.Agility:
                savedAgility += value;
                PlayerPrefs.SetInt("Agility", savedAgility);
                break;
        }
    }

    /// <summary>
    /// Sets a quota as reached
    /// </summary>
    /// <param name="type"></param>
    public void QuotaReached(OreType type)
    {
        switch (type)
        {
            case OreType.Sapphire:
                reachedSapphireQuota = true;
                break;

            case OreType.Ruby:
                reachedRubyQuota = true;
                break;

            case OreType.Topaz:
                reachedTopazQuota = true;
                break;

            case OreType.Emerald:
                reachedEmeraldQuota = true;
                break;
        }

        //If all quotas are reached, change game state
        if (ReachedSapphireQuota && ReachedRubyQuota && reachedEmeraldQuota && ReachedTopazQuota)
        {
            currentGameState = GameStates.Defense;
            //Call defense started event
            defenseStarted_Event.CallEvent(theEvent);
        }
    }

    public void RemoveOre()
    {
        currentTotalOres--;
    }
}

/*
[System.Serializable]
public struct PlayerStats
{
    //Core stats
    public int maxHealth;
    public int movementSpeed;
    public int attackDamage;
    public int carryingCapacity;

    //Side stats
    public int vision;
    public int confidence;
    public int luck;
    public int swag;
    public int agility;

    public PlayerStats(PlayerBaseStats baseStats)
    {
        maxHealth = baseStats.maxHealth;
        movementSpeed = baseStats.movementSpeed;
        attackDamage = baseStats.attackDamage;
        carryingCapacity = baseStats.carryingCapacity;

        vision = baseStats.vision;
        confidence = baseStats.confidence;
        luck = baseStats.luck;
        swag = baseStats.swag;
        agility = baseStats.agility;
    }
}
*/