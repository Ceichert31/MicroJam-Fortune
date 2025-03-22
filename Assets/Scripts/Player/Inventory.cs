using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Inventory")]
    [SerializeField] private Stack<Ore> inventory = new();

    //Gets stat from game manager
    private int maxCarryCapacity => GameManager.Instance.CarryCapacity;
    private int currentCarryCapacity;

    /// <summary>
    /// Adds ore to the inventory
    /// </summary>
    /// <param name="ore"></param>
    public void AddOre(Ore ore)
    {
        //Increase encumberance if above carry capacity
        if (currentCarryCapacity >= maxCarryCapacity)
        {
            GameManager.Instance.Encumbrance++;
        }

        //Add ore to stack
        inventory.Push(ore);
    }

    /// <summary>
    /// Drops an ore on the group
    /// </summary>
    public void DropOre()
    {
        //If dropping ore sets us below max carry capacity, reduce encumbrance
        if (currentCarryCapacity <= maxCarryCapacity) 
        {
            GameManager.Instance.Encumbrance--;
        }

        //Remove ore from stack
        Ore oreInstance = inventory.Pop();

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
