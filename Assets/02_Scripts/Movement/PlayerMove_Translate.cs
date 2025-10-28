using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMove_Translate : MonoBehaviour
{
    [Header("Move")]
    public float speed = 5f;
    private Vector2 moveInput;
    private Vector3 move;

    [Header("Jump")]
    private Rigidbody rb;
    private float jumpForce = 5f;
    public ForceMode force;
    private bool isJumping;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        moveInput = GameInputManager.Instance.GetMoveInput();
        move = new Vector3(moveInput.x, 0, moveInput.y);            //Set Move Direction

        transform.Translate(move * speed * Time.deltaTime, Space.World); //Object Move Using Transform.Translate

        if (isJumping == false && GameInputManager.Instance.GetJumpInput())
        {
            isJumping = true;
        }
    }

    private void FixedUpdate()
    {
        if (isJumping)
        {
            rb.AddForce(Vector3.up * jumpForce, force);
            isJumping = false;
        }
    }


}
