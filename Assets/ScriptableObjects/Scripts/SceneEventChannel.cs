using UnityEngine;
using UnityEngine.Events;

public enum SceneEventType
{
    Title,
    Play,
    Scene3,
    Scene4
}

[CreateAssetMenu(menuName = "Events/Scene Event Channel")]
public class SceneEventChannel : GenericEventChannel<SceneEvent> { }

[System.Serializable]
public struct SceneEvent
{
    public SceneEventType SceneType;
    public SceneEvent(SceneEventType sceneType) => SceneType = sceneType;
}