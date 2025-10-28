using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMove_Translate : MonoBehaviour
{
    public float speed = 5f;

    private Vector2 moveInput;
    private Vector3 move;
    private PlayerInputActions inputActions;

    [Header("Jump")]
    private Rigidbody rb;
    private float jumpForce = 5f;
    public ForceMode force;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        inputActions = new PlayerInputActions(); //Assign Input Action
    }

    private void OnEnable()
    {
        inputActions.Enable(); //Input Action Enable

        inputActions.Player.Jump.performed += OnJumpPerform; //Subcribe Jump Action
    }



    private void OnDisable()
    {
        inputActions.Player.Jump.performed -= OnJumpPerform; //Unsubcribe Jump Action
        inputActions.Disable(); //Input Action Disble
    }

    private void Update()
    {
        moveInput = inputActions.Player.Move.ReadValue<Vector2>();  //Get Keyboad Input: Move
        move = new Vector3(moveInput.x, 0, moveInput.y);            //Set Move Direction

        transform.Translate(move * speed * Time.deltaTime, Space.World); //Object Move Using Transform.Translate
    }

    private void OnJumpPerform(InputAction.CallbackContext context)
    {
        rb.AddForce(Vector3.up * jumpForce, force);
    }
}
