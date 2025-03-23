using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    InputSystem_Actions inputAction;
    InputSystem_Actions.PlayerActions playerActions;

    private Rigidbody2D rb;
    private Animator animator;

    [Header("Movement Settings")]
    [SerializeField] private int movementSpeed => GameManager.Instance.MovementSpeed;
    private float encumberance => GameManager.Instance.Encumbrance;

    private SpriteRenderer playerRenderer;

    private Inventory playerInventory;

    private PlayerInteractor playerInteractor;

    [Header("Mining Delay Settings")]
    [SerializeField] private float swingDelay = 0.1f;
    private bool canSwing = true;

    [Header("Event Variables")]
    [SerializeField] private VoidEventChannel eventChannel;
    [SerializeField] private BoolEventChannel bagEventChannel;
    private VoidEvent theEvent;

    private bool isMoving;

    [Header("Dynamite Settings")]
    [SerializeField] private float dynamiteCooldown = 5f;
    [SerializeField] private GameObject dynamitePrefab;

    private bool canPlaceDynamite = true;

    //Getters 
    public bool IsMoving { get { return isMoving; } }
    private void Awake()
    {
        inputAction = new InputSystem_Actions();
        playerActions = inputAction.Player;

        rb = GetComponent<Rigidbody2D>();
        playerRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();

        animator = GetComponent<Animator>();

        playerInventory = GetComponent<Inventory>();    
        playerInteractor = GetComponentInChildren<PlayerInteractor>();
    }
    private Vector2 MoveDirection()
    {
        Vector2 moveDirection = playerActions.Move.ReadValue<Vector2>().normalized;

        //Sprite flipping
        if (moveDirection.x > 0)
        {
            playerRenderer.flipX = false;
        }
        else if (moveDirection.x < 0)
        {
            playerRenderer.flipX = true;
        }

        return moveDirection;
    }

    private void Move()
    {
        //Calculate drag
        float dragForce = encumberance;
        dragForce = Mathf.Clamp(dragForce, 0, movementSpeed - 1);

        //Calculate target velocity
        Vector2 targetVelocity = (Time.fixedDeltaTime * 100) * (movementSpeed - dragForce)* MoveDirection();

        rb.linearVelocity = (targetVelocity);
    }

    private void LeftClickFunc(InputAction.CallbackContext ctx)
    {
        if (!canSwing) return;

        canSwing = false;

        Invoke(nameof(ResetCanSwing), swingDelay);

        // send pickaxe swing event
        eventChannel.CallEvent(theEvent);
    }

    private void ResetCanSwing()
    {
        canSwing = true;
    }

    private void DropLatestItem(InputAction.CallbackContext ctx)
    {
        playerInventory.DropNewestItem();
    }

    private void Interact(InputAction.CallbackContext ctx)
    {
        playerInteractor.CanInteract(true);
    }
    /// <summary>
    /// Cancel interact
    /// </summary>
    /// <param name="ctx"></param>
    private void StopInteracting(InputAction.CallbackContext ctx)
    {
        playerInteractor.CanInteract(false);
    }

    private bool isBagOpen = true;
    private void SetBag(InputAction.CallbackContext ctx)
    {
        isBagOpen = !isBagOpen;

        //Call event 
        bagEventChannel.CallEvent(new BoolEvent(isBagOpen));
    }

    private void Pause(InputAction.CallbackContext ctx)
    {
        GameManager.Instance.SetPauseState();
    }

    private void PlaceDynamite(InputAction.CallbackContext ctx)
    {
        if (!canPlaceDynamite) return;

        canPlaceDynamite = false;

        //Instantiate dynamite
        Instantiate(dynamitePrefab, transform.position, Quaternion.identity);

        Invoke(nameof(ResetPlaceDynamite), dynamiteCooldown);
    }

    private void ResetPlaceDynamite()
    {
        canPlaceDynamite = true;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
        isMoving = playerActions.Move.IsInProgress();

        animator.SetBool("isWalking", isMoving);
    }

    private void OnEnable()
    {
        playerActions.Enable();

        playerActions.Fire.performed += LeftClickFunc;

        playerActions.Drop.performed += DropLatestItem;

        playerActions.Interact.performed += Interact;
        playerActions.Interact.canceled += StopInteracting;

        playerActions.Bag.performed += SetBag;

        playerActions.Pause.performed += Pause;

        playerActions.PlaceMine.performed += PlaceDynamite;
    }

    private void OnDisable()
    {
        playerActions.Disable();

        playerActions.Fire.performed -= LeftClickFunc;

        playerActions.Drop.performed -= DropLatestItem;

        playerActions.Interact.performed -= Interact;
        playerActions.Interact.canceled -= StopInteracting;

        playerActions.Bag.performed -= SetBag;

        playerActions.Pause.performed -= Pause;

        playerActions.PlaceMine.performed -= PlaceDynamite;
    }
}
