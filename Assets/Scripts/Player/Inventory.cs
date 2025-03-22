using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Inventory")]
    [SerializeField] private List<Ore> inventory = new();

    //Gets stat from game manager
    [SerializeField] private int maxCarryCapacity => GameManager.Instance.CarryCapacity;
    [SerializeField] private int currentCarryCapacity;

    [ContextMenu("TEST")]
    public void TEST()
    {
        AddOre(new Ore());
    }

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
        inventory.Add(ore);
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
        Ore oreInstance = inventory[inventory.Count - 1];

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
