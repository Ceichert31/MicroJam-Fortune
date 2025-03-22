using UnityEngine;

[CreateAssetMenu(menuName = "Events/Ore Event Channel")]
public class OreEventChannel : GenericEventChannel<OreEvent> {}

[System.Serializable]
public struct OreEvent
{
    public OreStats Value;
    public OreEvent(OreStats value)
    {
        Value = value;
    }
}