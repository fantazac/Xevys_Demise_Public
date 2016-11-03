using System;
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

    public delegate void GamepadOnEnterPortalHandler();
    public event GamepadOnEnterPortalHandler OnEnterPortal;

    public delegate void GamepadOnPauseHandler();
    public event GamepadOnPauseHandler OnPause;

    private float _joysticksXAxisDeadZone = 0.1f;
    private float _joysticksYAxisDeadZone = 1f;

    private bool _leftShoulderReady = true;
    private bool _rightShoulderReady = true;
    private bool _xButtonReady = true;
    private bool _yButtonReady = true;
    private bool _aButtonReady = true;
    private bool _upButtonReady = true;
    private bool _startButtonReady = true;

    private void Update()
    {
        
        foreach (PlayerIndex player in Enum.GetValues(typeof(PlayerIndex)))
        {
            GamePadState state = GamePad.GetState(player);

            if (state.IsConnected)
            {
                if (Math.Abs(state.ThumbSticks.Left.X) > _joysticksXAxisDeadZone)
                {
                    if (state.ThumbSticks.Left.X < 0)
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

                if (Math.Abs(state.ThumbSticks.Left.Y) == _joysticksYAxisDeadZone)
                {
                    if (state.ThumbSticks.Left.Y < 0)
                    {
                        OnUnderwaterControl(true);
                        OnCrouch();
                        if (state.Buttons.A == ButtonState.Pressed)
                        {
                            OnJumpDown();
                        }
                    }
                }

                if (state.ThumbSticks.Left.Y >= 0)
                {
                    OnStandingUp();
                    if (state.ThumbSticks.Left.Y > 0)
                    {
                        OnUnderwaterControl(false);
                        if (_upButtonReady)
                        {
                            _upButtonReady = false;
                            OnEnterPortal();
                        }
                    }
                }

                if (!_upButtonReady && state.ThumbSticks.Left.Y <= 0)
                {
                    _upButtonReady = true;
                }

                if (state.Buttons.A == ButtonState.Pressed && _aButtonReady && state.ThumbSticks.Left.Y != -_joysticksYAxisDeadZone)
                {
                    _aButtonReady = false;
                    OnJump();
                }
                if (state.Buttons.A == ButtonState.Released && !_aButtonReady)
                {
                    _aButtonReady = true;
                }

                if (state.Buttons.Y == ButtonState.Pressed && _yButtonReady)
                {
                    _yButtonReady = false;
                    OnIronBootsEquip();
                }
                if (state.Buttons.Y == ButtonState.Released && !_yButtonReady)
                {
                    _yButtonReady = true;
                }

                if (state.Buttons.X == ButtonState.Pressed && _xButtonReady)
                {
                    _xButtonReady = false;
                    OnBasicAttack();
                }
                if (state.Buttons.X == ButtonState.Released && !_xButtonReady)
                {
                    _xButtonReady = true;
                }

                if (state.Buttons.B == ButtonState.Pressed)
                {
                    if (OnThrowAttack != null)
                    {
                        OnThrowAttack();
                    }
                }

                if (state.Buttons.LeftShoulder == ButtonState.Pressed && _leftShoulderReady)
                {
                    _leftShoulderReady = false;
                    OnThrowAttackChangeButtonPressed();
                }

                if (state.Buttons.LeftShoulder == ButtonState.Released && !_leftShoulderReady)
                {
                    _leftShoulderReady = true;
                }

                if (state.Buttons.RightShoulder == ButtonState.Pressed && _rightShoulderReady)
                {
                    _rightShoulderReady = false;
                    OnThrowAttackChangeButtonPressed();
                }
                if (state.Buttons.RightShoulder == ButtonState.Released && !_rightShoulderReady)
                {
                    _rightShoulderReady = true;
                }
                if (state.Buttons.Start == ButtonState.Pressed && _startButtonReady)
                {
                    OnPause();
                    _startButtonReady = false;
                }
                if (state.Buttons.Start == ButtonState.Released && !_startButtonReady)
                {
                    _startButtonReady = true;
                }
            }
        }
    }
}
