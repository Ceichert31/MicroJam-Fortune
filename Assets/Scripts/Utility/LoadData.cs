using UnityEngine;

public class LoadData : MonoBehaviour
{
    private void Awake()
    {
        PlayerDataManager.Instance.Load();
    }
}
