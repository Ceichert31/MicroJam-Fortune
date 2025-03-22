using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class Drill : MonoBehaviour, IDepositable
{
    [Header("Repair Cost")]
    [SerializeField] private OreCost repairCost;

    [SerializeField] private int 
        currentSapphires,
        currentRubies,
        currentTopaz,
        currentEmerald;

    public void Deposit(OreStats oreToDeposit)
    {
        //Check ore type and add to current ore
        switch (oreToDeposit.oreType)
        {
            case OreType.Sapphire:
                currentSapphires++;

                if (currentSapphires >= repairCost.requiredSapphires)
                {
                    //Check off
                }
                break;

            case OreType.Ruby:
                currentRubies++;

                if (currentRubies >= repairCost.requiredRubies)
                {
                    //Check off
                }
                break;

            case OreType.Topaz:
                currentTopaz++;

                if (currentTopaz >= repairCost.requiredTopaz)
                {
                    //Check off
                }
                break;

            case OreType.Emerald:
                currentEmerald++;

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
    public void Deposit(OreStats oreToDeposit);
}