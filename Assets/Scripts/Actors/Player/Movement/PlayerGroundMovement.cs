using UnityEngine;
using System.Collections;

public class PlayerGroundMovement : PlayerMovement
{

    protected override void OnMove(Vector3 vector, bool goesRight)
    {
        if (!_isKnockedBack)
        {
            _rigidbody.velocity = new Vector2(vector.x * _speed, _rigidbody.velocity.y);
            Flip(goesRight);
        }
    }

    protected override void OnJump()
    {
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
        if (!IsJumping() && !_isKnockedBack && GameObject.Find("CharacterTouchesGround").GetComponent<PlayerTouchesFlyingPlatform>().OnFlyingPlatform)
        {
            GameObject.Find("CharacterTouchesGround").GetComponent<PlayerTouchesFlyingPlatform>().DisablePlatformHitbox();
        }
    }

    protected override void OnStop()
    {
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
        if (_inventoryManager.IronBootsEnabled)
        {
            if (_inventoryManager.IronBootsActive)
            {
                _rigidbody.gravityScale = INITIAL_GRAVITY_SCALE;
            }
            _inventoryManager.IronBootsActive = !_inventoryManager.IronBootsActive;
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
