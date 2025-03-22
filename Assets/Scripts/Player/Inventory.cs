using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Scriptable Object Reference")]
    [SerializeField] private FloatEventChannel setCapacityText_Event;

    [Header("Inventory")]
    [SerializeField] private List<OreStats> inventory = new();

    //Gets stat from game manager
    [SerializeField] private int maxCarryCapacity => GameManager.Instance.CarryCapacity;
    [SerializeField] private int currentCarryCapacity;

    private FloatEvent capacityEvent;

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

        //Remove ore from stack
        inventory.RemoveAt(inventory.Count - 1);

        //Update capacity text
        capacityEvent.FloatValue = currentCarryCapacity;
        setCapacityText_Event.CallEvent(capacityEvent);
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
}
