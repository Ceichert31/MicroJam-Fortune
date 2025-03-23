using UnityEngine;

public class DamageSource : MonoBehaviour
{
    [Header("Damage Source Settings")]
    [Tooltip("How much damage this source will deal")]
    [SerializeField] private int damageValue = 1;

    [SerializeField] private bool isPlayer = false;

    [Tooltip("Layer we want to collide with")]
    [SerializeField] private int targetLayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        //Return if not target layer
        if (collision.gameObject.layer != targetLayer) return;

        if (collision.gameObject.TryGetComponent(out IDamageable instance))
        {
            //If player use player stats
            if (isPlayer)
            {
                instance.DealDamage(GameManager.Instance.AttackDamage * 2);
                return;
            }
            //Otherwise use preset damage 
            instance.DealDamage(damageValue);
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.TryGetComponent(out IDamageable instance))
        {
            if (isPlayer)
            {
                instance.DealDamage(GameManager.Instance.AttackDamage);
                return;
            }
            instance.DealDamage(damageValue);
        }
    }
}
