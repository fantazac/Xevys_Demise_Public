using UnityEngine;
using XInputDotNetPure;

public class AchievementInputsManager : MonoBehaviour
{
    public delegate void OnBackButtonPressedHandler();
    public event OnBackButtonPressedHandler OnBackButtonPressed;

    public delegate void OnInputsSchemeChangedHandler();
    public event OnInputsSchemeChangedHandler OnInputsSchemeChanged;

    private AchievementKeyboardInputs _keyboardInputs;
    private AchievementGamepadInputs _gamepadInputs;

    private void Start()
    {
        _keyboardInputs = GetComponentInChildren<AchievementKeyboardInputs>();
        _keyboardInputs.KeyboardOnEscapeButtonPressed += BackButtonPressed;


        _gamepadInputs = GetComponentInChildren<AchievementGamepadInputs>();
        _gamepadInputs.GamepadOnBackButtonPressed += BackButtonPressed;
    }

    private void FixedUpdate()
    {
        // Un seul schéma de contrôle est activé à la fois.
        // Si le joueur appuie sur une touche du support qui n'est pas actif,
        // on change le schéma de contrôle.

        if ((!_keyboardInputs.enabled && _gamepadInputs.enabled && PlayerIsUsingKeyboard()) ||
            (!_gamepadInputs.enabled && _keyboardInputs.enabled && PlayerIsUsingGamepad()))
        {
            OnInputsSchemeChanged();
            _keyboardInputs.enabled = !_keyboardInputs.enabled;
            _gamepadInputs.enabled = !_gamepadInputs.enabled;
        }
    }

    private bool PlayerIsUsingKeyboard()
    {
        return Input.anyKey;
    }

    private bool PlayerIsUsingGamepad()
    {
        GamePadState state = GamePad.GetState(PlayerIndex.One);
        return (state.Buttons.A == ButtonState.Pressed || state.Buttons.B == ButtonState.Pressed ||
                    state.Buttons.X == ButtonState.Pressed || state.Buttons.Y == ButtonState.Pressed ||
                    state.ThumbSticks.Left.X != 0 || state.ThumbSticks.Left.Y != 0 ||
                    state.ThumbSticks.Right.Y != 0 || state.ThumbSticks.Right.Y != 0 ||
                    state.Buttons.LeftShoulder == ButtonState.Pressed ||
                    state.Buttons.RightShoulder == ButtonState.Pressed ||
                    state.Buttons.Back == ButtonState.Pressed || state.Buttons.Start == ButtonState.Pressed ||
                    state.DPad.Left == ButtonState.Pressed || state.DPad.Right == ButtonState.Pressed ||
                    state.DPad.Up == ButtonState.Pressed || state.DPad.Down == ButtonState.Pressed);

    }

    private void BackButtonPressed()
    {
        OnBackButtonPressed();
    }
}
