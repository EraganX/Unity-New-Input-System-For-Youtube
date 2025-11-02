using System;
using UnityEngine;
using UnityEngine.InputSystem; // Using the new Input System package

[RequireComponent(typeof(Rigidbody))]
public class PlayerControl : MonoBehaviour
{
    private GameInputManager input;
    private Rigidbody rb;

    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    private Vector2 moveInput;
    private Vector3 moveDirection;
    private bool isGrounded = true;

    [Header("UI")]
    public GameObject buttonContainer;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        input = GameInputManager.instace;
    }

    private void OnEnable()
    {
        input.PlayerController(true);
        input.ToggleMenuEvent += OnPuaseMenu;
    }

    private void OnPuaseMenu()
    {
        buttonContainer.SetActive(input.MenuOpen);
    }

    private void OnDisable()
    {
        input.ToggleMenuEvent -= OnPuaseMenu;
    }

    private void Update()
    {
        moveInput = input.MoveInputs;
        moveDirection = new Vector3(moveInput.x, 0, moveInput.y);

        if (input.JumpInput && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void FixedUpdate()
    {
        Vector3 newVelocity = moveDirection * moveSpeed;
        newVelocity.y = rb.linearVelocity.y; // Preserve existing vertical velocity
        rb.linearVelocity = newVelocity;
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
