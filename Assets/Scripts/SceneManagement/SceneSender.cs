using UnityEngine;

public class SceneSender : MonoBehaviour
{
    [SerializeField] private SceneEventChannel eventChannel;
    [SerializeField] private SceneEvent value;

    private SceneEvent theEvent;

    [ContextMenu("Send Scene")]

    public void SendScene()
    {
        theEvent.SceneType = value.SceneType;
        eventChannel.CallEvent(theEvent);
    }
}