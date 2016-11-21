﻿using System;
using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class GamepadInputs : MonoBehaviour
{

    public delegate void GamepadOnMoveHandler(Vector3 movement, bool goesRight);
    public event GamepadOnMoveHandler OnMove;

    public delegate void GamepadOnJumpHandler();
    public event GamepadOnJumpHandler OnJump;

    public delegate void GamepadOnJumpDownHandler();
    public event GamepadOnJumpDownHandler OnJumpDown;

    public delegate void GamepadOnUnderwaterControlHandler(bool goesDown);
    public event GamepadOnUnderwaterControlHandler OnUnderwaterControl;

    public delegate void GamepadOnBootsEquipHandler();
    public event GamepadOnBootsEquipHandler OnIronBootsEquip;

    public delegate void GamepadOnStopHandler();
    public event GamepadOnStopHandler OnStop;

    public delegate void GamepadOnBasicAttackHandler();
    public event GamepadOnBasicAttackHandler OnBasicAttack;

    public delegate void GamepadOnCrouchHandler();
    public event GamepadOnCrouchHandler OnCrouch;

    public delegate void GamepadOnStandingUpHandler();
    public event GamepadOnStandingUpHandler OnStandingUp;

    public delegate void GamepadOnThrowAttackHandler();
    public event GamepadOnThrowAttackHandler OnThrowAttack;

    public delegate void GamepadOnThrowAttackChangeButtonPressedHandler();
    public event GamepadOnThrowAttackChangeButtonPressedHandler OnThrowAttackChangeButtonPressed;

    public delegate void GamepadOnPauseHandler();
    public event GamepadOnPauseHandler OnPause;

    private float _joysticksXAxisDeadZone = 0.7f;
    private float _joysticksYAxisDeadZone = 1f;

    private bool _leftShoulderReady = true;
    private bool _rightShoulderReady = true;
    private bool _xButtonReady = true;
    private bool _yButtonReady = true;
    private bool _aButtonReady = true;
    private bool _startButtonReady = true;

    private bool _usingDpadControlsScheme;

    private GamePadState _state;

    private void Start()
    {
        _state = GamePad.GetState(PlayerIndex.One);
        _usingDpadControlsScheme = false;
    }

    private void Update()
    {
        _state = GamePad.GetState(PlayerIndex.One);

        if (_usingDpadControlsScheme)
        {
            DPadControlsScheme();
        }
        else
        {
            JoystickControlsScheme();
        }

        SyncAllButtonsState();
        CheckAllButtonsPressed();
    }

    private void CheckAllButtonsPressed()
    {
        if (_state.Buttons.A == ButtonState.Pressed && _aButtonReady)
        {
            _aButtonReady = false;
            OnJump();
        }

        if (_state.Buttons.X == ButtonState.Pressed && _xButtonReady)
        {
            _xButtonReady = false;
            OnBasicAttack();
        }

        if (_state.Buttons.Y == ButtonState.Pressed && _yButtonReady)
        {
            _yButtonReady = false;
            OnIronBootsEquip();
        }

        if (_state.Buttons.B == ButtonState.Pressed)
        {
            if (OnThrowAttack != null)
            {
                OnThrowAttack();
            }
        }

        if (_state.Buttons.LeftShoulder == ButtonState.Pressed && _leftShoulderReady)
        {
            _leftShoulderReady = false;
            OnThrowAttackChangeButtonPressed();
        }

        if (_state.Buttons.RightShoulder == ButtonState.Pressed && _rightShoulderReady)
        {
            _rightShoulderReady = false;
            OnThrowAttackChangeButtonPressed();
        }

        if (_state.Buttons.Start == ButtonState.Pressed && _startButtonReady)
        {
            OnPause();
            _startButtonReady = false;
        }
    }

    private void SyncAllButtonsState()
    {
        if (_state.Buttons.Start == ButtonState.Released && !_startButtonReady)
        {
            _startButtonReady = true;
        }

        if (_state.Buttons.RightShoulder == ButtonState.Released && !_rightShoulderReady)
        {
            _rightShoulderReady = true;
        }

        if (_state.Buttons.LeftShoulder == ButtonState.Released && !_leftShoulderReady)
        {
            _leftShoulderReady = true;
        }

        if (_state.Buttons.Y == ButtonState.Released && !_yButtonReady)
        {
            _yButtonReady = true;
        }

        if (_state.Buttons.X == ButtonState.Released && !_xButtonReady)
        {
            _xButtonReady = true;
        }

        if (_state.Buttons.A == ButtonState.Released && !_aButtonReady)
        {
            _aButtonReady = true;
        }
    }

    private void JoystickControlsScheme()
    {
        if (Math.Abs(_state.ThumbSticks.Left.X) > _joysticksXAxisDeadZone)
        {
            if (_state.ThumbSticks.Left.X < 0)
            {
                OnMove(Vector3.left, false);
            }
            else
            {
                OnMove(Vector3.right, true);
            }
        }
        else
        {
            OnStop();
        }

        if (Math.Abs(_state.ThumbSticks.Left.Y) == _joysticksYAxisDeadZone)
        {
            if (_state.ThumbSticks.Left.Y < 0)
            {
                OnUnderwaterControl(true);
                OnCrouch();
                if (_state.Buttons.A == ButtonState.Pressed)
                {
                    OnJumpDown();
                }
            }
        }

        if (_state.ThumbSticks.Left.Y >= 0)
        {
            OnStandingUp();
            if (_state.ThumbSticks.Left.Y > 0)
            {
                OnUnderwaterControl(false);
            }
        }
    }

    private void DPadControlsScheme()
    {

        if (_state.DPad.Left == ButtonState.Pressed && _state.DPad.Right != ButtonState.Pressed && _state.DPad.Down != ButtonState.Pressed)
        {
            OnMove(Vector3.left, false);
        }
        else if (_state.DPad.Right == ButtonState.Pressed && _state.DPad.Left != ButtonState.Pressed && _state.DPad.Down != ButtonState.Pressed)
        {
            OnMove(Vector3.right, true);
        }
        else
        {
            OnStop();
        }

        if (_state.DPad.Down == ButtonState.Pressed)
        {
            OnUnderwaterControl(true);
            OnCrouch();
            if (_state.Buttons.A == ButtonState.Pressed)
            {
                OnJumpDown();
            }
        }
        else
        {
            OnStandingUp();
            OnUnderwaterControl(false);
        }
    }

    private void SetUsingDPadControlsScheme()
    {
        _usingDpadControlsScheme = true;
    }

    private void SetUsingJoystickControlsScheme()
    {
        _usingDpadControlsScheme = false;
    }
}