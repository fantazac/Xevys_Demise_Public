using UnityEngine;
using System.Collections;

public class PlayerWaterMovement : PlayerMovement
{

    private const float INITIAL_WATER_FALLING_SPEED = 3;
    private const float MINIMUM_SPEED_TO_CONTINUE_JUMPING = 3.6f;
    private const float PRECISION_MARGIN = 0.2f;
    private const float GRAVITY_DIVISION_FACTOR_ON_GROUND_UNDERWATER = 0.27f;
    private const float WATER_ACCELERATION_FACTOR = 1.4f;
    private const float SPEED_REDUCTION_FACTOR_IN_WATER = 0.4f;

    private bool _isFloating = false;
    private float _waterYSpeed;

    public bool IsFloating { get { return _isFloating; } set { _isFloating = value; } }

    protected override void OnMove(Vector3 vector, bool goesRight)
    {
        if (!enabled)
        {
            return;
        }

        if (!_isKnockedBack)
        {
            _rigidbody.velocity = new Vector2(vector.x * _speed * SPEED_REDUCTION_FACTOR_IN_WATER, _rigidbody.velocity.y);
            Flip(goesRight);
        }
    }

    protected override void OnJump()
    {
        if (!enabled)
        {
            return;
        }

        if (!_isKnockedBack)
        {
            if (!IsJumping() && _inventoryManager.IronBootsActive)
            {
                _isFloating = false;

                ChangePlayerVerticalVelocity(_jumpingSpeed * WATER_ACCELERATION_FACTOR);
                ChangeGravity();
            }
            else if (!IsJumping())
            {
                _isFloating = false;

                ChangePlayerVerticalVelocity(_jumpingSpeed / (WATER_ACCELERATION_FACTOR - (PRECISION_MARGIN * WATER_ACCELERATION_FACTOR)));
                ChangeGravity();
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

    protected override void OnUnderwaterControl(bool goesDown)
    {
        if (!enabled)
        {
            return;
        }

        if ((goesDown && _inventoryManager.IronBootsActive) || (!goesDown && !_inventoryManager.IronBootsActive))
        {
            _waterYSpeed = INITIAL_WATER_FALLING_SPEED * WATER_ACCELERATION_FACTOR;
        }
        else
        {
            _waterYSpeed = INITIAL_WATER_FALLING_SPEED / WATER_ACCELERATION_FACTOR;
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

    public override bool IsJumping()
    {
        return !((GetComponent<Rigidbody2D>().velocity.y == 0 && _isFloating) || GameObject.Find("CharacterTouchesGround").GetComponent<PlayerTouchesGround>().OnGround);
    }

    protected override void UpdateMovement()
    {

        if (!enabled)
        {
            return;
        }

        if (_isKnockedBack && _knockbackCount == KNOCKBACK_DURATION)
        {
            _isKnockedBack = false;
            _knockbackCount = 0;
        }
        else if (_isKnockedBack)
        {
            _knockbackCount++;
        }

        if (_inventoryManager.IronBootsActive)
        {
            if (_rigidbody.velocity.y <= 0)
            {
                _rigidbody.gravityScale = 0;
            }
            else
            {
                _rigidbody.gravityScale = INITIAL_GRAVITY_SCALE / GRAVITY_DIVISION_FACTOR_ON_GROUND_UNDERWATER;
            }

            if (_rigidbody.gravityScale == 0)
            {
                if (_rigidbody.velocity.y > (-_waterYSpeed + PRECISION_MARGIN))
                {
                    ChangePlayerVerticalVelocity(_rigidbody.velocity.y - PRECISION_MARGIN);
                }
                else if (_rigidbody.velocity.y < (-_waterYSpeed - PRECISION_MARGIN))
                {
                    ChangePlayerVerticalVelocity(_rigidbody.velocity.y + PRECISION_MARGIN);
                }
                else
                {
                    ChangePlayerVerticalVelocity(_rigidbody.velocity.y);
                }
            }
        }
        else
        {
            _rigidbody.gravityScale = 0;

            if (_isFloating)
            {
                if (_rigidbody.velocity.y > MINIMUM_SPEED_TO_CONTINUE_JUMPING)
                {
                    ChangePlayerVerticalVelocity(_rigidbody.velocity.y - MINIMUM_SPEED_TO_CONTINUE_JUMPING - PRECISION_MARGIN);
                }
                else
                {
                    ChangePlayerVerticalVelocity(0);
                }
            }
            else if (_rigidbody.velocity.y > (_waterYSpeed + (PRECISION_MARGIN * 2)))
            {
                ChangePlayerVerticalVelocity(_rigidbody.velocity.y - (PRECISION_MARGIN * 5 * Time.deltaTime * 60));
            }
            else if (_rigidbody.velocity.y < (_waterYSpeed - (PRECISION_MARGIN * 2)))
            {
                ChangePlayerVerticalVelocity(_rigidbody.velocity.y + (PRECISION_MARGIN * 5 * Time.deltaTime * 60));
            }
            else
            {
                ChangePlayerVerticalVelocity(_waterYSpeed);
            }
        }

        _waterYSpeed = INITIAL_WATER_FALLING_SPEED;
    }

}
