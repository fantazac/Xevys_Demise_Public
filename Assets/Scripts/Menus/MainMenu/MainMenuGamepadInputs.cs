using System;
using UnityEngine;
using XInputDotNetPure;

public class MainMenuGamepadInputs : MonoBehaviour
{
    public delegate void GamepadOnBackButtonPressedInMenuHandler();
    public event GamepadOnBackButtonPressedInMenuHandler OnBackButtonPressedInMenu;

    private GamePadState _state;

    private bool _bButtonReady = true;

    private void Start()
    {
        _state = GamePad.GetState(PlayerIndex.One);
    }

    private void Update()
    {
        _state = GamePad.GetState(PlayerIndex.One);

        if (_state.Buttons.B == ButtonState.Released && !_bButtonReady)
        {
            _bButtonReady = true;
        }

        if (_state.Buttons.B == ButtonState.Pressed && _bButtonReady)
        {
            _bButtonReady = false;
            OnBackButtonPressedInMenu();
        }
    }
}