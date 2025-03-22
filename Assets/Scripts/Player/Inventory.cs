using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Scriptable Object Reference")]
    [SerializeField] private FloatEventChannel setCapacityText_Event;
    [SerializeField] private OreEventChannel updateOreUI_Event;

    [Header("Inventory")]
    [SerializeField] private List<OreStats> inventory = new();

    //Gets stat from game manager
    [SerializeField] private int maxCarryCapacity => GameManager.Instance.CarryCapacity;
    [SerializeField] private int currentCarryCapacity;

    private FloatEvent capacityEvent;
    private OreEvent oreEvent;

    /// <summary>
    /// Adds ore to the inventory
    /// </summary>
    /// <param name="ore"></param>
    public void AddOre(OreEvent ctx)
    {
        //Increase carry capacity
        currentCarryCapacity++;

        //Increase encumberance if above carry capacity
        if (currentCarryCapacity > maxCarryCapacity)
        {
            GameManager.Instance.Encumbrance++;
        }

        //Add ore to stack
        inventory.Add(ctx.Value);

        //Update capacity text
        capacityEvent.FloatValue = currentCarryCapacity;
        setCapacityText_Event.CallEvent(capacityEvent);

        //Update ore UI
        ctx.Count = GetOreTypeCount(ctx.Value.oreType);
        updateOreUI_Event.CallEvent(ctx);
    }

    /// <summary>
    /// Drops an ore on the group
    /// </summary>
    public void DropNewestItem()
    {
        //Guard Clause
        if (inventory.Count <= 0) return;
        
        //Decrease current carry capacity
        currentCarryCapacity--;

        //If dropping ore sets us below max carry capacity, reduce encumbrance
        if (GameManager.Instance.Encumbrance > 0) 
        {
            GameManager.Instance.Encumbrance--;
        }

        //Cache latest ore
        oreEvent.Value = inventory[inventory.Count - 1];

        //Remove ore from stack
        inventory.RemoveAt(inventory.Count - 1);

        //Update capacity text
        capacityEvent.FloatValue = currentCarryCapacity;
        setCapacityText_Event.CallEvent(capacityEvent);

        //Update ore UI
        oreEvent.Count = GetOreTypeCount(oreEvent.Value.oreType);
        updateOreUI_Event.CallEvent(oreEvent);
    }
    /// <summary>
    /// Returns how many of a type of ore are in the players inventory
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    private int GetOreTypeCount(OreType type)
    {
        int count = 0;
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].oreType == type)
                count++;
        }
        return count;
    }

    /// <summary>
    /// Clears inventory
    /// </summary>
    public void RemoveOre()
    {
        //Calculate value
        //Add money
        inventory.Clear();
    }

    [ContextMenu("Remove")]
    /// <summary>
    /// Removes over capacity ore
    /// </summary>
    public void RemoveOverflow()
    {
        //Remove everything after max carry capacity
        for (int i = maxCarryCapacity - 1; i < inventory.Count; i++) 
        {
            //Remove ore
            inventory.RemoveAt(i);

            //Reduce capacity
            currentCarryCapacity--;

            //Reduce encumbrance
            if (GameManager.Instance.Encumbrance > 0)
            {
                GameManager.Instance.Encumbrance--;
            }
        }
        //Update capacity text
        capacityEvent.FloatValue = currentCarryCapacity;
        setCapacityText_Event.CallEvent(capacityEvent);
    }
}
