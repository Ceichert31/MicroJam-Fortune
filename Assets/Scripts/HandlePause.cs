using UnityEngine;
using UnityEngine.SceneManagement;


public class HandlePause : MonoBehaviour
{
    private GameObject pauseMenu;
    private CanvasGroup tutorialMenu;

    [SerializeField] private const string SCENE_NAME_1 = "TitleScreen";

    private bool isTutorial = false;

    private void Awake()
    {
        pauseMenu = transform.GetChild(0).gameObject;
        if (SceneManager.GetActiveScene().name == "TitleScreen")
            tutorialMenu = GameObject.Find("Canvas").transform.GetChild(4).GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "EnemyScene")
        {
            if (GameManager.Instance.IsPaused)
            {
                Time.timeScale = 0;
                pauseMenu.SetActive(true);
                if (Input.GetKeyDown(KeyCode.P))
                {
                    QuitToStart();
                }
            }
            else
            {
                Time.timeScale = 1;
                pauseMenu.SetActive(false);
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

    public void QuitToStart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SCENE_NAME_1);
    }
}
