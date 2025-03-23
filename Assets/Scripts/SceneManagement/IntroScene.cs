using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScene : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(LoadTitle());
    }

    IEnumerator LoadTitle()
    {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("TitleScreen");
    }
}
