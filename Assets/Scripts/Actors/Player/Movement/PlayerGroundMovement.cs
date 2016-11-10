using System;
using UnityEngine;
using System.Collections;

public class PlayerGroundMovement : PlayerMovement
{
    private const float TIME_TO_WAIT_BEFORE_CROUCH_ALLOWED = 0.3f;

    private bool _stoppedEnoughToCrouch = false;

    protected override void OnMove(Vector3 vector, bool goesRight)
    {
        if (!enabled)
        {
            return;
        }
 
        if (!IsKnockedBack && !IsCrouching)
        {
            _stoppedEnoughToCrouch = false;
            _rigidbody.velocity = new Vector2(vector.x * _speed, _rigidbody.velocity.y);
            Flip(goesRight);
        }
    }

    private IEnumerator CountTimeSinceStoppedCoroutine()
    {
        float counter = 0;
        while (counter < TIME_TO_WAIT_BEFORE_CROUCH_ALLOWED)
        {
            counter += Time.deltaTime;
            yield return null;
        }
        _stoppedEnoughToCrouch = true;
    }

    protected override void OnCrouch()
    {
        if (!enabled)
        {
            return;
        }
        if (_rigidbody.velocity == Vector2.zero)
        {
            _anim.SetBool("IsCrouching", true);

            if (_stoppedEnoughToCrouch)
            {
                IsCrouching = true;
                _basicAttackBox.size = new Vector2(_basicAttackBox.size.x, ATTACK_BOX_COLLIDER_Y_WHEN_STAND * CROUCHING_OFFSET);
                _playerBoxCollider.size = new Vector2(_playerBoxCollider.size.x, PLAYER_COLLIDER_BOX_Y_SIZE_WHEN_STAND * CROUCHING_OFFSET);
            }         
        }   
    }

    protected override void OnStandingUp()
    {
        if (!enabled)
        {
            return;
        }
        if (!_anim.GetBool("IsAttacking"))
        {
            IsCrouching = false;
            _basicAttackBox.size = new Vector2(_basicAttackBox.size.x, ATTACK_BOX_COLLIDER_Y_WHEN_STAND);
            _playerBoxCollider.size = new Vector2(_playerBoxCollider.size.x, PLAYER_COLLIDER_BOX_Y_SIZE_WHEN_STAND);
        }       
    }

    protected override void OnJump()
    {
        if (!enabled)
        {
            return;
        }

        if (!IsKnockedBack && !IsCrouching)
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

        if (!_stoppedEnoughToCrouch)
        {
            StartCoroutine("CountTimeSinceStoppedCoroutine");
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
            _showItems.IronBootsSelect();
            _inventoryManager.IronBootsActive = !_inventoryManager.IronBootsActive;
        }
    }

    public override void ChangeGravity()
    {
        if (!enabled)
        {
            return;
        }
        
        if (!_inventoryManager.IronBootsActive)
        {
            _rigidbody.gravityScale = INITIAL_GRAVITY_SCALE;
        }
        else
        {
            _rigidbody.gravityScale = INITIAL_GRAVITY_SCALE * 2;
        }
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
