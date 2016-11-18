﻿using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    protected InputManager _inputManager;
    protected Rigidbody2D _rigidbody;
    protected BoxCollider2D _basicAttackBox;
    protected BoxCollider2D _playerBoxCollider;
    protected BoxCollider2D _playerCroutchHitbox;
    protected SpriteRenderer _playerSpriteRenderer;
    protected InventoryManager _inventoryManager;
    protected PlayerTouchesGround _playerTouchesGround;
    protected Animator _anim;
    protected ShowItems _showItems;
    protected Health _playerHealth;
    protected PlayerBasicAttack _playerBasicAttack;
    protected PlayerTouchesFlyingPlatform _playerTouchesFlyingPlatform;
    protected FlipPlayer _flipPlayer;
    protected PlayerFloatingInteraction _playerFloating;

    public delegate void OnFallingHandler();
    public event OnFallingHandler OnFalling;
    public delegate void OnLandingHandler();
    public event OnLandingHandler OnLanding;

    protected const float INITIAL_GRAVITY_SCALE = 5;
    protected const float TERMINAL_SPEED = -18;
    protected const float LINEAR_DRAG = 18f;
    protected const float KNOCKBACK_DURATION = 0.25f;

    protected float _horizontalSpeed = 7;
    protected float _jumpingSpeed = 17;
    protected bool _isKnockedBack = false;
    protected float _knockbackCount = 0;
    protected bool _canDoubleJump = false;
    protected bool _wasFalling = false;
    protected bool _isCrouching = false;

    public float Speed { get { return _horizontalSpeed; } set { _horizontalSpeed = value; } }
    public float JumpingSpeed { get { return _jumpingSpeed; } set { _jumpingSpeed = value; } }
    public bool IsKnockedBack { get { return _isKnockedBack; } set { _isKnockedBack = value; } }
    public bool IsCrouching { get { return _isCrouching; } set { _isCrouching = value; } }
    public float TerminalSpeed { get { return TERMINAL_SPEED; } }

    protected virtual void Start()
    {
        _anim = StaticObjects.GetPlayer().GetComponentInChildren<Animator>();
        _playerTouchesFlyingPlatform = GetComponentInChildren<PlayerTouchesFlyingPlatform>();
        _playerSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _inventoryManager = GetComponent<InventoryManager>();
        _playerBoxCollider = GetComponent<BoxCollider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _inputManager = GetComponentInChildren<InputManager>();
        _basicAttackBox = GameObject.Find("CharacterBasicAttackBox").GetComponent<BoxCollider2D>();
        _showItems = StaticObjects.GetItemCanvas().GetComponent<ShowItems>();
        _playerHealth = GetComponent<Health>();
        _playerTouchesGround = GetComponentInChildren<PlayerTouchesGround>();
        _playerBasicAttack = GetComponent<PlayerBasicAttack>();
        _flipPlayer = GetComponent<FlipPlayer>();
        _playerCroutchHitbox = GameObject.Find("CharacterCroutchedHitbox").GetComponent<BoxCollider2D>();
        _playerFloating = GameObject.Find("CharacterFloatingHitbox").GetComponent<PlayerFloatingInteraction>();

        _inputManager.OnMove += OnMove;
        _inputManager.OnJump += OnJump;
        _inputManager.OnJumpDown += OnJumpDown;
        _inputManager.OnUnderwaterControl += OnUnderwaterControl;
        _inputManager.OnIronBootsEquip += OnIronBootsEquip;
        _inputManager.OnStop += OnStop;
        _inputManager.OnCrouch += OnCrouch;
        _inputManager.OnStandingUp += OnStandingUp;

        _playerHealth.OnDeath += OnDeath;

        _rigidbody.gravityScale = INITIAL_GRAVITY_SCALE;
    }

    protected virtual void OnMove(Vector3 vector, bool goesRight)
    {
        if (PlayerCanMove())
        {
            MovePlayer(vector, goesRight);
        }
        else
        {
            OnStop();
        }
    }

    protected virtual void OnJumpDown()
    {
        if (PlayerCanDropFromFlyingPlatform())
        {
            _playerTouchesFlyingPlatform.DropFromPlatform();
        }
    }

    protected virtual void OnStop()
    {
        if (PlayerIsMovingHorizontally() && !_isKnockedBack)
        {
            _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
        }
    }

    protected virtual void OnIronBootsEquip()
    {
        _showItems.IronBootsSelect();
        _inventoryManager.IronBootsActive = !_inventoryManager.IronBootsActive;
    }

    protected virtual void OnJump() { }

    protected virtual void OnUnderwaterControl(bool goesDown) { }

    protected virtual void UpdateMovement() { }

    public virtual void ChangeGravity() { }

    protected virtual void OnCrouch() { }

    protected virtual void OnStandingUp() { }

    public virtual bool IsJumping()
    {
        return !((PlayerIsOnGround() || VerticalVelocityIsZeroOnFlyingPlatform() || PlayerIsImmobile())
            && !VerticalVelocityIsPositive());
    }

    private bool VerticalVelocityIsPositive()
    {
        return _rigidbody.velocity.y > 0;
    }

    private bool VerticalVelocityIsZeroOnFlyingPlatform()
    {
        return _playerTouchesFlyingPlatform.OnFlyingPlatform && _rigidbody.velocity.y == 0;
    }

    protected bool PlayerIsImmobile()
    {
        return _rigidbody.velocity == Vector2.zero;
    }

    protected bool PlayerIsMovingHorizontally()
    {
        return _rigidbody.velocity.x != 0;
    }

    protected bool PlayerIsMovingVertically()
    {
        return _rigidbody.velocity.y != 0;
    }

    private bool PlayerIsOnGround()
    {
        return PlayerTouchesGround() && !PlayerTouchesFlyingPlatform();
    }

    protected bool PlayerTouchesGround()
    {
        return _playerTouchesGround.OnGround;
    }

    private bool PlayerTouchesFlyingPlatform()
    {
        return _playerTouchesFlyingPlatform.OnFlyingPlatform;
    }

    private bool PlayerCanDropFromFlyingPlatform()
    {
        return !IsJumping() && !_isKnockedBack && PlayerTouchesFlyingPlatform();
    }

    private bool PlayerCanMove()
    {
        return !IsKnockedBack && !IsCrouching && !_playerBasicAttack.IsAttacking();
    }

    private bool PlayerIsAlmostStopped()
    {
        return PlayerIsAlmostStoppedAndIsFacingRight() || PlayerIsAlmostStoppedAndIsFacingLeft();
    }

    private bool PlayerIsAlmostStoppedAndIsFacingRight()
    {
        return _rigidbody.velocity.x < 1 && _flipPlayer.IsFacingRight;
    }

    private bool PlayerIsAlmostStoppedAndIsFacingLeft()
    {
        return _rigidbody.velocity.x > -1 && !_flipPlayer.IsFacingRight;
    }

    protected bool PlayerIsFalling()
    {
        return IsJumping() && _rigidbody.velocity.y < 0;
    }

    protected bool PlayerIsJumping()
    {
        return IsJumping() && _rigidbody.velocity.y > 0;
    }

    protected void OnPlayerFalling()
    {
        OnFalling();
    }

    protected void OnPlayerLanding()
    {
        OnLanding();
    }

    private void MovePlayer(Vector3 vector, bool goesRight)
    {
        _rigidbody.velocity = new Vector2(vector.x * _horizontalSpeed, _rigidbody.velocity.y);
        Flip(goesRight);
    }

    // À modifier absolument
    private void Update()
    {
        //demander à ben si cest mieux un Abs ou un if negatif -> mettre positif
        _anim.SetFloat("Speed", Mathf.Abs(_rigidbody.velocity.x));
        _anim.SetBool("IsJumping", PlayerIsJumping());
        _anim.SetBool("IsFalling", PlayerIsFalling());

        if (_isKnockedBack && _knockbackCount >= KNOCKBACK_DURATION)
        {
            _isKnockedBack = false;
            _knockbackCount = 0;
        }
        else if (_isKnockedBack)
        {
            _knockbackCount += Time.deltaTime;
        }

        UpdateMovement();
    }

    protected void Flip(bool goesRight)
    {
        if (GetComponent<FlipPlayer>().Flip(goesRight))
        {
            _basicAttackBox.offset = new Vector2(_basicAttackBox.offset.x * -1, _basicAttackBox.offset.y);
            _anim.transform.localScale = new Vector3(-1 * _anim.transform.localScale.x,
                _anim.transform.localScale.y, _anim.transform.localScale.z);
        }
    }

    protected void ChangePlayerVerticalVelocity(float verticalVelocity)
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, verticalVelocity);
    }

    private void OnDeath()
    {
        _anim.SetBool("IsDamaged", true);
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
}
