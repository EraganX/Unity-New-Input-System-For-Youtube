using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    public static PlayerInputManager Instance;

    private PlayerInputs inputAction;

    private void Awake()
    {
        if (Instance == null) Instance = this;

        inputAction = new PlayerInputs();
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
        return inputAction.Tank.Move.ReadValue<Vector2>();
    }

    public Vector2 GetMousePosition()
    {
        return inputAction.Tank.MousePosition.ReadValue<Vector2>();
    }

    public bool GetFireInput()
    {
        return inputAction.Tank.Fire.triggered;
    }

}
