using System;
using UnityEngine;
using System.Collections;

public class PlayerGroundMovement : PlayerMovement
{

    protected override void Start()
    {
        base.Start();
        _playerFloating.OnPlayerUnderWater += OnStandingUp;
        _playerFloating.OnPlayerOutOfWater += ActivateDoubleJump;
    }

    protected override void OnMove(Vector3 vector, bool goesRight)
    {
        if (enabled)
        {
            base.OnMove(vector, goesRight);
        }
    }

    protected override void OnJump()
    {
        if (enabled)
        {
            if (!_playerState.IsKnockedBack && !_playerState.IsCroutching)
            {
                if (!IsJumping())
                {
                    ChangePlayerVerticalVelocity(_jumpingSpeed);
                }
                else if (PlayerCanDoubleJump())
                {
                    _canDoubleJump = false;
                    ChangePlayerVerticalVelocity(_jumpingSpeed);
                }
            }
        }
    }

    protected override void OnIronBootsEquip()
    {
        if (enabled)
        {
            if (_inventoryManager.IronBootsEnabled)
            {
                base.OnIronBootsEquip();
                ChangeGravity();
            }
        }
    }

    public override void ChangeGravity()
    {
        if (enabled)
        {
            _rigidbody.gravityScale = _inventoryManager.IronBootsActive ?
                INITIAL_GRAVITY_SCALE * 2 : INITIAL_GRAVITY_SCALE;
        }
    }

    protected override void OnCrouch()
    {
        if (PlayerCanCroutch() && enabled)
        {
            if (PlayerIsMovingHorizontally())
            {
                _rigidbody.velocity = Vector2.zero;
            }
            SetCroutch(true);
            transform.position += Vector3.down * CROUTCH_Y_OFFSET;
        }
    }

    private void ActivateDoubleJump()
    {
        _canDoubleJump = true;
    }

    private bool PlayerCanDoubleJump()
    {
        return _canDoubleJump && IsJumping() && _inventoryManager.FeatherEnabled;
    }

    private bool PlayerCanCroutch()
    {
        return !_playerState.IsCroutching && !_playerState.IsKnockedBack && !PlayerIsMovingVertically();
    }

    protected override void UpdateMovement()
    {
        if (enabled)
        {
            if (PlayerIsFalling())
            {
                _wasFalling = true;
                OnPlayerFalling();
            }
            else if (_wasFalling)
            {
                _wasFalling = false;
                OnPlayerLanding();
            }

            if (!IsJumping() && PlayerTouchesGround() && _inventoryManager.FeatherEnabled && !_canDoubleJump)
            {
                ActivateDoubleJump();
            }

            if (_rigidbody.velocity.y < TERMINAL_SPEED)
            {
                _rigidbody.velocity += Vector2.up * (TERMINAL_SPEED - _rigidbody.velocity.y);
            }
        }
    }
}
