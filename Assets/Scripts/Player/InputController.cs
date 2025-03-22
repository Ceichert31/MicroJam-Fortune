using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    InputSystem_Actions inputAction;
    InputSystem_Actions.PlayerActions playerActions;

    private Rigidbody2D rb;

    [Header("Movement Settings")]
    [SerializeField] private float movementSpeed = 10f;

    private SpriteRenderer playerRenderer;

    private bool isMoving;

    //Getters 
    public bool IsMoving { get { return isMoving; } }

    private void Awake()
    {
        inputAction = new InputSystem_Actions();
        playerActions = inputAction.Player;

        rb = GetComponent<Rigidbody2D>();
        playerRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
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

        rb.linearVelocity = (Time.fixedDeltaTime * 100) * movementSpeed * MoveDirection();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
        isMoving = playerActions.Move.IsInProgress();
    }

    private void OnEnable()
    {
        playerActions.Enable();
    }

    private void OnDisable()
    {
        playerActions.Disable();
    }
}
