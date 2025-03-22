using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private int currentHealth;
    [SerializeField] private float iFrameDuration = 0.8f;
    private int maxHealth => GameManager.Instance.MaxHealth;

    private bool hasIFrames;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void DealDamage(int damage)
    {
        //Prevent damage
        if (hasIFrames) return;

        hasIFrames = true;
        Invoke(nameof(ResetIFrames), iFrameDuration);

        //Deal damage
        currentHealth -= damage;

        //Update health UI

        if (currentHealth <= 0) 
        {
            PlayerDeath();
        }
    }

    private void ResetIFrames()
    {
        hasIFrames = false;
    }

    private void PlayerDeath()
    {

    }
}
