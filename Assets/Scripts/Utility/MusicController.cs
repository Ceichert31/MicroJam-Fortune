using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;

public class MusicController : MonoBehaviour
{
    [SerializeField]
    private AudioSource defaultSource,
        redSource,
        blueSource,
        greenSource,
        yellowSource;

    [SerializeField] private AudioMixer mixer;

    /// <summary>
    /// Transitions from old volume to new
    /// </summary>
    /// <param name="newVolume"></param>
    /// <returns></returns>
    private IEnumerator SwitchMusic(string newMusic)
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
