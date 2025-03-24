using UnityEngine;

public class LoadData : MonoBehaviour
{
    private void OnEnable()
    {
        PlayerDataManager.Instance.Load();
    }
}
