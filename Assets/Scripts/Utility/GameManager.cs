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
    [SerializeField] private VoidEventChannel eventChannel;

    private float encumbrance;

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

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;

        //Init player stats
        playerStats = new PlayerStats(baseStats);
    }

    private VoidEvent theEvent;
    [ContextMenu("Restart Day")]
    public void RestartDay()
    {
        eventChannel.CallEvent(theEvent);
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