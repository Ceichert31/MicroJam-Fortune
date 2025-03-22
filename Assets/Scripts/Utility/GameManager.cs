using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform player;

    public static GameManager Instance;

    [Header("Current Stats")]
    [SerializeField] private PlayerStats playerStats;

    [Header("Scriptable Object Reference")]
    [SerializeField] private PlayerBaseStats baseStats;

    [Header("Event Channel")]
    [SerializeField] private VoidEventChannel oreRespawn_Event;
    [SerializeField] private VoidEventChannel gameTickUpdate_Event;

    private VoidEvent gameTickEvent;

    private float encumbrance;

    [Header("Game Tick Settings")]
    [SerializeField] private float gameTick = 5f;

    private float gameTickTimer;

    private GameStates currentGameState = GameStates.Exploration;
    private bool reachedSapphireQuota;
    private bool reachedRubyQuota;
    private bool reachedEmeraldQuota;
    private bool reachedTopazQuota;

    public bool ReachedSapphireQuota { get { return reachedSapphireQuota; } }
    public bool ReachedRubyQuota { get { return reachedRubyQuota; } }
    public bool ReachedEmeraldQuota { get { return reachedEmeraldQuota; } }
    public bool ReachedTopazQuota { get { return reachedTopazQuota; } }

    private enum CoreStats
    {
        Health,
        MovementSpeed,
        AttackDamage,
        Energy
    }

    private enum SideStats
    {
        Vision,
        CarryingCapacity,
        Luck,
        Swag,
        Agility
    }

    public enum GameStates
    {
        Paused,
        Exploration,
        Defense,
    }

    //Getters
    public Transform PlayerTransform { get { return player; } }
    //Core stat getters
    public int MaxHealth { get { return playerStats.maxHealth; } }
    public int MovementSpeed { get { return playerStats.movementSpeed; } }
    public float Encumbrance { get { return encumbrance; } set { encumbrance = value; } }
    public int AttackDamage { get { return playerStats.attackDamage; } }
    public int MaxEnergy { get { return playerStats.maxEnergy; } }
    //Side stat getters
    public int Vision { get { return playerStats.vision; } }
    public int CarryCapacity { get { return playerStats.carryingCapacity; } }
    public int Luck { get { return playerStats.luck; } }
    public int Swag { get { return playerStats.swag; } }
    public int Agility { get { return playerStats.agility; } }
    public GameStates CurrentGameState { get { return currentGameState; } }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;

        //Init player stats
        playerStats = new PlayerStats(baseStats);

        gameTickTimer = gameTick;
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
    }

    private VoidEvent theEvent;
    [ContextMenu("Restart Day")]
    public void RestartDay()
    {
        oreRespawn_Event.CallEvent(theEvent);
    }
    public void SetGameState(GameStates state)
    {
        currentGameState = state;
    }
    private void UpdateCoreStat(CoreStats coreStats, int value)
    {
        switch (coreStats)
        {
            case CoreStats.Health:
                playerStats.maxHealth += value;
                break;
            case CoreStats.MovementSpeed:
                playerStats.movementSpeed += value;
                break;
            case CoreStats.AttackDamage:
                playerStats.attackDamage += value;
                break;
            case CoreStats.Energy:
                playerStats.maxEnergy += value;
                break;
        }
    }

    private void UpdateSideStat(SideStats sideStats, int value)
    {
        switch (sideStats)
        {
            case SideStats.Vision:
                playerStats.vision += value;
                break;
            case SideStats.CarryingCapacity:
                playerStats.carryingCapacity += value;
                break;
            case SideStats.Luck:
                playerStats.luck += value;
                break;
            case SideStats.Swag:
                playerStats.swag += value;
                break;
            case SideStats.Agility:
                playerStats.agility += value;
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
        }
    }
}
[System.Serializable]
public struct PlayerStats
{
    //Core stats
    public int maxHealth;
    public int movementSpeed;
    public int attackDamage;
    public int maxEnergy;

    //Side stats
    public int vision;
    public int carryingCapacity;
    public int luck;
    public int swag;
    public int agility;

    public PlayerStats(PlayerBaseStats baseStats)
    {
        maxHealth = baseStats.maxHealth;
        movementSpeed = baseStats.movementSpeed;
        attackDamage = baseStats.attackDamage;
        maxEnergy = baseStats.maxEnergy;

        vision = baseStats.vision;
        carryingCapacity = baseStats.carryingCapacity;
        luck = baseStats.luck;
        swag = baseStats.swag;
        agility = baseStats.agility;
    }
}