using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 5f;
    private Rigidbody2D rb;
    private Vector3 bulletPath;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        bulletPath = (GameManager.Instance.PlayerTransform.position - transform.position).normalized;
    }

    private void Update()
    {
        rb.linearVelocity = bulletPath * bulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
