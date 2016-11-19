using UnityEngine;
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
    protected ShowItems _showItems;
    protected Health _playerHealth;
    protected PlayerBasicAttack _playerBasicAttack;
    protected PlayerTouchesFlyingPlatform _playerTouchesFlyingPlatform;

    protected PlayerOrientation _playerOrientation;
    protected PlayerFloatingInteraction _playerFloating;
    protected PlayerState _state;

    protected PlayerGroundMovement _playerGroundMovement;

    public delegate void OnFallingHandler();
    public event OnFallingHandler OnFalling;
    public delegate void OnLandingHandler();
    public event OnLandingHandler OnLanding;
    public delegate void OnPlayerFlippedHandler();
    public event OnPlayerFlippedHandler OnPlayerFlipped;

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

    public bool IsKnockedBack { get { return _isKnockedBack; } set { _isKnockedBack = value; } }

    protected virtual void Start()
    {
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
        _playerOrientation = GetComponent<PlayerOrientation>();
        _playerCroutchHitbox = GameObject.Find("CharacterCroutchedHitbox").GetComponent<BoxCollider2D>();
        _playerFloating = GameObject.Find("CharacterFloatingHitbox").GetComponent<PlayerFloatingInteraction>();
        _state = GetComponent<PlayerState>();

        _playerGroundMovement = GetComponent<PlayerGroundMovement>();

        _inputManager.OnMove += OnMove;
        _inputManager.OnJump += OnJump;
        _inputManager.OnJumpDown += OnJumpDown;
        _inputManager.OnUnderwaterControl += OnUnderwaterControl;
        _inputManager.OnIronBootsEquip += OnIronBootsEquip;
        _inputManager.OnStop += OnStop;
        _inputManager.OnCrouch += OnCrouch;
        _inputManager.OnStandingUp += OnStandingUp;

        _rigidbody.gravityScale = INITIAL_GRAVITY_SCALE;
    }

    protected virtual void OnMove(Vector3 vector, bool goesRight)
    {
        if (PlayerCanMove())
        {
            MovePlayer(vector, goesRight);
            PlayerState.SetMoving(_rigidbody.velocity.x);
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
            PlayerState.SetImmobile();
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
        return !IsKnockedBack && !PlayerState.IsCroutching && !PlayerState.IsAttacking;
    }

    private bool PlayerIsAlmostStopped()
    {
        return PlayerIsAlmostStoppedAndIsFacingRight() || PlayerIsAlmostStoppedAndIsFacingLeft();
    }

    private bool PlayerIsAlmostStoppedAndIsFacingRight()
    {
        return _rigidbody.velocity.x < 1 && _playerOrientation.IsFacingRight;
    }

    private bool PlayerIsAlmostStoppedAndIsFacingLeft()
    {
        return _rigidbody.velocity.x > -1 && !_playerOrientation.IsFacingRight;
    }

    protected bool PlayerIsFalling()
    {
        return IsJumping() && _rigidbody.velocity.y <= 0;
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

    private void Update()
    {
        if (PlayerIsJumping() != PlayerState.IsJumping)
        {
            PlayerState.SetJumping(PlayerIsJumping());
        }
        else if (PlayerIsFalling() != PlayerState.IsFalling)
        {
            PlayerState.SetFalling(PlayerIsFalling());
        }

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
        if (GetComponent<PlayerOrientation>().Flip(goesRight))
        {
            _basicAttackBox.offset = new Vector2(_basicAttackBox.offset.x * -1, _basicAttackBox.offset.y);
            OnPlayerFlipped();
        }
    }

    protected void ChangePlayerVerticalVelocity(float verticalVelocity)
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, verticalVelocity);
    }
}
