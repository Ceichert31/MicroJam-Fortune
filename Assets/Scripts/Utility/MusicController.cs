using UnityEngine;
using UnityEngine.Audio;
using System.Collections.Generic;
using System.Collections;

public class MusicController : MonoBehaviour
{
    private string defaultSource = "Default",
        redSource = "Red",
        blueSource = "Blue",
        greenSource = "Green",
        yellowSource = "Yellow";

    [SerializeField] private float musicTransitionTime = 3f;

    private string currentMusic = "Default";

    [SerializeField] private AudioMixer mixer;

    private GameManager.Biomes currentBiome => GameManager.Instance.CurrentBiome;

    private Coroutine instance = null;
    private void Update()
    {
        if (instance != null) return;
        switch (currentBiome)
        {
            case GameManager.Biomes.DEFAULT:
                //ResetVolumes();
                instance = StartCoroutine(SwitchMusic(defaultSource));
                break;

            case GameManager.Biomes.BLUE:
                //ResetVolumes();
                instance = StartCoroutine(SwitchMusic(blueSource));
                break;

            case GameManager.Biomes.YELLOW:
                //ResetVolumes();
                instance = StartCoroutine(SwitchMusic(yellowSource));
                break;

            case GameManager.Biomes.GREEN:
                //ResetVolumes();
                instance = StartCoroutine(SwitchMusic(greenSource));
                break;

            case GameManager.Biomes.RED:
                //();
                instance = StartCoroutine(SwitchMusic(redSource));
                break;
        }
    }

    /// <summary>
    /// Transitions from old volume to new
    /// </summary>
    /// <param name="newVolume"></param>
    /// <returns></returns>
    private IEnumerator SwitchMusic(string newMusic)
    {
        float timeElapsed = 0;
        while (timeElapsed < musicTransitionTime)
        {
            timeElapsed += Time.deltaTime;

            mixer.SetFloat(currentMusic, -80f);
            //newVolume.weight += Time.deltaTime;
            mixer.SetFloat(newMusic, 0f);

            yield return null;
        }

        //Swap weights
        //currentVolume.weight = 0;
        //newVolume.weight = 1;

        //Set new volume
        currentMusic = newMusic;

        instance = null;
    }

    /// <summary>
    /// Starts battle theme
    /// </summary>
    /// <param name="ctx"></param>
    public void PlayBattleTheme(VoidEvent ctx)
    {

    }
}
