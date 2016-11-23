using System;
using System.CodeDom;
using UnityEngine;
using XInputDotNetPure;

public class InputManager : MonoBehaviour
{
    public delegate void OnMoveHandler(Vector3 movement, bool goesRight);
    public event OnMoveHandler OnMove;

    public delegate void OnJumpHandler();
    public event OnJumpHandler OnJump;

    public delegate void OnJumpDownHandler();
    public event OnJumpDownHandler OnJumpDown;

    public delegate void OnUnderwaterControlHandler(bool goesDown);
    public event OnUnderwaterControlHandler OnUnderwaterControl;

    public delegate void OnBootsEquipHandler();
    public event OnBootsEquipHandler OnIronBootsEquip;

    public delegate void OnStopHandler();
    public event OnStopHandler OnStop;

    public delegate void OnBasicAttackHandler();
    public event OnBasicAttackHandler OnBasicAttack;

    public delegate void OnCrouchHandler();
    public event OnCrouchHandler OnCrouch;

    public delegate void OnStandingUpHandler();
    public event OnStandingUpHandler OnStandingUp;

    public delegate void OnThrowAttackHandler();
    public event OnThrowAttackHandler OnThrowAttack;

    public delegate void OnThrowAttackChangeButtonPressedHandler();
    public event OnThrowAttackChangeButtonPressedHandler OnThrowAttackChangeButtonPressed;

    public delegate void OnPauseHandler();
    public event OnPauseHandler OnPause;

    public delegate void OnFlipHandler(bool goesRight);
    public event OnFlipHandler OnFlip;

    private KeyboardInputs _keyboardInputs;
    private GamepadInputs _gamepadInputs;

    private void Start()
    {
        _keyboardInputs = GetComponentInChildren<KeyboardInputs>();
        _keyboardInputs.OnMove += InputsOnMove;
        _keyboardInputs.OnJump += InputsOnJump;
        _keyboardInputs.OnJumpDown += InputsOnJumpDown;
        _keyboardInputs.OnUnderwaterControl += InputsOnUnderwaterControl;
        _keyboardInputs.OnIronBootsEquip += InputsOnIronBootsEquip;
        _keyboardInputs.OnStop += InputsOnStop;
        _keyboardInputs.OnBasicAttack += InputsOnBasicAttack;
        _keyboardInputs.OnCrouch += InputsOnCrouch;
        _keyboardInputs.OnStandingUp += InputsOnStandingUp;
        _keyboardInputs.OnThrowAttack += InputsOnThrowAttack;
        _keyboardInputs.OnThrowAttackChangeButtonPressed += InputsOnThrowAttackChangeButtonPressed;
        _keyboardInputs.OnPause += InputsOnPause;
        _keyboardInputs.OnFlip += InputsOnFlip;

        _gamepadInputs = GetComponentInChildren<GamepadInputs>();
        _gamepadInputs.OnMove += InputsOnMove;
        _gamepadInputs.OnJump += InputsOnJump;
        _gamepadInputs.OnJumpDown += InputsOnJumpDown;
        _gamepadInputs.OnUnderwaterControl += InputsOnUnderwaterControl;
        _gamepadInputs.OnIronBootsEquip += InputsOnIronBootsEquip;
        _gamepadInputs.OnStop += InputsOnStop;
        _gamepadInputs.OnBasicAttack += InputsOnBasicAttack;
        _gamepadInputs.OnCrouch += InputsOnCrouch;
        _gamepadInputs.OnStandingUp += InputsOnStandingUp;
        _gamepadInputs.OnThrowAttack += InputsOnThrowAttack;
        _gamepadInputs.OnThrowAttackChangeButtonPressed += InputsOnThrowAttackChangeButtonPressed;
        _gamepadInputs.OnPause += InputsOnPause;

    }

    private void FixedUpdate()
    {
        // Un seul schéma de contrôle est activé à la fois.
        // Si le joueur appuie sur une touche du support qui n'est pas actif,
        // on change le schéma de contrôle.

        if ((!_keyboardInputs.enabled && _gamepadInputs.enabled && PlayerIsUsingKeyboard()) ||
            (!_gamepadInputs.enabled && _keyboardInputs.enabled && PlayerIsUsingGamepad()))
        {    
            _keyboardInputs.enabled = !_keyboardInputs.enabled;
            _gamepadInputs.enabled = !_gamepadInputs.enabled;
        }
    }

    private bool PlayerIsUsingKeyboard()
    {
        return (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) ||
                Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) ||
                Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.E) ||
                Input.GetKey(KeyCode.K) || Input.GetKey(KeyCode.L) ||
                Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Escape) ||
                Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) ||
                Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow));
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

    private void InputsOnMove(Vector3 movement, bool goesRight)
    {
        OnMove(movement, goesRight);
    }

    private void InputsOnJump()
    {
        OnJump();
    }

    private void InputsOnJumpDown()
    {
        OnJumpDown();
    }

    private void InputsOnUnderwaterControl(bool goesDown)
    {
        OnUnderwaterControl(goesDown);
    }

    private void InputsOnIronBootsEquip()
    {
        OnIronBootsEquip();
    }

    private void InputsOnStop()
    {
        OnStop();
    }

    private void InputsOnBasicAttack()
    {
        OnBasicAttack();
    }

    private void InputsOnCrouch()
    {
        OnCrouch();
    }

    private void InputsOnStandingUp()
    {
        OnStandingUp();
    }

    private void InputsOnThrowAttack()
    {
        if (OnThrowAttack != null)
        {
            OnThrowAttack();
        }
    }

    private void InputsOnThrowAttackChangeButtonPressed()
    {
        OnThrowAttackChangeButtonPressed();
    }

    private void InputsOnPause()
    {
        OnPause();
    }

    private void InputsOnFlip(bool goesRight)
    {
        OnFlip(goesRight);
    }
}
