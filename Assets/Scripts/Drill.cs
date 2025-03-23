using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using DG.Tweening;

public class Drill : MonoBehaviour, IDepositable
{
    [Header("Repair Cost")]
    [SerializeField] private OreCost repairCost;
    [SerializeField] private OreEventChannel updateOreUI_Event;

    [SerializeField] private int 
        currentSapphires,
        currentRubies,
        currentTopaz,
        currentEmerald;

    [Header("UI")]
    private GameObject canvas;

    private void Start()
    {
        canvas = transform.GetChild(1).gameObject;
    }

    public void Deposit(OreStats oreToDeposit)
    {
        if (oreToDeposit == null) return;

        //Check ore type and add to current ore
        switch (oreToDeposit.oreType)
        {
            case OreType.Sapphire:
                currentSapphires++;

                updateOreUI_Event.CallEvent(new(oreToDeposit, currentSapphires));

                if (currentSapphires >= repairCost.requiredSapphires)
                {
                    //Check off
                    //Set game manager bool
                }
                break;

            case OreType.Ruby:
                currentRubies++;

                updateOreUI_Event.CallEvent(new(oreToDeposit, currentRubies));

                if (currentRubies >= repairCost.requiredRubies)
                {
                    //Check off
                }
                break;

            case OreType.Topaz:
                currentTopaz++;

                updateOreUI_Event.CallEvent(new(oreToDeposit, currentTopaz));

                if (currentTopaz >= repairCost.requiredTopaz)
                {
                    //Check off
                }
                break;

            case OreType.Emerald:
                currentEmerald++;

                updateOreUI_Event.CallEvent(new(oreToDeposit, currentEmerald));

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

    //open when player near
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canvas.transform.DOScaleX(1, 0.2f).SetEase(Ease.OutQuint);
        }
    }

    //close when player not near
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canvas.transform.DOScaleX(0, 0.2f).SetEase(Ease.OutQuint);
        }
    }
}

interface IDepositable
{
    public void Deposit(OreStats oreToDeposit);
}