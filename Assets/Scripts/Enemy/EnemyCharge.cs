using UnityEngine;
using System.Collections;

public class EnemyCharge : MonoBehaviour, IDamageable
{
    [Header("Enemy Settings")]
    [SerializeField] private int enemyHealth = 5;
    [Header("Charge Settings")]
    [SerializeField] private float detectionRadius = 7f;
    [SerializeField] private float chargeCooldown = 3f;
    [SerializeField] private float chargeSpeed = 6f;
    [SerializeField] private float drawBackDistance = 1.5f;
    [SerializeField] private float drawBackDuration = 0.5f;
    [SerializeField] private LayerMask obstacleLayer;
    [Header("Wall Collision Settings")]
    [SerializeField] private float postChargeBackupDistance = 1.5f;
    [SerializeField] private float postChargeBackupDuration = 0.3f;
    [Header("Sprite Flash")]
    [SerializeField] private int iFrameDuration = 10;
    [SerializeField] private float spriteFlashInterval = 0.05f;

    private Animator animator;
    private SpriteRenderer enemyRenderer;
    private BoxCollider2D boxCollider;
    private Rigidbody2D rb;

    private bool canHitWall = false;
    private bool isCharging = false;
    private bool isDrawingBack = false;
    private bool canCharge = true;
    private Vector3 chargeDirection;
    private Coroutine drawbackCoroutine;

    private Transform playerTransform => GameManager.Instance.PlayerTransform;

    [SerializeField] private float stunTime = 1f;
    [SerializeField] private float knockbackForce = 10f;
    [SerializeField] private AudioPitcherSO damageAudio;
    [SerializeField] private GameObject deathAudioObject;

    private AudioSource source;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        enemyRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        source = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!isCharging && !isDrawingBack && canCharge)
        {
            if (Vector3.Distance(transform.position, playerTransform.position) <= detectionRadius)
            {
                StartCoroutine(ChargeSequence());
            }
        }

        if (isCharging)
        {
            transform.position += chargeDirection * chargeSpeed * Time.deltaTime;

            StartCoroutine(ReenableCollisionAfterDelay(0.25f));

            StartCoroutine(CanHitWall(0.3f));

            RaycastHit2D hit = Physics2D.Raycast(transform.position, chargeDirection, 0.5f, obstacleLayer);

            if (hit.collider != null && canHitWall)
            {
                Vector3 wallPosition = transform.position;
                StopCharging();
                StartCoroutine(BackupFromWall(wallPosition));

                canHitWall = false;
            }
        }
        else
        {
            boxCollider.enabled = true;
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

    private IEnumerator ReenableCollisionAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        Debug.Log("added back");
        int defaultLayer = LayerMask.NameToLayer("Default");
        int myObjectLayer = gameObject.layer;
        Physics2D.IgnoreLayerCollision(myObjectLayer, defaultLayer, false);
    }
    private IEnumerator CanHitWall(float delay)
    {
        yield return new WaitForSeconds(delay);

        canHitWall = true;
    }

    private IEnumerator ChargeSequence()
    {
        canCharge = false;

        if (enemyRenderer != null && !isCharging)
        {
            Vector3 direction = playerTransform.position - transform.position;
            enemyRenderer.flipX = direction.x < 0;
        }

        chargeDirection = (playerTransform.position - transform.position).normalized;

        isDrawingBack = true;

        Debug.Log("ignored");
        int defaultLayer = LayerMask.NameToLayer("Default");
        int myObjectLayer = gameObject.layer;
        Physics2D.IgnoreLayerCollision(myObjectLayer, defaultLayer);

        Vector3 drawBackPosition = transform.position - (chargeDirection * drawBackDistance);

        float elapsed = 0;
        Vector3 startPos = transform.position;

        animator.SetTrigger("RockAttack");

        while (elapsed < drawBackDuration)
        {
            transform.position = Vector3.Lerp(startPos, drawBackPosition, elapsed / drawBackDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        isDrawingBack = false;

        yield return new WaitForSeconds(0.2f);

        isCharging = true;

        yield return new WaitForSeconds(2f);

        if (isCharging)
        {
            StopCharging();
        }
    }

    private void StopCharging()
    {
        isCharging = false;
        StartCoroutine(StartCooldown());
    }

    private IEnumerator StartCooldown()
    {
        yield return new WaitForSeconds(chargeCooldown);
        canCharge = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
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
            Instantiate(deathAudioObject, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isCharging) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.DealDamage(1);
            }

            StopCharging();
        }
        else if (collision.gameObject.CompareTag("Wall") || collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            Vector3 wallPosition = transform.position;
            StopCharging();
            StartCoroutine(BackupFromWall(wallPosition));
        }
    }

    private IEnumerator BackupFromWall(Vector3 wallHitPosition)
    {
        Vector3 backupDirection = -chargeDirection;
        Vector3 backupPosition = wallHitPosition + (backupDirection * postChargeBackupDistance);

        float elapsed = 0;
        Vector3 backupStart = transform.position;

        while (elapsed < postChargeBackupDuration)
        {
            transform.position = Vector3.Lerp(backupStart, backupPosition, elapsed / postChargeBackupDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
    }

    public void SetDetectionRadius(float radius)
    {
        detectionRadius = radius;
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