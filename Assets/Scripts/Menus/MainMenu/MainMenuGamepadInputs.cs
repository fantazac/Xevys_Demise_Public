using System;
using UnityEngine;
using XInputDotNetPure;

public class MainMenuGamepadInputs : MonoBehaviour
{
    public delegate void GamepadOnBackButtonPressedInMenuHandler();
    public event GamepadOnBackButtonPressedInMenuHandler OnBackButtonPressedInMenu;

    private GamePadState _state;

    private void Start()
    {
        _state = GamePad.GetState(PlayerIndex.One);
    }

    private void Update()
    {
        _state = GamePad.GetState(PlayerIndex.One);

        if (_state.Buttons.B == ButtonState.Pressed)
        {
            OnBackButtonPressedInMenu();
        }
    }
}