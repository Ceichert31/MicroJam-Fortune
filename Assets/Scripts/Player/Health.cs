using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private int currentHealth;
    private int maxHealth = GameManager.Instance.MaxHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void DealDamage(int damage)
    {
        //Deal damage
        currentHealth -= damage;

        //Update health UI

        if (currentHealth <= 0) 
        {
            PlayerDeath();
        }
    }

    private void PlayerDeath()
    {

    }
}
