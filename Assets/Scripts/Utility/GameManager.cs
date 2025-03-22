using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform player;

    public static GameManager Instance;

    [SerializeField] private PlayerStats playerStats;

    //Getters
    public Transform PlayerTransform { get { return player; } }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;

        //Init player stats
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
    public int maxHealth;
    public int movementSpeed;
    public int attackDamage;
    public int maxEnergy;

    public PlayerStats(int health, int speed, int damage, int energy)
    {
        maxHealth = health;
        movementSpeed = speed;
        attackDamage = damage;
        maxEnergy = energy;
    }
}