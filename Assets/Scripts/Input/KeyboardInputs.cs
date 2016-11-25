using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

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

    public delegate void KeyboardOnPauseHandler();
    public event KeyboardOnPauseHandler OnPause;

    public delegate void KeyboardOnFlipHandler(bool goesRight);
    public event KeyboardOnFlipHandler OnFlip;

    private PauseMenuAnimationManager _pauseMenuAnimationManager;
    private bool _usingArrowControlsScheme;
    private bool _inMenu;

    private void Start()
    {
        GameObject.Find(StaticObjects.GetFindTags().PauseMenuControlsOptionsButtons).GetComponent<ControlsSchemeSettings>().OnKeyboardControlChanged += SetUsingArrowControlsScheme;
        _pauseMenuAnimationManager = StaticObjects.GetPauseMenuPanel().GetComponent<PauseMenuAnimationManager>();
        _pauseMenuAnimationManager.OnPauseMenuOutOfScreen += IsInMenu;
        _usingArrowControlsScheme = false;
        _inMenu = false;
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

            CheckAllKeysPressed();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnPause();
        }
    }

    private void IsInMenu(bool isActive)
    {
        _inMenu = isActive;
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

        if (!Input.GetKey(KeyCode.DownArrow))
        {
            OnStandingUp();
        }
    }

    private void SetUsingArrowControlsScheme(bool control)
    {
        _usingArrowControlsScheme = !control;
    }
}
