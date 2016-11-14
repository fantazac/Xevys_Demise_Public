using UnityEngine;
using System.Collections;

public class KeyboardInputs : MonoBehaviour {

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

    private Health _playerHealth;

    private void Start()
    {
        _playerHealth = StaticObjects.GetPlayer().GetComponent<Health>();
        _playerHealth.OnDeath += OnDeath;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            OnBasicAttack();
        }
        else if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.S))
        {
            OnMove(Vector3.left, false);
        }
        else if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S))
        {
            OnMove(Vector3.right, true);
        }
        else
        {
            OnStop();
        }

        if (Input.GetKey(KeyCode.S))
        {
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

        if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            OnCrouch();
        }
        else
        {
            OnStandingUp();
        }

        if (Input.GetKey(KeyCode.W))
        {
            OnUnderwaterControl(false);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            OnIronBootsEquip();
        }

        if (Input.GetKey(KeyCode.L))
        {
            OnThrowAttack();
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

    private void OnDeath()
    {
        enabled = false;
    }
}
