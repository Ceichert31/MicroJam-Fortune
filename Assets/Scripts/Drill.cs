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
                    GameManager.Instance.QuotaReached(OreType.Sapphire);
                }
                break;

            case OreType.Ruby:
                currentRubies++;

                updateOreUI_Event.CallEvent(new(oreToDeposit, currentRubies));

                if (currentRubies >= repairCost.requiredRubies)
                {
                    //Check off
                    GameManager.Instance.QuotaReached(OreType.Ruby);
                }
                break;

            case OreType.Topaz:
                currentTopaz++;

                updateOreUI_Event.CallEvent(new(oreToDeposit, currentTopaz));

                if (currentTopaz >= repairCost.requiredTopaz)
                {
                    //Check off
                    GameManager.Instance.QuotaReached(OreType.Topaz);
                }
                break;

            case OreType.Emerald:
                currentEmerald++;

                updateOreUI_Event.CallEvent(new(oreToDeposit, currentEmerald));

                if (currentEmerald >= repairCost.requiredEmerald)
                {
                    //Check off
                    GameManager.Instance.QuotaReached(OreType.Emerald);
                }
                break;
        }
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