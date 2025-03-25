using UnityEngine;

public class SceneCheck : MonoBehaviour
{
    [SerializeField] private PlayerBaseStats baseStats;
    private void Start()
    {
        ///GameManager.Instance.SetStats(new(baseStats));

        //if (GameObject.Find("Data").TryGetComponent(out PlayerDataManager instance))
        //{
        //    Debug.Log("SAVED2");
        //    instance.Save();
        //}
        //PlayerDataManager.Instance.Save();
    }
}
