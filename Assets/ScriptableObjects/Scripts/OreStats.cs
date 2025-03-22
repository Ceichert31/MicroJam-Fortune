using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Ore")]
public class OreStats : ScriptableObject
{
    public OreType oreType;
    public GameObject dropItem;
    public int baseValue;
    public int minValue;
    public int maxValue;
    public int durability;
    public int disableChance;

}

public enum OreType
{
    Ruby,
    Topaz,
    Sapphire,
    Emerald,
}