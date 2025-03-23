using UnityEngine;

public class MineableRock : MonoBehaviour, IDamageable
{
    [SerializeField] private OreEventChannel addOre_Event;
    [SerializeField] private OreStats oreStats;
    [SerializeField] private int currentHealth;
    [SerializeField] AudioPitcherSO damageSound;
    [SerializeField] AudioPitcherSO breakSound;


    private GameObject childObject;

    private BoxCollider2D boxCol;

    private Animator animator;

    private OreEvent oreEvent;

    private AudioSource source;
    private void Start()
    {
        //Set health
        currentHealth = oreStats.durability;

        childObject = transform.GetChild(0).gameObject;

        boxCol = GetComponent<BoxCollider2D>();

        animator = GetComponent<Animator>();

        source = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Damages the ore
    /// </summary>
    /// <param name="damage"></param>
    public void DealDamage(int damage)
    {
        //Deal damage
        currentHealth--;
        animator.SetTrigger("OreHit");

        damageSound.Play(source);

        //Destroy
        if (currentHealth <= 0)
        {
            //Drop ore 
            //oreStats.dropItem;

            breakSound.Play(source);

            //Add ore
            oreEvent.Value = oreStats;
            oreEvent.Count = Random.Range(oreStats.minValue, oreStats.maxValue);
            addOre_Event.CallEvent(oreEvent);

            float luck = GameManager.Instance.Luck * 0.1f;
            if (Random.Range(0, 1) > luck)
            {
                Debug.Log(luck);
            }

            //Disable
            SetObjectStatus(false);
        }
    }

    /// <summary>
    /// Resets object and checks to see if it should be disabled
    /// </summary>
    /// <param name="ctx"></param>
    public void ResetObject(VoidEvent ctx)
    {
        //Reset health
        currentHealth = oreStats.durability;

        //Reenable object
        SetObjectStatus(true);
   

        //Check if object should be disabled
        if (ShouldDisable())
        {
            SetObjectStatus(false);
        }
    }

    private void SetObjectStatus(bool isEnabled)
    {
        childObject.SetActive(isEnabled);
        boxCol.enabled = isEnabled;
    }

    /// <summary>
    /// Returns whether the current ore should be disabled
    /// </summary>
    /// <returns></returns>
    private bool ShouldDisable()
    {
        int chance = Random.Range(0, 10);

        return chance <= oreStats.disableChance;
    }
}

interface IDamageable 
{
    void DealDamage(int damage);
}
