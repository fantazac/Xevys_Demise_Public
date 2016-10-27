using UnityEngine;
using System.Collections;

public class PlayerGroundMovement : PlayerMovement
{

    protected override void OnMove(Vector3 vector, bool goesRight)
    {
        if (!enabled)
        {
            return;
        }

        if (!_isKnockedBack && !_isCrouching)
        {
            _rigidbody.velocity = new Vector2(vector.x * _speed, _rigidbody.velocity.y);
            Flip(goesRight);
        }
    }

    protected override void OnCrouch()
    {
        if (!enabled)
        {
            return;
        }      
        IsCrouching = true;
    }

    protected override void OnStandingUp()
    {
        if (!enabled)
        {
            return;
        }
        IsCrouching = false;
    }

    protected override void OnJump()
    {
        if (!enabled)
        {
            return;
        }

        if (!_isKnockedBack)
        {
            if (!IsJumping())
            {
                ChangePlayerVerticalVelocity(_jumpingSpeed);
            }
            else if (IsJumping() && _inventoryManager.FeatherEnabled && _canDoubleJump)
            {
                _canDoubleJump = false;
                ChangePlayerVerticalVelocity(_jumpingSpeed);
            }
        }
    }

    protected override void OnJumpDown()
    {
        if (!enabled)
        {
            return;
        }

        if (!IsJumping() && !_isKnockedBack && GameObject.Find("CharacterTouchesGround").GetComponent<PlayerTouchesFlyingPlatform>().OnFlyingPlatform)
        {
            GameObject.Find("CharacterTouchesGround").GetComponent<PlayerTouchesFlyingPlatform>().DisablePlatformHitbox();
        }
    }

    protected override void OnStop()
    {
        if (!enabled)
        {
            return;
        }

        if (!_isKnockedBack)
        {
            if (_rigidbody.velocity.x < 1 && GetComponent<FlipPlayer>().IsFacingRight || _rigidbody.velocity.x > -1 && !GetComponent<FlipPlayer>().IsFacingRight)
            {
                _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
            }
            else
            {
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x * SPEED_REDUCTION_WHEN_STOPPING, _rigidbody.velocity.y);
                if (GetComponent<FlipPlayer>().IsFacingRight)
                {
                    _rigidbody.AddForce(new Vector2(-LINEAR_DRAG, 0));
                }
                else
                {
                    _rigidbody.AddForce(new Vector2(LINEAR_DRAG, 0));
                }
            }
        }
    }

    protected override void OnIronBootsEquip()
    {
        if (!enabled)
        {
            return;
        }

        if (_inventoryManager.IronBootsEnabled)
        {
            ChangeGravity();
            _showItems.OnIronBootsSelected();
            _inventoryManager.IronBootsActive = !_inventoryManager.IronBootsActive;
        }
    }

    public override void ChangeGravity()
    {
        if (!enabled)
        {
            return;
        }

        if (_inventoryManager.IronBootsActive)
        {
            _rigidbody.gravityScale = INITIAL_GRAVITY_SCALE;
        }
        else
        {
            _rigidbody.gravityScale = INITIAL_GRAVITY_SCALE * 2;
        }
    }

    public override bool IsJumping()
    {
        return !(((GameObject.Find("CharacterTouchesGround").GetComponent<PlayerTouchesGround>().OnGround
                && !GameObject.Find("CharacterTouchesGround").GetComponent<PlayerTouchesFlyingPlatform>().OnFlyingPlatform)
                || (GameObject.Find("CharacterTouchesGround").GetComponent<PlayerTouchesFlyingPlatform>().OnFlyingPlatform && _rigidbody.velocity.y == 0)
                || _rigidbody.velocity == Vector2.zero)
                && !(_rigidbody.velocity.y > 0));
    }

    protected override void UpdateMovement()
    {
        if (!enabled)
        {
            return;
        }

        _rigidbody.gravityScale = INITIAL_GRAVITY_SCALE;

        if (_isKnockedBack && _knockbackCount == KNOCKBACK_DURATION)
        {
            _isKnockedBack = false;
            _knockbackCount = 0;
        }
        else if (_isKnockedBack)
        {
            _knockbackCount++;
        }

        if (!IsJumping() && _inventoryManager.FeatherEnabled && !_canDoubleJump)
        {
            _canDoubleJump = true;
        }

        if (_rigidbody.velocity.y < TERMINAL_SPEED)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, TERMINAL_SPEED);
        }

    }

}
