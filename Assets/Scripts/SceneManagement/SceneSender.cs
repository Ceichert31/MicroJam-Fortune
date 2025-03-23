using UnityEngine;

public class SceneSender : MonoBehaviour
{
    [SerializeField] private SceneEventChannel eventChannel;
    [SerializeField] private SceneEvent value;

    private SceneEvent theEvent;

    [SerializeField] private bool saveScene;

    [ContextMenu("Send Scene")]

    public void SendScene()
    {
        if (saveScene)
        {
            PlayerDataManager.Instance.Save();
        }

        theEvent.SceneType = value.SceneType;
        eventChannel.CallEvent(theEvent);
    }
}