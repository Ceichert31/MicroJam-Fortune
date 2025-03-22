using UnityEngine;

public class MineableRock : MonoBehaviour, IMineable
{
    [SerializeField] private OreStats oreStats;
    [SerializeField] private int currentHealth;

    private GameObject childObject;

    private void Start()
    {
        //Set health
        currentHealth = oreStats.durability;

        childObject = transform.GetChild(0).gameObject;
    }

    /// <summary>
    /// Damages the ore
    /// </summary>
    /// <param name="damage"></param>
    public void DealDamage(int damage)
    {
        //Deal damage
        currentHealth -= damage;

        //Destroy
        if (currentHealth <= 0)
        {
            //Drop ore 
            //oreStats.dropItem;

            //Disable
            childObject.SetActive(false);
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
        childObject.SetActive(true);

        //Check if object should be disabled
        if (ShouldDisable())
        {
            childObject.SetActive(false);
        }
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

interface IMineable 
{
    void DealDamage(int damage);
}
