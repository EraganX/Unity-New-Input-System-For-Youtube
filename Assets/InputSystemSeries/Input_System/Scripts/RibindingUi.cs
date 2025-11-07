using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class RibindingUi : MonoBehaviour
{
    [Header("UI Elements")]
    public Button rebindJumpButton;
    public Text jumpKeyText;

    private void Start()
    {
        UpdateAllBindingText();

        rebindJumpButton.onClick.AddListener(RebindJump);
    }

    public void UpdateAllBindingText()
    {
        int keyboardIndex = 0;

        jumpKeyText.text = GameInputManager.instace.playerInputAction.Player.Jump
            .GetBindingDisplayString(keyboardIndex, InputBinding.DisplayStringOptions.DontIncludeInteractions);

    }

    private void RebindJump()
    {
        rebindJumpButton.interactable = false;
        jumpKeyText.text = "Wainting.....";

        int keyboardIndex = 0;

        GameInputManager.instace.StartRebindings(
            GameInputManager.instace.playerInputAction.Player.Jump,
            keyboardIndex,
            () =>
            {
                rebindJumpButton.interactable = true;
                UpdateAllBindingText();
            },
            () =>
            {
                rebindJumpButton.interactable = true;
                UpdateAllBindingText();
            }
            );

    }
}
