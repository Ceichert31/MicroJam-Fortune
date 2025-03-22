using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Inventory")]
    [SerializeField] private List<OreStats> inventory = new();

    //Gets stat from game manager
    [SerializeField] private int maxCarryCapacity => GameManager.Instance.CarryCapacity;
    [SerializeField] private int currentCarryCapacity;

    /// <summary>
    /// Adds ore to the inventory
    /// </summary>
    /// <param name="ore"></param>
    public void AddOre(OreEvent ctx)
    {
        //Increase carry capacity
        currentCarryCapacity++;

        //Increase encumberance if above carry capacity
        if (currentCarryCapacity >= maxCarryCapacity)
        {
            GameManager.Instance.Encumbrance++;
        }

        //Add ore to stack
        inventory.Add(ctx.Value);
    }

    /// <summary>
    /// Drops an ore on the group
    /// </summary>
    public void DropOre()
    {
        //Decrease current carry capacity
        currentCarryCapacity--;

        //If dropping ore sets us below max carry capacity, reduce encumbrance
        if (currentCarryCapacity <= maxCarryCapacity) 
        {
            GameManager.Instance.Encumbrance--;
        }

        //Remove ore from stack
        OreStats oreInstance = inventory[inventory.Count - 1];

        //Instaniate ore
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
