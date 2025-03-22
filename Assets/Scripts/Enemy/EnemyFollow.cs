using UnityEngine;

public class EnemyFollow : MonoBehaviour, IDamageable
{
    [SerializeField] private float chaseRadius = 5f;
    [SerializeField] private float speed = 2f;
    [SerializeField] private int enemyHealth = 3;

    private Transform playerTransform => GameManager.Instance.PlayerTransform;

    private void Awake()
    {
        // empty
    }

    private void Update()
    {
        if (playerTransform != null && Vector3.Distance(transform.position, playerTransform.position) <= chaseRadius)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
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