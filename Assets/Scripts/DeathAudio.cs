using UnityEngine;

public class DeathAudio : MonoBehaviour
{
    [SerializeField] private AudioPitcherSO deathAudio;
    private AudioSource source;
    [SerializeField] private float lifetime = 6f;    
    private void Start()
    {
        source = GetComponent<AudioSource>();
        deathAudio.Play(source);
        Invoke(nameof(DestroyObject), lifetime);
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
