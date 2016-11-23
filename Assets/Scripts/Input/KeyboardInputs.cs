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

    private bool _usingArrowControlsScheme;

    private void Start()
    {
        GameObject.Find("PauseMenuControlsOptionsButtons").GetComponent<ControlsSchemeSettings>().OnKeyboardControlChanged += SetUsingArrowControlsScheme;
        _usingArrowControlsScheme = false;
    }

    private void Update()
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

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnPause();
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
        else if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.DownArrow))
        {
            OnMove(Vector3.left, false);
        }
        else if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.DownArrow))
        {
            OnMove(Vector3.right, true);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.DownArrow))
        {
            OnFlip(false);
        }
        else if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.DownArrow))
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

    private bool IsPressingDown()
    {
        return Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
    }

    private void SetUsingArrowControlsScheme(bool control)
    {
        _usingArrowControlsScheme = !control;
    }
}
