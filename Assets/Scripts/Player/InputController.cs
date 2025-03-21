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

    [Header("Event Variables")]
    [SerializeField] private VoidEventChannel eventChannel;
    private VoidEvent theEvent;

    private bool isMoving;
    private bool hasInteracted;

    //Getters 
    public bool IsMoving { get { return isMoving; } }
    public bool HasInteracted { get { return hasInteracted;} }


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
        Vector2 targetVelocity = (Time.fixedDeltaTime * 100) * (movementSpeed - dragForce) * MoveDirection();

        rb.linearVelocity = (targetVelocity);
    }

    private void LeftClickFunc(InputAction.CallbackContext ctx)
    {
        // send pickaxe swing event
        eventChannel.CallEvent(theEvent);
    }

    private void DropLatestItem(InputAction.CallbackContext ctx)
    {
        playerInventory.DropNewestItem();
    }

    private void Interact(InputAction.CallbackContext ctx)
    {
        playerInteractor.CanInteract();
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
    }

    private void OnDisable()
    {
        playerActions.Disable();

        playerActions.Fire.performed -= LeftClickFunc;

        playerActions.Drop.performed -= DropLatestItem;

        playerActions.Interact.performed -= Interact;
    }
}
