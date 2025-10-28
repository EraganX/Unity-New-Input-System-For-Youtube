using UnityEngine;

public class GameInputManager : MonoBehaviour
{
    public static GameInputManager Instance;

    private PlayerInputActions inputAction;

    private void Awake()
    {
        if (Instance == null) Instance = this;

        inputAction = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputAction.Enable();
    }

    private void OnDisable()
    {
        inputAction.Disable();
    }

    public Vector2 GetMoveInput()
    {
        return inputAction.Player.Move.ReadValue<Vector2>();
    }

    public bool GetJumpInput()
    {
        return inputAction.Player.Jump.triggered;
    }
}
