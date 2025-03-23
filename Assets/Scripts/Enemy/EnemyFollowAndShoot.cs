using System.Collections;
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
    private Transform enemySpr;
    private Transform bulletSpawn;

    private bool delay = false;

    private Transform playerTransform => GameManager.Instance.PlayerTransform;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        bulletSpawn = transform.GetChild(0);
        enemySpr = transform.GetChild(1);
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

    private void Shoot()
    {
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

        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void SetDetectionRadius(float radius)
    {
        chaseRadius = radius;
    }
}