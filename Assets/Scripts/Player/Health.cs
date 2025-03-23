using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private int currentHealth;
    [SerializeField] private int iFrameDuration;
    [SerializeField] private float spriteFlashInterval = 0.8f;
    [SerializeField] private FloatEventChannel removeHealthUI_Event;
    [SerializeField] private FloatEventChannel addHealthUI_Event;
    [SerializeField] private AudioPitcherSO damageAudio;
    [SerializeField] private SceneEventChannel deathSceneSwitch;
    private int maxHealth => GameManager.Instance.MaxHealth;

    private SpriteRenderer playerRenderer;

    private Inventory inventory;

    private bool isDamageable = true;

    private AudioSource source;

    [Header("Death Settings")]
    [SerializeField] private float deathSlowTime = 0.5f;

    private void Start()
    {
        currentHealth = maxHealth;

        inventory = GetComponent<Inventory>();

        source = GetComponent<AudioSource>();

        playerRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    public void DealDamage(int damage)
    {
        //Prevent damage
        if (!isDamageable) return;

        damageAudio.Play(source);

        //Deal damage
        currentHealth -= damage;

        //Update health UI
        //Send current health
        removeHealthUI_Event.CallEvent(new FloatEvent(damage));

        //death
        if (currentHealth <= 0) 
        {
            PlayerDeath();
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
        if (GameManager.Instance.CurrentGameState == GameManager.GameStates.Defense) 
        {
            //Slow game time
            DOTween.To(() => Time.timeScale, x => Time.timeScale = x, 0.1f, deathSlowTime).SetEase(Ease.InQuad).SetUpdate(true);

            GameManager.Instance.SetGameState(GameManager.GameStates.Death);

            deathSceneSwitch.CallEvent(new SceneEvent(SceneEventType.Title));
            //Play death animation

            return;
        }

        //Clear player inventory
        inventory.ClearInventory();
        //Teleport player to 0,0
        GameManager.Instance.ResetPlayerPosition();

        currentHealth = maxHealth;
        addHealthUI_Event.CallEvent(new (maxHealth));
    }
}
