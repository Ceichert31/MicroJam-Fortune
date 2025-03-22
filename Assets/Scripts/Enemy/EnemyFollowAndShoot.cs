using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyFollowAndShoot : MonoBehaviour, IDamageable
{
    [SerializeField] private float chaseRadius = 5f;
    [SerializeField] private float shootRadius = 3f;
    [SerializeField] private float speed = 2f;
    [SerializeField] private int enemyHealth = 3;
    private Animator animator;
    private SpriteRenderer enemyRenderer;

    private Transform playerTransform => GameManager.Instance.PlayerTransform;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        enemyRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    private Vector3 enemyToPlayer;
    private void Update()
    {
        enemyToPlayer = transform.position - playerTransform.position;
        if (playerTransform != null && Vector3.Distance(transform.position, playerTransform.position) <= chaseRadius && Vector3.Distance(transform.position, playerTransform.position) > shootRadius)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
        }
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
}