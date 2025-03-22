using UnityEngine;

public class Drill : MonoBehaviour, IDepositable
{
    [Header("Repair Cost")]
    [SerializeField] private OreCost repairCost;

    private int 
        currentSapphires,
        currentRubies,
        currentTopaz,
        currentEmerald;

    public void Deposit(OreStats ore, int count)
    {
        //Check if all ores have been deposited
        switch (ore.oreType) 
        {
            case OreType.Sapphire:
                currentSapphires += count;

                if (currentSapphires >= repairCost.requiredSapphires)
                {
                    //Check off
                }
                break;

            case OreType.Ruby:
                currentRubies += count;

                if (currentRubies >= repairCost.requiredRubies)
                {
                    //Check off
                }
                break;

            case OreType.Topaz:
                currentTopaz += count;

                if (currentTopaz >= repairCost.requiredTopaz)
                {
                    //Check off
                }
                break;

            case OreType.Emerald:
                currentEmerald += count;

                if (currentEmerald >= repairCost.requiredEmerald)
                {
                    //Check off
                }
                break;
        }

        //Update UI with checkmark if ore is complete

        //If true start defence
        //procceed once player has survived 30 seconds
    }
}

interface IDepositable
{
    public void Deposit(OreStats ore, int count);
}