using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/PlayerBaseStats")]
public class PlayerBaseStats : ScriptableObject
{
    public int maxHealth = 7; // Core Stats
    public int maxEnergy = 7;
    public int movementSpeed = 5;
    public int attackDamage = 5;


    public int vision = 3; // Side Stats
    public int carryingCapacity = 3;
    public int luck = 3;
    public int swag = 0;
    public int agility = 3;
}
