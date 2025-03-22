using UnityEngine;

public class MineableRock : MonoBehaviour, IMineable
{
    [SerializeField] private OreStats oreStats;
    [SerializeField] private int health;

    private void Start()
    {
        //Set health
        health = oreStats.durability;
    }

    /// <summary>
    /// Damages the ore
    /// </summary>
    /// <param name="damage"></param>
    public void DealDamage(int damage)
    {
        health -= damage;

        //Destroy
        if (health <= 0)
        {
            //Drop ore 
            //oreStats.dropItem;

            //Disable
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Resets object and checks to see if it should be disabled
    /// </summary>
    /// <param name="ctx"></param>
    public void ResetObject(VoidEvent ctx)
    {
        //Reset health
        health = oreStats.durability;

        //Reenable object
        gameObject.SetActive(true);

        //Check if object should be disabled
        if (ShouldDisable())
        {
            gameObject.SetActive(false);
        }
    }

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
