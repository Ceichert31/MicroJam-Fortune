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
}
