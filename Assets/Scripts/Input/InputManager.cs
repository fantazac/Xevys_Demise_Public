﻿using UnityEngine;

public class InputManager : MonoBehaviour
{
    public delegate void OnMoveHandler(Vector3 movement, bool goesRight);

    public delegate void OnJumpHandler();

    public delegate void OnUnderwaterControlHandler(bool goesDown);

    public delegate void OnBootsEquipHandler();

    public delegate void OnBootsUnequipHandler();

    public delegate void OnStopHandler();

    public delegate void OnAttackHandler();

    public event OnMoveHandler OnMove;
    public event OnJumpHandler OnJump;
    public event OnUnderwaterControlHandler OnUnderwaterControl;
    public event OnBootsEquipHandler OnBootsEquip;
    public event OnBootsUnequipHandler OnBootsUnequip;
    public event OnStopHandler OnStop;
    public event OnAttackHandler OnAttack;

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
            OnMove(Vector3.left, false);
        else if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
            OnMove(Vector3.right, true);
        else
            OnStop();

        if (Input.GetKey(KeyCode.Space))
            OnJump();

        if (Input.GetKey(KeyCode.DownArrow))
            OnUnderwaterControl(true);

        if (Input.GetKey(KeyCode.UpArrow))
            OnUnderwaterControl(false);

        if (Input.GetKey(KeyCode.E))
            OnBootsEquip();
        else if (Input.GetKey(KeyCode.U))
            OnBootsUnequip();

        if (Input.GetKey(KeyCode.K))      
            OnAttack();
        
    }
}