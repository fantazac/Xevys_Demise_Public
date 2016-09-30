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
    public event OnBootsEquipHandler OnBootsEquip;

    public delegate void OnBootsUnequipHandler();
    public event OnBootsUnequipHandler OnBootsUnequip;

    public delegate void OnStopHandler();
    public event OnStopHandler OnStop;

    public delegate void OnBasicAttackHandler();
    public event OnBasicAttackHandler OnBasicAttack;

    public delegate void OnThrowAttackHandler();
    public event OnThrowAttackHandler OnThrowAttack;

    public delegate void OnThrowAttackChangedHandler();
    public event OnThrowAttackChangedHandler OnThrowAttackChanged;

    private float joysticksXAxisDeadZone = 0.1f;
    private float joysticksYAxisDeadZone = 1f;

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
            OnMove(Vector3.left, false);
        else if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
            OnMove(Vector3.right, true);
        else
            OnStop();

        if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.Space))
            OnJumpDown();
        else if (Input.GetKey(KeyCode.Space))
            OnJump();

        if (Input.GetKey(KeyCode.DownArrow))
            OnUnderwaterControl(true);

        if (Input.GetKey(KeyCode.UpArrow))
            OnUnderwaterControl(false);

        if (Input.GetKey(KeyCode.E))
            OnBootsEquip();
        else if (Input.GetKey(KeyCode.U))
            OnBootsUnequip();

        if (Input.GetKeyDown(KeyCode.K))
            OnBasicAttack();

        if (Input.GetKey(KeyCode.L))
            OnThrowAttack();

        if (Input.GetKeyDown(KeyCode.Q))
            OnThrowAttackChanged();

        GamePadInputs();
    }

    private void GamePadInputs()
    {
        foreach (PlayerIndex player in Enum.GetValues(typeof(PlayerIndex)))
        {
            //Obtention de l'état du gamepad
            GamePadState state = GamePad.GetState(player);

            //Tester si la manette est connectée
            if (state.IsConnected)
            {
                //Déplacement gauche à droite du joueur (utilisez les events)
                if (Math.Abs(state.ThumbSticks.Left.X) > joysticksXAxisDeadZone)
                {
                    if (state.ThumbSticks.Left.X < 0)
                        OnMove(Vector3.left, false);
                    else
                        OnMove(Vector3.right, true);
                }

                if (Math.Abs(state.ThumbSticks.Left.Y) == joysticksYAxisDeadZone)
                {
                    if (state.Buttons.A == ButtonState.Pressed && state.ThumbSticks.Left.Y < 0)
                        OnJumpDown();

                    if (state.ThumbSticks.Left.Y < 0)
                        OnUnderwaterControl(true);

                    if (state.ThumbSticks.Left.Y > 0)
                        OnUnderwaterControl(false);
                }

                if (state.Buttons.A == ButtonState.Pressed)
                    OnJump();

                if (state.Buttons.RightStick == ButtonState.Pressed)
                    OnBootsEquip();
                else if (GameObject.Find("Character").GetComponent<PlayerMovement>().WearsBoots)
                    OnBootsUnequip();

                if (state.Buttons.X == ButtonState.Pressed)
                    OnBasicAttack();

                if (state.Buttons.B == ButtonState.Pressed)
                    OnThrowAttack();

                //Faire vibrer le gamepad en fonction des triggers
                //GamePad.SetVibration(player, state.Triggers.Left, state.Triggers.Right);
            }
        }
    }
}
