using UnityEngine;

public class DamageSource : MonoBehaviour
{
    [Header("Damage Source Settings")]
    [Tooltip("How much damage this source will deal")]
    [SerializeField] private int damageValue = 1;

    [SerializeField] private bool isPlayer = false;

    [Tooltip("Layer we want to collide with")]
    [SerializeField] private int targetLayer;

    private int attackDamage => GameManager.Instance.AttackDamage;
    private int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        damage = attackDamage;
        damage = Mathf.Clamp(damage, 0, 100);    

        //Return if not target layer
        if (collision.gameObject.layer != targetLayer) return;

        if (collision.gameObject.TryGetComponent(out IDamageable instance))
        {
            //If player use player stats
            if (isPlayer)
            {
                instance.DealDamage(damage * 2);
                return;
            }
            //Otherwise use preset damage 
            instance.DealDamage(damageValue);
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        damage = attackDamage;
        damage = Mathf.Clamp(damage, 0, 100);

        if (other.TryGetComponent(out IDamageable instance))
        {
            if (isPlayer)
            {
                instance.DealDamage(damage);
                return;
            }
            instance.DealDamage(damageValue);
        }
    }
}
