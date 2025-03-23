using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyFollow : MonoBehaviour, IDamageable
{
    [SerializeField] private float chaseRadius = 5f;
    [SerializeField] private float speed = 2f;
    [SerializeField] private int enemyHealth = 3;
    private Animator animator;
    private SpriteRenderer enemyRenderer;

    private Transform PlayerTransform => GameManager.Instance.PlayerTransform;

    private Rigidbody2D rb;
    [SerializeField] private float stunTime = 1f;
    [SerializeField] private float knockbackForce = 10f;

    [SerializeField] private AudioPitcherSO damageAudio;
    [SerializeField] private AudioPitcherSO flyAudio;
    [SerializeField] private GameObject deathAudioObject;

    [SerializeField] private float flyingAudioDelay = 1f;
    private float flyingTimer;

    private AudioSource source;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        enemyRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();

        source = GetComponent<AudioSource>();

        flyingTimer = flyingAudioDelay;
    }
    private void Update()
    {
        if (PlayerTransform != null && Vector3.Distance(transform.position, PlayerTransform.position) <= chaseRadius)
        {
            transform.position = Vector3.MoveTowards(transform.position, PlayerTransform.position, speed * Time.deltaTime);
        }

        flyingTimer += Time.deltaTime;
        if (flyingTimer >= flyingAudioDelay)
        {
            flyingTimer = 0;
            flyAudio.Play(source);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
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


    void IDamageable.DealDamage(int damage)
    {
        enemyHealth -= damage;

        damageAudio.Play(source);

        Vector2 dir = (GameManager.Instance.PlayerTransform.position - transform.position).normalized;

        KnockBack(-dir * knockbackForce);

        if (enemyHealth <= 0)
        {
            Instantiate(deathAudioObject, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }

    public void SetDetectionRadius(float radius)
    {
        chaseRadius = radius;
    }
}