using UnityEngine;

public class LoadData : MonoBehaviour
{
    private void OnEnable()
    {
        //if (GameObject.Find("Data").TryGetComponent(out PlayerDataManager instance))
        //{
        //    Debug.Log("LOADED!");
        //    instance.Load();
        //}

        GameManager.Instance.Load();
    }
}
