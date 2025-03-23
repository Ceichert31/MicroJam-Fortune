using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySentry : MonoBehaviour, IDamageable
{
    [SerializeField] private float fireRadius = 5f;
    [SerializeField] private int enemyHealth = 3;
    [SerializeField] private float attackDelayTime = 3f;

    private GameObject particleObject;
    private Animator animator;
    private ParticleSystem particleSys;
    private Transform playerTransform => GameManager.Instance.PlayerTransform;

    private Vector3 enemyToPlayer;
    private bool attackDelay = false;

    [SerializeField] private AudioPitcherSO damageAudio;
    [SerializeField] private AudioPitcherSO fireAudio;
    [SerializeField] private GameObject deathAudioObject;

    private AudioSource source;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
        particleObject = transform.GetChild(0).gameObject;
        particleSys = particleObject.transform.GetChild(0).GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        enemyToPlayer = transform.position - playerTransform.transform.position;
        //particleObject.transform.up = enemyToPlayer;

        if (playerTransform != null && Vector3.Distance(transform.position, playerTransform.position) <= fireRadius && !attackDelay)
        {
            particleObject.transform.up = enemyToPlayer;
            animator.SetTrigger("MushroomAttack");
            StartCoroutine(IAttackDelay());
        }
    }

    private void PlayParticle()
    {
        fireAudio.Play(source);
        particleSys.Play();
    }

    private IEnumerator IAttackDelay()
    {
        attackDelay = true;
        yield return new WaitForSeconds(attackDelayTime);
        attackDelay = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fireRadius);
    }

    void IDamageable.DealDamage(int damage)
    {
        enemyHealth -= damage;

        damageAudio.Play(source);

        if (enemyHealth <= 0)
        {
            Instantiate(deathAudioObject, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
