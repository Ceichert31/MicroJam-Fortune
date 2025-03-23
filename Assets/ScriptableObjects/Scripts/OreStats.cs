using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Ore/Ore Stats")]
public class OreStats : ScriptableObject
{
    public OreType oreType;
    public GameObject dropItem;
    public int baseValue;
    public int minValue;
    public int maxValue;
    public int durability;
    [Range(0, 10)]
    public int disableChance;

}
[System.Serializable]
public enum OreType
{
    Ruby,
    Topaz,
    Sapphire,
    Emerald,
}