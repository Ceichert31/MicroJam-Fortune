using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform player;

    public static GameManager Instance;

    [Header("Current Stats")]
    [SerializeField] private PlayerStats playerStats;

    [Header("Scriptable Object Reference")]
    [SerializeField] private PlayerBaseStats baseStats;

    //Getters
    public Transform PlayerTransform { get { return player; } }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;

        //Init player stats
        playerStats = new PlayerStats(baseStats);
    }

    public int GetMaxHealth()
    {
        return playerStats.maxHealth;
    }
    public int GetMovementSpeed()
    {
        return playerStats.movementSpeed;
    }
    public int GetAttackDamage()
    {
        return playerStats.attackDamage;
    }
    public int GetMaxEnergy()
    {
        return playerStats.maxEnergy;
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