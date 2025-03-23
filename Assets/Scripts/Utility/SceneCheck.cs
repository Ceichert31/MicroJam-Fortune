using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCheck : MonoBehaviour
{
    [SerializeField] private PlayerBaseStats baseStats;
    private void Awake()
    {
        GameManager.Instance.SetStats(new(baseStats));
        PlayerDataManager.Instance.Save();
    }
}
