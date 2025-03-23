using UnityEngine;


public class HandlePause : MonoBehaviour
{
    private CanvasGroup pauseMenu;

    private void Awake()
    {
        pauseMenu = transform.GetChild(0).GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        if (GameManager.Instance.IsPaused)
        {
            Time.timeScale = 0;
            pauseMenu.alpha = 0.9f;
            pauseMenu.interactable = true;
            pauseMenu.blocksRaycasts = true;
        }
        else
        {
            Time.timeScale = 1;
            pauseMenu.alpha = 0;
            pauseMenu.interactable = false;
            pauseMenu.blocksRaycasts = false;
        }
    }
}
