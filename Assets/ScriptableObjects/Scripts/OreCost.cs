using UnityEngine;

[CreateAssetMenu(menuName = "Ore/Ore Cost")]
public class OreCost : ScriptableObject
{
    public int minRequiredSapphires;
    public int maxRequiredSapphires;
    public int minRequiredRubies;
    public int maxRequiredRubies;
    public int minRequiredTopaz;
    public int maxRequiredTopaz;
    public int minRequiredEmerald;
    public int maxRequiredEmerald;

    public int requiredSapphires;
    public int requiredRubies;
    public int requiredTopaz;
    public int requiredEmerald;
}
