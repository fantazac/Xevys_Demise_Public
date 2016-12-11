using UnityEngine;
using XInputDotNetPure;

public class AchievementGamepadInputs : MonoBehaviour
{
    public delegate void GamepadOnBackButtonPressedHandler();
    public event GamepadOnBackButtonPressedHandler GamepadOnBackButtonPressed;

    private GamePadState _state;

    private bool _bButtonReady = true;

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
        if (_state.Buttons.B == ButtonState.Released && !_bButtonReady)
        {
            _bButtonReady = true;
        }

        if (_state.Buttons.B == ButtonState.Pressed && _bButtonReady)
        {
            _bButtonReady = false;
            GamepadOnBackButtonPressed();
        }
    }
}