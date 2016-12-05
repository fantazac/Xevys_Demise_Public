using System;
using UnityEngine;

public class KeyboardInputs : MonoBehaviour
{

    public delegate void KeyboardOnMoveHandler(Vector3 movement, bool goesRight);
    public event KeyboardOnMoveHandler OnMove;

    public delegate void KeyboardOnJumpHandler();
    public event KeyboardOnJumpHandler OnJump;

    public delegate void KeyboardOnJumpDownHandler();
    public event KeyboardOnJumpDownHandler OnJumpDown;

    public delegate void KeyboardOnUnderwaterControlHandler(bool goesDown);
    public event KeyboardOnUnderwaterControlHandler OnUnderwaterControl;

    public delegate void KeyboardOnBootsEquipHandler();
    public event KeyboardOnBootsEquipHandler OnIronBootsEquip;

    public delegate void KeyboardOnStopHandler();
    public event KeyboardOnStopHandler OnStop;

    public delegate void KeyboardOnBasicAttackHandler();
    public event KeyboardOnBasicAttackHandler OnBasicAttack;

    public delegate void KeyboardOnCrouchHandler();
    public event KeyboardOnCrouchHandler OnCrouch;

    public delegate void KeyboardOnStandingUpHandler();
    public event KeyboardOnStandingUpHandler OnStandingUp;

    public delegate void KeyboardOnThrowAttackHandler();
    public event KeyboardOnThrowAttackHandler OnThrowAttack;

    public delegate void KeyboardOnThrowAttackChangeButtonPressedHandler();
    public event KeyboardOnThrowAttackChangeButtonPressedHandler OnThrowAttackChangeButtonPressed;

    public delegate void KeyboardOnPauseHandler(bool isDead);
    public event KeyboardOnPauseHandler OnPause;

    public delegate void KeyboardOnFlipHandler(bool goesRight);
    public event KeyboardOnFlipHandler OnFlip;

    public delegate void KeyboardOnEnterPortalHandler();
    public event KeyboardOnEnterPortalHandler OnEnterPortal;

    private PauseMenuAnimationManager _pauseMenuAnimationManager;
    private PauseMenuCurrentInterfaceAnimator _pauseMenuCurrentInterfaceAnimator;

    public delegate void KeyboardOnCheatHandler(int item);
    public event KeyboardOnCheatHandler OnCheat;

    private bool _usingArrowControlsScheme;
    private bool _inMenu;
    private bool _died;

    private void Start()
    {
        ControlSchemesManager.OnKeyboardControlChanged += SetUsingArrowControlsScheme;
        _pauseMenuCurrentInterfaceAnimator = GameObject.Find(StaticObjects.GetMainObjects().PauseMenuButtons).GetComponent<PauseMenuCurrentInterfaceAnimator>();
        _pauseMenuAnimationManager = StaticObjects.GetPauseMenuPanel().GetComponent<PauseMenuAnimationManager>();
        _pauseMenuAnimationManager.OnPauseMenuOutOfScreen += IsInMenu;
        _pauseMenuCurrentInterfaceAnimator.OnPlayerDeathShowDeathInterface += PlayerDied;

        _usingArrowControlsScheme = false;
        _inMenu = false;
        _died = false;
    }

    private void Update()
    {
        if (!_inMenu)
        {
            if (_usingArrowControlsScheme)
            {
                ArrowControlsScheme();
            }
            else
            {
                WASDControlsScheme();
            }

            CheckCheatKeys();
            CheckAllKeysPressed();
            UpdateStartButton();
        }
        else if (!_died)
        {
            UpdateStartButton();
        }
    }

    private void CheckCheatKeys()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            OnCheat(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            OnCheat(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            OnCheat(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            OnCheat(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            OnCheat(4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            OnCheat(5);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            OnCheat(6);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            OnCheat(7);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            OnCheat(8);
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            OnCheat(9);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            OnCheat(10);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            OnCheat(11);
        }
    }

    private void IsInMenu(bool isActive)
    {
        _inMenu = isActive;
        if (!isActive)
        {
            _died = false;
        }
    }

    private void PlayerDied(bool isDead)
    {
        _died = isDead;
    }

    private void UpdateStartButton()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnPause(false);
        }
    }

    private void CheckAllKeysPressed()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            OnIronBootsEquip();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            OnThrowAttackChangeButtonPressed();
        }
    }

    private void WASDControlsScheme()
    {
        if (Input.GetKey(KeyCode.S))
        {
            OnCrouch();
            OnUnderwaterControl(true);

            if (Input.GetKey(KeyCode.Space))
            {
                OnJumpDown();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            OnJump();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            OnBasicAttack();
        }
        else if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && !StaticObjects.GetPlayerState().IsCroutching)
        {
            OnMove(Vector3.left, false);
        }
        else if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A) && !StaticObjects.GetPlayerState().IsCroutching)
        {
            OnMove(Vector3.right, true);
        }
        else if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && !StaticObjects.GetPlayerState().IsAttacking)
        {
            OnFlip(false);
        }
        else if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A) && !StaticObjects.GetPlayerState().IsAttacking)
        {
            OnFlip(true);
        }
        else
        {
            OnStop();
        }

        if (Input.GetKey(KeyCode.L))
        {
            OnThrowAttack();
        }

        if (Input.GetKey(KeyCode.W))
        {
            OnUnderwaterControl(false);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            OnEnterPortal();
        }

        if (!Input.GetKey(KeyCode.S))
        {
            OnStandingUp();
        }
    }

    private void ArrowControlsScheme()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            OnCrouch();
            OnUnderwaterControl(true);

            if (Input.GetKey(KeyCode.Space))
            {
                OnJumpDown();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            OnJump();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            OnBasicAttack();
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow) && !StaticObjects.GetPlayerState().IsCroutching)
        {
            OnMove(Vector3.left, false);
        }
        else if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow) && !StaticObjects.GetPlayerState().IsCroutching)
        {
            OnMove(Vector3.right, true);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow) && !StaticObjects.GetPlayerState().IsAttacking)
        {
            OnFlip(false);
        }
        else if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow) && !StaticObjects.GetPlayerState().IsAttacking)
        {
            OnFlip(true);
        }
        else
        {
            OnStop();
        }

        if (Input.GetKey(KeyCode.S))
        {
            OnThrowAttack();
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            OnUnderwaterControl(false);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            OnEnterPortal();
        }

        if (!Input.GetKey(KeyCode.DownArrow))
        {
            OnStandingUp();
        }
    }

    private void SetUsingArrowControlsScheme(int control)
    {
        _usingArrowControlsScheme = !Convert.ToBoolean(control);
    }
}
