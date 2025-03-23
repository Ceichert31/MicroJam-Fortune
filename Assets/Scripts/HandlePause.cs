using UnityEngine;
using UnityEngine.SceneManagement;


public class HandlePause : MonoBehaviour
{
    private CanvasGroup pauseMenu;
    private CanvasGroup tutorialMenu;

    private bool isTutorial = false;

    private void Awake()
    {
        pauseMenu = transform.GetChild(0).GetComponent<CanvasGroup>();
        if (SceneManager.GetActiveScene().name == "TitleScreen")
            tutorialMenu = GameObject.Find("Canvas").transform.GetChild(3).GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "EnemyScene")
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

        if (SceneManager.GetActiveScene().name == "TitleScreen")
        {
            if (isTutorial)
            {
                tutorialMenu.alpha = 0.95f;
                tutorialMenu.interactable = true;
                tutorialMenu.blocksRaycasts = true;
            }
            else
            {
                tutorialMenu.alpha = 0f;
                tutorialMenu.interactable = false;
                tutorialMenu.blocksRaycasts = false;
            }
        }
    }

    public void TutorialButton()
    {
        isTutorial = !isTutorial;
    }
}
