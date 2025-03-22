using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] private int interactLayer;

    private Inventory inventory;

    private bool hasInteracted;

    private void Start()
    {
        inventory = GetComponentInParent<Inventory>();
    }

    public void CanInteract()
    {
        hasInteracted = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Check layer
        if (collision.gameObject.layer != interactLayer) return;

        //Check for button press
        if (!hasInteracted) return;

        hasInteracted = false;

        if (collision.gameObject.TryGetComponent(out IDepositable instance))
        {
            for (int i = 0; i < inventory.CurrentCarryCapcity; i++) 
            {
                instance.Deposit(inventory.GetLastOre());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Clear text prompt
    }
}
