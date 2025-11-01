using System;
using UnityEngine;
using UnityEngine.InputSystem; // Using the new Input System package

[RequireComponent(typeof(Rigidbody))]
public class PlayerControl : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    private Rigidbody rb;

    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    private Vector2 moveInput;
    private Vector3 moveDirection;

    [Header("UI")]
    public GameObject buttonContainer;
    public bool uiOpen = false;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        playerInputActions.Player.Enable();
        playerInputActions.Player.Jump.performed += OnJumpPerformed;

        playerInputActions.Player.PauseMenuOpen.performed += OnPauseMenu;
        playerInputActions.UI.PauseMenuExit.performed += OnPauseMenu;
    }

    private void OnPauseMenu(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            uiOpen = !uiOpen;
            if (uiOpen)
            {
                playerInputActions.Player.Disable();
                playerInputActions.UI.Enable();
            }
            else
            {
                playerInputActions.UI.Disable();
                playerInputActions.Player.Enable();
            }

            buttonContainer.SetActive(uiOpen);
        }
    }

    private void OnDisable()
    {
        playerInputActions.Player.Disable();
        playerInputActions.Player.Jump.performed -= OnJumpPerformed;

        playerInputActions.Player.PauseMenuOpen.performed -= OnPauseMenu;
        playerInputActions.UI.PauseMenuExit.performed -= OnPauseMenu;
    }

    private void Update()
    {
        moveInput = playerInputActions.Player.Move.ReadValue<Vector2>();
        moveDirection = new Vector3(moveInput.x, 0, moveInput.y);
    }

    private void FixedUpdate()
    {
        Vector3 newVelocity = moveDirection * moveSpeed;
        newVelocity.y = rb.linearVelocity.y; // Preserve existing vertical velocity
        rb.linearVelocity = newVelocity;
    }

    private bool isGrounded = true;

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        if (isGrounded && context.performed)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

}
