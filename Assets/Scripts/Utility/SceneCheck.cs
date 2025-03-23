using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCheck : MonoBehaviour
{
    private void Awake()
    {
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 2:
                //Load data on main game scene start
                PlayerDataManager.Instance.Load();
                break;
        }
    }
}
