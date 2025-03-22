using UnityEngine;

public class MineableRock : MonoBehaviour, IDamageable
{
    [SerializeField] private OreEventChannel addOre_Event;
    [SerializeField] private OreStats oreStats;
    [SerializeField] private int currentHealth;

    private GameObject childObject;

    private BoxCollider2D boxCol;

    private Animator animator;

    private void Start()
    {
        //Set health
        currentHealth = oreStats.durability;

        childObject = transform.GetChild(0).gameObject;

        boxCol = GetComponent<BoxCollider2D>();

        animator = GetComponent<Animator>();
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

        //Destroy
        if (currentHealth <= 0)
        {
            //Drop ore 
            //oreStats.dropItem;

            //Add ore
            addOre_Event.CallEvent(new OreEvent(oreStats));

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
