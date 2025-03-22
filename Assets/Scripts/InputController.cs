using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    InputSystem_Actions inputAction;
    InputSystem_Actions.PlayerActions playerActions;

    private Rigidbody2D rb;

    [Header("Movement Settings")]
    [SerializeField] private float movementSpeed = 10f;

    private bool isMoving;

    private void Awake()
    {
        inputAction = new InputSystem_Actions();
        playerActions = inputAction.Player;

        rb = GetComponent<Rigidbody2D>();
    }

    private Vector2 MoveDirection()
    {
        return playerActions.Move.ReadValue<Vector2>().normalized;
    }

    private void Move()
    {
        rb.linearVelocity = MoveDirection() * movementSpeed;
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
