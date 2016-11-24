using UnityEngine;
using System.Collections;

public class PlayerWaterMovement : PlayerMovement
{

    private const float INITIAL_WATER_FALLING_SPEED = 3;
    private const float MINIMUM_SPEED_TO_CONTINUE_JUMPING = 3.6f;
    private const float PRECISION_MARGIN = 0.2f;
    private const float GRAVITY_DIVISION_FACTOR_ON_GROUND_UNDERWATER = 0.27f;
    private const float WATER_ACCELERATION_FACTOR = 1.4f;
    private const float SPEED_REDUCTION_FACTOR_IN_WATER = 0.3f;
    private const float WATER_DECELARATION = 95f;

    private bool _isFloating = false;
    private float _waterYSpeed;

    private AudioReverbZone _audioReverbZone;

    public bool IsFloating { get { return _isFloating; } set { _isFloating = value; } }

    protected override void Start()
    {
        base.Start();
        _playerHealth.OnDeath += ExitWater;
        _audioReverbZone = GetComponent<AudioReverbZone>();
    }

    protected override void OnMove(Vector3 vector, bool goesRight)
    {
        if (enabled)
        {
            base.OnMove(new Vector2(vector.x * SPEED_REDUCTION_FACTOR_IN_WATER, vector.y), goesRight);
        }
    }

    protected override void OnJump()
    {
        if (enabled)
        {
            if (!_playerState.IsKnockedBack)
            {
                if (!IsJumping())
                {
                    if (_inventoryManager.IronBootsActive)
                    {
                        ChangePlayerVerticalVelocity(_jumpingSpeed * WATER_ACCELERATION_FACTOR);
                    }
                    else if(_isFloating)
                    {
                        ChangePlayerVerticalVelocity(_jumpingSpeed / (WATER_ACCELERATION_FACTOR - (PRECISION_MARGIN * WATER_ACCELERATION_FACTOR)));
                        ExitWater();
                    }
                }
            }
        }
    }

    private void ExitWater()
    {
        _isFloating = false;
        _playerState.DisableFloating();

        _playerGroundMovement.enabled = true;
        _playerGroundMovement.ChangeGravity();

        enabled = false;
    }

    protected override void OnCrouch()
    {
        if (!_playerState.IsCroutching && !_playerState.IsKnockedBack && enabled)
        {
            if (PlayerTouchesGround() && _inventoryManager.IronBootsActive)
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
        if (enabled && _inventoryManager.IronBootsEnabled)
        {
            if (_inventoryManager.IronBootsActive)
            {
                OnStandingUp();
            }
            base.OnIronBootsEquip();
        }
    }

    public override bool IsJumping()
    {
        return !(_isFloating || _playerTouchesGround.OnGround);
    }

    protected override void UpdateMovement()
    {
        if (enabled)
        {
            if (_inventoryManager.IronBootsActive)
            {
                _rigidbody.gravityScale = _rigidbody.velocity.y <= 0 ?
                    0 : INITIAL_GRAVITY_SCALE / GRAVITY_DIVISION_FACTOR_ON_GROUND_UNDERWATER;
            
                if (_rigidbody.gravityScale == 0)
                {
                    if (_rigidbody.velocity.y > (-_waterYSpeed + PRECISION_MARGIN))
                    {
                        ChangePlayerVerticalVelocity(_rigidbody.velocity.y - (WATER_DECELARATION * Time.deltaTime));
                    }
                    else if (_rigidbody.velocity.y < (-_waterYSpeed - PRECISION_MARGIN))
                    {
                        ChangePlayerVerticalVelocity(_rigidbody.velocity.y + (WATER_DECELARATION * Time.deltaTime));
                    }
                    else
                    {
                        ChangePlayerVerticalVelocity(-_waterYSpeed);
                    }
                }
            }
            else if (_isFloating)
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
                ChangePlayerVerticalVelocity(_rigidbody.velocity.y - (WATER_DECELARATION * Time.deltaTime));
            }
            else if (_rigidbody.velocity.y < (_waterYSpeed - (PRECISION_MARGIN * 2)))
            {
                ChangePlayerVerticalVelocity(_rigidbody.velocity.y + (WATER_DECELARATION * Time.deltaTime));
            }
            else if(_rigidbody.velocity.y != _waterYSpeed)
            {
                ChangePlayerVerticalVelocity(_waterYSpeed);
            }

            if(_rigidbody.gravityScale != 0 && !_inventoryManager.IronBootsActive)
            {
                _rigidbody.gravityScale = 0;
            }

            if(_waterYSpeed != INITIAL_WATER_FALLING_SPEED)
            {
                _waterYSpeed = INITIAL_WATER_FALLING_SPEED;
            }
        }
    }

    private void SetReverbZoneState(bool enable)
    {
        _audioReverbZone.enabled = enable;
    }
}
