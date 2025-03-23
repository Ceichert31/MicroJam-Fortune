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

    private Animator animator;
    private SpriteRenderer enemyRenderer;
    private BoxCollider2D boxCollider;
    private Rigidbody2D rb;

    private bool isCharging = false;
    private bool isDrawingBack = false;
    private bool canCharge = true;
    private Vector3 chargeDirection;
    private Coroutine drawbackCoroutine;

    private Transform playerTransform => GameManager.Instance.PlayerTransform;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        enemyRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
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

            RaycastHit2D hit = Physics2D.Raycast(transform.position, chargeDirection, 0.5f, obstacleLayer);

            if (hit.collider != null)
            {
                Vector3 wallPosition = transform.position;
                StopCharging();
                StartCoroutine(BackupFromWall(wallPosition));
            }
        }
        else
        {
            boxCollider.enabled = true;
        }
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

        if (enemyHealth <= 0)
        {
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
}