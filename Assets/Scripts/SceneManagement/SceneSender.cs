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
        //if (saveScene)
        //{
        //    if (GameObject.Find("Data").TryGetComponent(out PlayerDataManager instance))
        //    {
        //        Debug.Log("SAVED1");
        //        instance.Save();
        //    }
        //}

        theEvent.SceneType = value.SceneType;
        eventChannel.CallEvent(theEvent);
    }
}