using UnityEngine;
using XInputDotNetPure;

public class AchievementGamepadInputs : MonoBehaviour
{
    public delegate void GamepadOnBackButtonPressedHandler();
    public event GamepadOnBackButtonPressedHandler GamepadOnBackButtonPressed;

    private GamePadState _state;

    private void Start()
    {
        _state = GamePad.GetState(PlayerIndex.One);
    }

    private void Update()
    {
        _state = GamePad.GetState(PlayerIndex.One);

        UpdateStartButton();
    }

    private void UpdateStartButton()
    {
        if (_state.Buttons.B == ButtonState.Pressed)
        {
            GamepadOnBackButtonPressed();
        }
    }
}