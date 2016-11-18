using System;
using UnityEngine;
using System.Collections;

public class PlayerGroundMovement : PlayerMovement
{

    private float CROUTCH_Y_OFFSET = 0.36f;

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
            if (!IsKnockedBack && !IsCrouching)
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
        if (!IsCrouching && enabled)
        {
            if (!PlayerIsMovingVertically())
            {
                if (PlayerIsMovingHorizontally())
                {
                    _rigidbody.velocity = Vector2.zero;
                }
                SetCroutch(true);
                transform.position += Vector3.down * CROUTCH_Y_OFFSET;
            }
        }
    }

    protected override void OnStandingUp()
    {
        if (IsCrouching && enabled)
        {
            if (!PlayerIsMovingVertically())
            {
                transform.position += Vector3.up * CROUTCH_Y_OFFSET;
            }
            SetCroutch(false);
        }
    }

    private void ActivateDoubleJump()
    {
        _canDoubleJump = true;
    }

    private void SetCroutch(bool enable)
    {
        IsCrouching = enable;
        _playerCroutchHitbox.enabled = enable;
        _playerBoxCollider.isTrigger = enable;
        PlayerState.SetCroutching(enable);
    }

    private bool PlayerCanDoubleJump()
    {
        return _canDoubleJump && IsJumping() && _inventoryManager.FeatherEnabled;
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
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, TERMINAL_SPEED);
            }
        }
    }
}
