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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Check layer
        //Try get
        //check for e
        if (collision.gameObject.layer != interactLayer) return;

        //Check for button press
        if (!hasInteracted) return;

        hasInteracted = false;

        Debug.Log("1");

        if (collision.gameObject.TryGetComponent(out IDepositable instance))
        {
            Debug.Log("2");
            foreach (OreStats ore in inventory.GetAllOre()) 
            {
                Debug.Log("3");
                instance.Deposit(ore, 1);
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }
}
