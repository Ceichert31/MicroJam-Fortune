using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Scriptable Object Reference")]
    [SerializeField] private FloatEventChannel setCapacityText_Event;
    [SerializeField] private OreEventChannel updateOreUI_Event;
    [SerializeField] private VoidEventChannel resetOreUI_Event;

    [Header("Inventory")]
    [SerializeField] private List<OreStats> inventory = new();

    //Gets stat from game manager
    [SerializeField] private int maxCarryCapacity => GameManager.Instance.Confidence;
    [SerializeField] private int currentCarryCapacity;

    private FloatEvent capacityEvent;
    private OreEvent oreEvent;

    //Getters
    public int CurrentCarryCapcity { get { return currentCarryCapacity; } }

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
    public void ClearInventory()
    {
        //Guard Clause
        if (inventory.Count <= 0) return;

        //Get rid of speed penalty
        GameManager.Instance.Encumbrance = 0;

        //Empty carry capacity
        currentCarryCapacity = 0;
        capacityEvent.FloatValue = currentCarryCapacity;
        setCapacityText_Event.CallEvent(capacityEvent);

        //Set all ore UI to zero
        resetOreUI_Event.CallEvent(new VoidEvent());

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

    /// <summary>
    /// Returns the inventory and clears it
    /// </summary>
    /// <returns></returns>
    public OreStats GetLastOre()
    {
        //Cache last ore, clear it, return it
        //Guard Clause
        if (inventory.Count <= 0) return null;

        //Decrease current carry capacity
        currentCarryCapacity--;

        //If dropping ore sets us below max carry capacity, reduce encumbrance
        if (GameManager.Instance.Encumbrance > 0)
        {
            GameManager.Instance.Encumbrance--;
        }

        //Cache latest ore
        OreStats ore = inventory[inventory.Count - 1];

        //Remove ore from stack
        inventory.RemoveAt(inventory.Count - 1);

        //Update capacity text
        capacityEvent.FloatValue = currentCarryCapacity;
        setCapacityText_Event.CallEvent(capacityEvent);

        //Update ore UI
        oreEvent.Value = ore;
        oreEvent.Count = GetOreTypeCount(ore.oreType);
        updateOreUI_Event.CallEvent(oreEvent);

        return ore;
    }
}
