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

    private bool _feetTouchWater = false;
    private bool _isFloating = false;
    private float _waterYSpeed;

    public bool FeetTouchWater { get { return _feetTouchWater; } set { _feetTouchWater = value; } }
    public bool IsFloating { get { return _isFloating; } set { _isFloating = value; } }

    protected override void OnMove(Vector3 vector, bool goesRight)
    {
        if (!_isKnockedBack)
        {
            if (_feetTouchWater)
            {
                _rigidbody.velocity = new Vector2(vector.x * _speed * SPEED_REDUCTION_FACTOR_IN_WATER, _rigidbody.velocity.y);
            }
            else
            {
                _rigidbody.velocity = new Vector2(vector.x * _speed, _rigidbody.velocity.y);
            }

            Flip(goesRight);
        }
    }

    protected override void OnJump()
    {
        if (!_isKnockedBack)
        {
            if (!IsJumping() && _feetTouchWater && _inventoryManager.IronBootsActive)
            {
                ChangePlayerVerticalVelocity(_jumpingSpeed * WATER_ACCELERATION_FACTOR);
            }
            else if (!IsJumping() && _feetTouchWater)
            {
                ChangePlayerVerticalVelocity(_jumpingSpeed / WATER_ACCELERATION_FACTOR);
                if (_isFloating && _feetTouchWater)
                {
                    _feetTouchWater = false;
                }
            }
            else if (!IsJumping())
            {
                ChangePlayerVerticalVelocity(_jumpingSpeed);
            }
            else if (IsJumping() && !_feetTouchWater && !_inventoryManager.IronBootsActive && _inventoryManager.FeatherEnabled && _canDoubleJump)
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

    protected override void OnUnderwaterControl(bool goesDown)
    {
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
        if (_inventoryManager.IronBootsEnabled)
        {
            _inventoryManager.IronBootsActive = !_inventoryManager.IronBootsActive;
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

    public override bool IsJumping()
    {
        return !(GetComponent<Rigidbody2D>().velocity.y == 0 || GameObject.Find("CharacterTouchesGround").GetComponent<PlayerTouchesGround>().OnGround);
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

        if (_inventoryManager.IronBootsActive)
        {
            if (_feetTouchWater && _isFloating)
            {
                if (_rigidbody.velocity.y > MINIMUM_SPEED_TO_CONTINUE_JUMPING)
                {
                    ChangePlayerVerticalVelocity(_rigidbody.velocity.y - MINIMUM_SPEED_TO_CONTINUE_JUMPING - PRECISION_MARGIN);
                    if (_isFloating && _feetTouchWater)
                    {
                        _feetTouchWater = false;
                    }
                }
            }

            if (_feetTouchWater && _rigidbody.velocity.y < 0)
            {
                _rigidbody.gravityScale = 0;
            }
            else if (_feetTouchWater)
            {
                _rigidbody.gravityScale = INITIAL_GRAVITY_SCALE / GRAVITY_DIVISION_FACTOR_ON_GROUND_UNDERWATER;
            }
            else
            {
                _rigidbody.gravityScale = INITIAL_GRAVITY_SCALE;
            }

            if (_feetTouchWater && _rigidbody.gravityScale == 0)
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
            if (_feetTouchWater)
            {
                _rigidbody.gravityScale = 0;
            }
            else
            {
                _rigidbody.gravityScale = INITIAL_GRAVITY_SCALE;
            }

            if (_feetTouchWater && _isFloating)
            {
                if (_rigidbody.velocity.y > MINIMUM_SPEED_TO_CONTINUE_JUMPING)
                {
                    ChangePlayerVerticalVelocity(_rigidbody.velocity.y - MINIMUM_SPEED_TO_CONTINUE_JUMPING - PRECISION_MARGIN);
                    if (_isFloating && _feetTouchWater)
                    {
                        _feetTouchWater = false;
                    }
                }
                else
                {
                    ChangePlayerVerticalVelocity(0);
                }
            }
            else if (_feetTouchWater && _rigidbody.velocity.y > (_waterYSpeed + (PRECISION_MARGIN * 2)))
            {
                ChangePlayerVerticalVelocity(_rigidbody.velocity.y - (PRECISION_MARGIN * 5));
            }
            else if (_feetTouchWater && _rigidbody.velocity.y < (_waterYSpeed - (PRECISION_MARGIN * 2)))
            {
                ChangePlayerVerticalVelocity(_rigidbody.velocity.y + (PRECISION_MARGIN * 5));
            }
            else if (_feetTouchWater)
            {
                ChangePlayerVerticalVelocity(_waterYSpeed);
            }
        }

        if (!_feetTouchWater && _rigidbody.velocity.y < TERMINAL_SPEED)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, TERMINAL_SPEED);
        }

        _waterYSpeed = INITIAL_WATER_FALLING_SPEED;
    }

}
