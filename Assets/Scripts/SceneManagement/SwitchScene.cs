using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class SwitchScene : MonoBehaviour
{
    [SerializeField] private const string SCENE_NAME_1 = "TitleScreen";
    [SerializeField] private const string SCENE_NAME_2 = "EnemyScene";
    [SerializeField] private const string SCENE_NAME_3 = "WinScreen";
    [SerializeField] private const string SCENE_NAME_4 = "Scene4";
    [SerializeField] private Animator transition;
    [SerializeField] private float transitionTime = 1f;

    public void LoadScene(SceneEvent ctx)
    {
        switch (ctx.SceneType)
        {
            case SceneEventType.Title:
                StartCoroutine(LoadLevel(SCENE_NAME_1));
                break;
            case SceneEventType.Play:
                StartCoroutine(LoadLevel(SCENE_NAME_2));
                break;
            case SceneEventType.Scene3:
                StartCoroutine(LoadLevel(SCENE_NAME_3));
                break;
            case SceneEventType.Scene4:
                StartCoroutine(LoadLevel(SCENE_NAME_4));
                break;
        }
    }

    // Load a scene given the name of a scene
    private IEnumerator LoadLevel(string SCENE_NAME)
    {
        // Play scene transition animation
        transition.SetTrigger("Fade");

        // Wait 'transitionTime' seconds before loading the scene
        yield return new WaitForSeconds(transitionTime);

        // Load scene
        SceneManager.LoadScene(SCENE_NAME);
    }
}