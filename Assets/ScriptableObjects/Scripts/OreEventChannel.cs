using UnityEngine;

[CreateAssetMenu(menuName = "Events/Ore Event Channel")]
public class OreEventChannel : GenericEventChannel<OreEvent> {}

[System.Serializable]
public struct OreEvent
{
    public OreStats Value;
    public int Count;
    public OreEvent(OreStats value, int count)
    {
        Value = value;
        Count = count;
    }
}