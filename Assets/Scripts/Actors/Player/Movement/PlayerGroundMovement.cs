using System;
using UnityEngine;
using System.Collections;

public class PlayerGroundMovement : PlayerMovement
{
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
        if (enabled)
        {
            if (!PlayerIsMovingHorizontally())
            {
                _anim.SetBool("IsCrouching", true);

                IsCrouching = true;
                //_basicAttackBox.size = new Vector2(_basicAttackBox.size.x, ATTACK_BOX_COLLIDER_Y_WHEN_STAND * CROUCHING_OFFSET);
                _playerBoxCollider.size = new Vector2(_playerBoxCollider.size.x, _playerBoxCollider.size.y * 0.8f);
            }
        }
    }

    protected override void OnStandingUp()
    {
        if (enabled)
        {
            //if (!_anim.GetBool("IsAttacking"))
            //{
                IsCrouching = false;
                //_basicAttackBox.size = new Vector2(_basicAttackBox.size.x, ATTACK_BOX_COLLIDER_Y_WHEN_STAND);
                _playerBoxCollider.size = new Vector2(_playerBoxCollider.size.x, PLAYER_COLLIDER_BOX_Y_SIZE_WHEN_STAND);
            //}
        }
    }

    private bool PlayerCanDoubleJump()
    {
        return _canDoubleJump && IsJumping() && _inventoryManager.FeatherEnabled;
    }

    protected override void UpdateMovement()
    {
        if (enabled)
        {
            //_rigidbody.gravityScale = INITIAL_GRAVITY_SCALE;

            if (_isKnockedBack && _knockbackCount >= KNOCKBACK_DURATION)
            {
                _isKnockedBack = false;
                _knockbackCount = 0;
            }
            else if (_isKnockedBack)
            {
                _knockbackCount += Time.deltaTime;
            }

            if (!IsJumping() && PlayerTouchesGround() && _inventoryManager.FeatherEnabled && !_canDoubleJump)
            {
                _canDoubleJump = true;
            }

            if (_rigidbody.velocity.y < TERMINAL_SPEED)
            {
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, TERMINAL_SPEED);
            }
        }
    }
}
