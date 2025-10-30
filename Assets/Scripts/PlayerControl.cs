using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    /*    private void OnJump()
        {
            print("Jump Action");
        }

        private void OnMove(InputValue value)
        {
            Vector2 moveInput = value.Get<Vector2>();
            print("Move Action");
        }*/

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            print("Jumping");
        }
    }

    public void Movement(InputAction.CallbackContext context)
    {
        Vector2 moveInput = context.ReadValue<Vector2>();
        print("Moving");
    }
}
