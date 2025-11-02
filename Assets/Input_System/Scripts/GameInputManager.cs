using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInputManager : MonoBehaviour
{
    public static GameInputManager instace;
    private PlayerInputActions playerInputAction;

    public Vector2 MoveInputs { get; private set; }
    public bool JumpInput { get; private set; }
    public bool MenuOpen { get; private set; }

    public event System.Action ToggleMenuEvent;

    private void Awake()
    {
        if (instace == null)
        {
            instace = this;
            DontDestroyOnLoad(gameObject);
        }

        playerInputAction = new PlayerInputActions();
    }

    private void OnEnable()
    {
        playerInputAction.Player.Enable();

        playerInputAction.Player.Jump.performed += OnJump;
        playerInputAction.Player.PauseMenuOpen.performed += OnMenuOpen;
        playerInputAction.UI.PauseMenuExit.performed += OnMenuOpen;
    }

    private void OnDisable()
    {
        playerInputAction.Player.Jump.performed -= OnJump;
        playerInputAction.Player.PauseMenuOpen.performed -= OnMenuOpen;
        playerInputAction.UI.PauseMenuExit.performed -= OnMenuOpen;
    }

    private void OnMenuOpen(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            MenuOpen = !MenuOpen;

            if (MenuOpen)
            {
                PlayerController(false);
                UIController(true);
            }
            else
            {
                PlayerController(true);
                UIController(false);
            }

            ToggleMenuEvent?.Invoke();
        }
    }

    public void UIController(bool isOpen)
    {
        if (isOpen)
        {
            playerInputAction.UI.Enable();
        }
        else
        {
            playerInputAction.UI.Disable();
        }
    }

    public void PlayerController(bool IsControl)
    {
        if (IsControl)
        {
            playerInputAction.Player.Enable();
        }
        else
        {
            playerInputAction.Player.Disable();
        }
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            JumpInput = true;
        }
    }

    private void LateUpdate()
    {
        JumpInput = false;
    }

    private void Update()
    {
        MoveInputs = playerInputAction.Player.Move.ReadValue<Vector2>();
    }
}
