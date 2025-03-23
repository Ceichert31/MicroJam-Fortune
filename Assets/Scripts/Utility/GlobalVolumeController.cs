using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class GlobalVolumeController : MonoBehaviour
{

    [SerializeField] private float volumeTransitionTime;
    [SerializeField] private Volume defaultProfile,
        blueProfile,
        redProfile,
        greenProfile,
        yellowProfile;

    private Volume currentVolume;

    private GameManager.Biomes currentBiome => GameManager.Instance.CurrentBiome;

    private void Awake()
    {
        defaultProfile = GetComponent<Volume>();

        currentVolume = defaultProfile;
    }
    private Coroutine instance = null;
    private void Update()
    {
        if (instance != null) return;
        switch (currentBiome)
        {
            case GameManager.Biomes.DEFAULT:
                instance = StartCoroutine(SwitchVolumes(defaultProfile));
                break;

            case GameManager.Biomes.BLUE:
                instance = StartCoroutine(SwitchVolumes(blueProfile));
                break;

            case GameManager.Biomes.YELLOW:
                instance = StartCoroutine(SwitchVolumes(yellowProfile));
                break;

            case GameManager.Biomes.GREEN:
                instance = StartCoroutine(SwitchVolumes(greenProfile));
                break;

            case GameManager.Biomes.RED:
                instance = StartCoroutine(SwitchVolumes(redProfile));
                break;
        }
    }
    /// <summary>
    /// Transitions from old volume to new
    /// </summary>
    /// <param name="newVolume"></param>
    /// <returns></returns>
    private IEnumerator SwitchVolumes(Volume newVolume)
    {
        float timeElapsed = 0;
        while (timeElapsed < volumeTransitionTime)
        {
            timeElapsed += Time.deltaTime;

            currentVolume.weight -= Time.deltaTime;
            newVolume.weight += Time.deltaTime;

            yield return null;
        }

        //Swap weights
        currentVolume.weight = 0;
        newVolume.weight = 1;

        //Set new volume
        currentVolume = newVolume;

        instance = null;
    }
}
