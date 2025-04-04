using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyFollowAndShoot : MonoBehaviour, IDamageable
{
    [SerializeField] private float chaseRadius = 5f;
    [SerializeField] private float shootRadius = 3f;
    [SerializeField] private float speed = 2f;
    [SerializeField] private int enemyHealth = 3;
    [Header("Bullet")]
    [SerializeField] private GameObject EnemyBullet;
    [SerializeField] private float shootDelayTime = 2.5f;

    private Animator animator;
    private SpriteRenderer enemyRenderer;
    private Transform enemySpr;
    private Transform bulletSpawn;

    private bool delay = false;

    private Transform playerTransform => GameManager.Instance.PlayerTransform;

    private Rigidbody2D rb;
    [SerializeField] private float stunTime = 1f;
    [SerializeField] private float knockbackForce = 10f;
    [SerializeField] private AudioPitcherSO damageAudio;
    [SerializeField] private AudioPitcherSO fireAudio;
    [SerializeField] private GameObject deathAudioObject;

    private AudioSource source;

    [Header("Sprite Flash")]
    [SerializeField] private int iFrameDuration = 10;
    [SerializeField] private float spriteFlashInterval = 0.05f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        bulletSpawn = transform.GetChild(0);
        enemySpr = transform.GetChild(1);
        enemyRenderer = enemySpr.GetComponent<SpriteRenderer>();

        source = GetComponent<AudioSource>();

        rb = GetComponent<Rigidbody2D>();
    }

    private Vector3 enemyToPlayer;
    private void Update()
    {
        enemyToPlayer = transform.position - playerTransform.position;

        if (enemyToPlayer.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }


        if (playerTransform != null && Vector3.Distance(transform.position, playerTransform.position) <= chaseRadius && Vector3.Distance(transform.position, playerTransform.position) > shootRadius)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
        }

        if (Vector3.Distance(transform.position, playerTransform.position) <= shootRadius && !delay)
        {
            // add delya and shot
            animator.SetTrigger("FireAttack");
            StartCoroutine(IShootDelay());
        }


    }

    public void KnockBack(Vector2 knockbackDir)
    {
        rb.AddForce(knockbackDir, ForceMode2D.Impulse);
        Invoke(nameof(ResetCanMove), stunTime);
    }

    private void ResetCanMove()
    {
        rb.linearVelocity = Vector2.zero;
    }

    private void Shoot()
    {
        fireAudio.Play(source);
        Instantiate(EnemyBullet, transform.position, Quaternion.identity);
    }

    private IEnumerator IShootDelay()
    {
        delay = true;

        yield return new WaitForSeconds(shootDelayTime);

        delay = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, shootRadius);
    }

    void IDamageable.DealDamage(int damage)
    {
        enemyHealth -= damage;

        damageAudio.Play(source);

        Vector2 dir = (GameManager.Instance.PlayerTransform.position - transform.position).normalized;

        KnockBack(-dir * knockbackForce);
        StartCoroutine(DamageFlash());

        if (enemyHealth <= 0)
        {
            //deathAudio.Play(source);
            Instantiate(deathAudioObject, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }

    public void SetDetectionRadius(float radius)
    {
        chaseRadius = radius;
    }

    private IEnumerator DamageFlash()
    {
        float flashNumber = 0;
        while (flashNumber < iFrameDuration)
        {
            enemyRenderer.enabled = false;
            yield return new WaitForSeconds(spriteFlashInterval);
            enemyRenderer.enabled = true;
            flashNumber++;
            yield return new WaitForSeconds(spriteFlashInterval);
        }
    }
}