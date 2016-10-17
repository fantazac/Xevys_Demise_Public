﻿using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{

    private InputManager _inputManager;
    private Rigidbody2D _rigidbody;
    private BoxCollider2D _basicAttackBox;
    private InventoryManager _inventoryManager;

    private const float INITIAL_GRAVITY_SCALE = 5;
    private const float INITIAL_WATER_FALLING_SPEED = 3;
    private const float MINIMUM_SPEED_TO_CONTINUE_JUMPING = 3.6f;
    private const float PRECISION_MARGIN = 0.2f;
    private const float GRAVITY_DIVISION_FACTOR_ON_GROUND_UNDERWATER = 0.27f;
    private const float TERMINAL_SPEED = -18;
    private const float SPEED_REDUCTION_WHEN_STOPPING = 0.94f;
    private const float WATER_ACCELERATION_FACTOR = 1.4f;
    private const float LINEAR_DRAG = 30f;
    private const float KNOCKBACK_DURATION = 15;

    private bool _facingRight = true;
    private float _speed = 7;
    private float _jumpingSpeed = 17;
    private bool _feetTouchWater = false;
    private bool _isFloating = false;
    private bool _wearsIronBoots = false;
    private bool _isKnockedBack = false;
    private float _knockbackCount = 0;
    private float _waterYSpeed;
    private Animator _anim;
    private Transform _spriteTransform;
    private bool _canDoubleJump = false;

    public float Speed { get { return _speed; } set { _speed = value; } }
    public float JumpingSpeed { get { return _jumpingSpeed; } set { _jumpingSpeed = value; } }
    public bool FeetTouchWater { get { return _feetTouchWater; } set { _feetTouchWater = value; } }
    public bool IsFloating { get { return _isFloating; } set { _isFloating = value; } }
    public bool FacingRight { get { return _facingRight; } }
    public bool WearsIronBoots { get { return _wearsIronBoots; } }
    public bool IsKnockedBack { get { return _isKnockedBack; } set { _isKnockedBack = value; } }
    public float TerminalSpeed { get { return TERMINAL_SPEED; } }

    private void Start()
    {
        _anim = GameObject.Find("CharacterSprite").GetComponent<Animator>();
        _inventoryManager = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryManager>();
        _spriteTransform = _anim.GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _inputManager = GetComponent<InputManager>();
        _basicAttackBox = GameObject.Find("CharacterBasicAttackBox").GetComponent<BoxCollider2D>();
        _inputManager.OnMove += OnMove;
        _inputManager.OnJump += OnJump;
        _inputManager.OnJumpDown += OnJumpDown;
        _inputManager.OnUnderwaterControl += OnUnderwaterControl;
        _inputManager.OnIronBootsEquip += OnIronBootsEquip;
        _inputManager.OnStop += OnStop;

        _waterYSpeed = INITIAL_WATER_FALLING_SPEED;
    }

    private void OnMove(Vector3 vector, bool goesRight)
    {
        if (!_isKnockedBack)
        {
            _rigidbody.velocity = new Vector2(vector.x * _speed, _rigidbody.velocity.y);
            Flip(goesRight);
        }
    }

    private void OnJump()
    {
        if (!_isKnockedBack)
        {

            if (!IsJumping() && _feetTouchWater && _wearsIronBoots)
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
            else if (IsJumping() && !_feetTouchWater && !_wearsIronBoots && _inventoryManager.FeatherEnabled && _canDoubleJump)
            {
                _canDoubleJump = false;
                ChangePlayerVerticalVelocity(_jumpingSpeed);
            }
        }
    }

    private void OnJumpDown()
    {
        if (!IsJumping() && !_isKnockedBack && GameObject.Find("CharacterTouchesGround").GetComponent<PlayerTouchesFlyingPlatform>().OnFlyingPlatform)
        {
            GameObject.Find("CharacterTouchesGround").GetComponent<PlayerTouchesFlyingPlatform>().DisablePlatformHitbox();
        }
    }

    private void OnUnderwaterControl(bool goesDown)
    {
        if ((goesDown && _wearsIronBoots) || (!goesDown && !_wearsIronBoots))
        {
            _waterYSpeed = INITIAL_WATER_FALLING_SPEED * WATER_ACCELERATION_FACTOR;
        }
        else
        {
            _waterYSpeed = INITIAL_WATER_FALLING_SPEED / WATER_ACCELERATION_FACTOR;
        }
    }

    private void OnIronBootsEquip()
    {
        if (_wearsIronBoots)
        {
            _rigidbody.gravityScale = INITIAL_GRAVITY_SCALE;
        }
        _wearsIronBoots = !_wearsIronBoots;
    }

    private void OnStop()
    {
        if (!_isKnockedBack)
        {
            if (_rigidbody.velocity.x < 1 && _facingRight || _rigidbody.velocity.x > -1 && !_facingRight)
            {
                _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
            }
            else
            {
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x * SPEED_REDUCTION_WHEN_STOPPING, _rigidbody.velocity.y);
                if (_facingRight)
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

    public bool IsJumping()
    {
        if (_feetTouchWater)
        {
            return !(_rigidbody.velocity.y == 0 || GameObject.Find("CharacterTouchesGround").GetComponent<PlayerTouchesGround>().OnGround);
        }
        else
        {
            return !(((GameObject.Find("CharacterTouchesGround").GetComponent<PlayerTouchesGround>().OnGround 
                && !GameObject.Find("CharacterTouchesGround").GetComponent<PlayerTouchesFlyingPlatform>().OnFlyingPlatform)
                || (GameObject.Find("CharacterTouchesGround").GetComponent<PlayerTouchesFlyingPlatform>().OnFlyingPlatform && _rigidbody.velocity.y == 0)
                || _rigidbody.velocity == Vector2.zero)
                && !(_rigidbody.velocity.y > 0));
        }
    }

    private void Update()
    {
        _anim.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal")));
        _anim.SetBool("IsJumping", IsJumping() && _rigidbody.velocity.y > 0);
        _anim.SetBool("IsFalling", IsJumping() && _rigidbody.velocity.y < 0);

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

        if (_wearsIronBoots)
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

    public void Flip(bool goesRight)
    {
        if (goesRight != _facingRight)
        {
            _facingRight = goesRight;
            _basicAttackBox.offset = new Vector2(_basicAttackBox.offset.x * -1, _basicAttackBox.offset.y);
            _spriteTransform.localScale = new Vector3(-1 * _spriteTransform.localScale.x, _spriteTransform.localScale.y, _spriteTransform.localScale.z);
        }
    }

    private void ChangePlayerVerticalVelocity(float verticalVelocity)
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, verticalVelocity);
    }
}
