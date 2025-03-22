using UnityEngine;

[CreateAssetMenu(menuName = "Ore")]
public class Ore : ScriptableObject
{
    public OreType oreType;
    public int baseValue;
    public int minValue;
    public int maxValue;
}

public enum OreType
{
    Ruby,
    Topaz,
    Sapphire,
    Emerald,
}