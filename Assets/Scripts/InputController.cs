using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    InputSystem_Actions inputAction;
    InputSystem_Actions.PlayerActions playerActions;

    private Rigidbody2D rb;

    [Header("Movement Settings")]
    [SerializeField] private float movementSpeed = 10f;

    private void Awake()
    {
        inputAction = new InputSystem_Actions();
        playerActions = inputAction.Player;

        rb = GetComponent<Rigidbody2D>();
    }

    private Vector2 MoveDirection()
    {
        Vector2 direction = playerActions.Move.ReadValue<Vector2>();
        return direction.normalized;
    }

    private void Move(InputAction.CallbackContext ctx)
    {
        rb.linearVelocity = MoveDirection() * movementSpeed;
    }

    private void OnEnable()
    {
        playerActions.Enable();

        playerActions.Move.performed += Move;
    }

    private void OnDisable()
    {
        playerActions.Disable();

        playerActions.Move.performed -= Move;
    }
}
