using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCheck : MonoBehaviour
{
    [SerializeField] private PlayerBaseStats baseStats;
    private void Awake()
    {
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            //Stats
            case 1:
                GameManager.Instance.SetStats(new (baseStats));
                PlayerDataManager.Instance.Save();
                break;

                //Game
            case 2:
                //Load data on main game scene start
                PlayerDataManager.Instance.Load();
                break;
        }
    }
}
