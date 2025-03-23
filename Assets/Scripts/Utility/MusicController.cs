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

    private float currentVolume = 0;
    private float newVolume = -80;

    [SerializeField] private AudioMixer mixer;
    [SerializeField] private AudioClip battleClip;
    [SerializeField] private AudioSource mainSource;

    private GameManager.Biomes currentBiome => GameManager.Instance.CurrentBiome;

    private Coroutine instance = null;

    private const float MIN_VOLUME = -80f;
    private const float MAX_VOLUME = -8f;
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
                if (GameManager.Instance.CurrentGameState == GameManager.GameStates.Defense) return;

                instance = StartCoroutine(SwitchMusic(blueSource));
                break;

            case GameManager.Biomes.YELLOW:
                if (GameManager.Instance.CurrentGameState == GameManager.GameStates.Defense) return;

                instance = StartCoroutine(SwitchMusic(yellowSource));
                break;

            case GameManager.Biomes.GREEN:
                if (GameManager.Instance.CurrentGameState == GameManager.GameStates.Defense) return;

                instance = StartCoroutine(SwitchMusic(greenSource));
                break;

            case GameManager.Biomes.RED:
                if (GameManager.Instance.CurrentGameState == GameManager.GameStates.Defense) return;

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

            currentVolume = Mathf.Lerp(currentVolume, MIN_VOLUME, timeElapsed / musicTransitionTime);
            newVolume = Mathf.Lerp(newVolume, MAX_VOLUME, timeElapsed / musicTransitionTime);

            mixer.SetFloat(currentMusic, currentVolume);
            mixer.SetFloat(newMusic, newVolume);

            yield return null;
        }

        mixer.SetFloat(currentMusic, MIN_VOLUME);
        mixer.SetFloat(newMusic, MAX_VOLUME);

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
        //Set all 
        mixer.SetFloat(defaultSource, MIN_VOLUME);
        mixer.SetFloat(redSource, MIN_VOLUME);
        mixer.SetFloat(blueSource, MIN_VOLUME);
        mixer.SetFloat(greenSource, MIN_VOLUME);
        mixer.SetFloat(yellowSource, MIN_VOLUME);

        //Switch clips
        mainSource.clip = battleClip;
        mainSource.Play();
    }
}
