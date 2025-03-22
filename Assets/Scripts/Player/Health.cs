using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private int currentHealth;
    [SerializeField] private int iFrameDuration;
    [SerializeField] private float spriteFlashInterval = 0.8f;
    [SerializeField] private FloatEventChannel removeHealthUI_Event;
    [SerializeField] private FloatEventChannel addHealthUI_Event;
    private int maxHealth => GameManager.Instance.MaxHealth;

    private SpriteRenderer playerRenderer;

    private bool isDamageable = true;

    private void Start()
    {
        currentHealth = maxHealth;

        playerRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    public void DealDamage(int damage)
    {
        //Prevent damage
        if (!isDamageable) return;

        //Deal damage
        currentHealth -= damage;

        //Update health UI
        //Send current health
        removeHealthUI_Event.CallEvent(new FloatEvent(damage));

        //death
        if (currentHealth <= 0) 
        {
            PlayerDeath();
            Time.timeScale = 0;
        }

        StartCoroutine(StartIFrames());
    }
    IEnumerator StartIFrames()
    {
        //disable player damage here
        isDamageable = false;

        float flashNumber = 0;
        while (flashNumber < iFrameDuration)
        {
            playerRenderer.enabled = false;
            yield return new WaitForSeconds(spriteFlashInterval);
            playerRenderer.enabled = true;
            flashNumber++;
            yield return new WaitForSeconds(spriteFlashInterval);
        }

        //Reenable player damage here
        isDamageable = true;
    }

    private void PlayerDeath()
    {
        
    }
}
