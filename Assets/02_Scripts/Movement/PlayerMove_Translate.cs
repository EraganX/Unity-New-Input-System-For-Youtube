using UnityEngine;

public class PlayerMove_Translate : MonoBehaviour
{
    public float speed = 5f;

    private Vector2 moveInput;
    private Vector3 move;
    private PlayerInputActions inputActions;

    private void Awake()
    {
        inputActions = new PlayerInputActions(); //Assign Input Action
    }

    private void OnEnable()
    {
        inputActions.Enable(); //Input Action Enable
    }

    private void OnDisable()
    {
        inputActions.Disable(); //Input Action Disble
    }

    private void Update()
    {
        moveInput = inputActions.Player.Move.ReadValue<Vector2>();  //Get Keyboad Input: Move
        move = new Vector3(moveInput.x, 0, moveInput.y);            //Set Move Direction

        transform.Translate(move * speed * Time.deltaTime, Space.World); //Object Move Using Transform.Translate
    }
}
