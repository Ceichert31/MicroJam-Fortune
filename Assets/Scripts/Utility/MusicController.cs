using UnityEngine;
using UnityEngine.Audio;
using System.Collections.Generic;
using System.Collections;

public class MusicController : MonoBehaviour
{
    [SerializeField]
    private AudioSource defaultSource,
        redSource,
        blueSource,
        greenSource,
        yellowSource;

    [SerializeField] private float musicTransitionTime = 3f;

    private string currentMusic = "Default";

    [SerializeField] private AudioMixer mixer;

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
            mixer.SetFloat(newMusic, 80f);

            yield return null;
        }

        //Swap weights
        //currentVolume.weight = 0;
        //newVolume.weight = 1;

        //Set new volume
        currentMusic = newMusic;

        //instance = null;
    }

}
