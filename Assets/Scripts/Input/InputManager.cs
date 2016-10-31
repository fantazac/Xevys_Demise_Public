using System;
using System.CodeDom;
using UnityEngine;

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

    public delegate void OnEnterPortalHandler();
    public event OnEnterPortalHandler OnEnterPortal;

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
        _keyboardInputs.OnEnterPortal += InputsOnEnterPortal;

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
        _gamepadInputs.OnEnterPortal += InputsOnEnterPortal;
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

    private void InputsOnEnterPortal()
    {
        OnEnterPortal();
    }
}
