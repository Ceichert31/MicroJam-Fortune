using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] private int interactLayer;

    [SerializeField] private float interactCooldownMax = 0.5f;
    [SerializeField] private float interactCooldownMin = 0.2f;

    private float currentDelay;

    private Inventory inventory;

    private bool hasInteracted;
    private bool canInteract = true;

    private void Start()
    {
        inventory = GetComponentInParent<Inventory>();
    }

    public void CanInteract(bool canInteract)
    {
        hasInteracted = canInteract;
        currentDelay = interactCooldownMax;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Check layer
        if (collision.gameObject.layer != interactLayer) return;

        if (!canInteract) return;

        canInteract = false;

        //Speed up deposit
        currentDelay = Mathf.Lerp(interactCooldownMax, interactCooldownMin, Time.deltaTime);

        //Reset
        Invoke(nameof(ResetCanInteract), currentDelay);

        //Check if object has IDepositable interface
        if (collision.gameObject.TryGetComponent(out IDepositable instance))
        {
            //Cache ore
            OreStats ore = inventory.GetLastOre();

            //If inventory empty, break
            if (ore == null) return;

            //Deposit
            instance.Deposit(ore);
        }
    }

    private void ResetCanInteract()
    {
        canInteract = true;
    }
   

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Clear text prompt
    }
}
