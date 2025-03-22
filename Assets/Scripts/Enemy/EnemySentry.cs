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

    private void Awake()
    {
        animator = GetComponent<Animator>();
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

        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
