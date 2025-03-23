using UnityEngine;

public class MineAnimatorMethods : MonoBehaviour
{
    [SerializeField] private float lifetime = 10f;
    private float timer;

    [SerializeField] private AudioPitcherSO explosionAudio;
    private AudioSource source;

    [SerializeField] private ParticleSystem explosion;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > lifetime)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Called by animator
    /// </summary>
    public void BlowUP()
    {
        explosionAudio.Play(source);
        explosion.Play();
        Destroy(transform.GetChild(0).gameObject);
    }
}
