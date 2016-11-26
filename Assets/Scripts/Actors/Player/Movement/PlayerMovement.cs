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
    protected PlayerState _playerState;

    protected ActorOrientation _orientation;
    protected PlayerFloatingInteraction _playerFloating;

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
    protected const float CROUTCH_Y_OFFSET = 0.36f;

    protected float _horizontalSpeed = 7;
    protected float _jumpingSpeed = 17;
    protected bool _canDoubleJump = false;
    protected bool _wasFalling = false;
    protected bool _isCrouching = false;

    protected virtual void Start()
    {
        _playerTouchesFlyingPlatform = GetComponentInChildren<PlayerTouchesFlyingPlatform>();
        _playerSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _inventoryManager = GetComponent<InventoryManager>();
        _playerBoxCollider = GetComponent<BoxCollider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _inputManager = GetComponentInChildren<InputManager>();
        _basicAttackBox = GameObject.Find(StaticObjects.GetFindTags().CharacterBasicAttackBox).GetComponent<BoxCollider2D>();
        _showItems = StaticObjects.GetItemCanvas().GetComponent<ShowItems>();
        _playerHealth = GetComponent<Health>();
        _playerTouchesGround = GetComponentInChildren<PlayerTouchesGround>();
        _playerBasicAttack = GetComponent<PlayerBasicAttack>();
        _orientation = GetComponent<ActorOrientation>();
        _playerCroutchHitbox = GameObject.Find(StaticObjects.GetFindTags().CharacterCroutchedHitbox).GetComponent<BoxCollider2D>();
        _playerFloating = GameObject.Find(StaticObjects.GetFindTags().CharacterFloatingHitbox).GetComponent<PlayerFloatingInteraction>();
        _playerState = GetComponent<PlayerState>();

        _playerGroundMovement = GetComponent<PlayerGroundMovement>();

        _inputManager.OnMove += OnMove;
        _inputManager.OnJump += OnJump;
        _inputManager.OnJumpDown += OnJumpDown;
        _inputManager.OnUnderwaterControl += OnUnderwaterControl;
        _inputManager.OnIronBootsEquip += OnIronBootsEquip;
        _inputManager.OnStop += OnStop;
        _inputManager.OnCrouch += OnCrouch;
        _inputManager.OnStandingUp += OnStandingUp;
        _inputManager.OnFlip += Flip;

        _playerHealth.OnDamageTaken += OnStandingUpAfterHit;

        _rigidbody.gravityScale = INITIAL_GRAVITY_SCALE;
    }

    protected virtual void OnMove(Vector3 vector, bool goesRight)
    {
        if (PlayerCanMove())
        {
            MovePlayer(vector, goesRight);
            _playerState.SetMoving(_rigidbody.velocity.x);
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
        if (PlayerIsMovingHorizontally() && !_playerState.IsKnockedBack)
        {
            _rigidbody.velocity = Vector2.up * _rigidbody.velocity.y;
            _playerState.SetImmobile();
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

    protected void OnStandingUp()
    {
        if (_playerState.IsCroutching)
        {
            if (!PlayerIsMovingVertically())
            {
                
            }
            transform.position += Vector3.up * CROUTCH_Y_OFFSET;
            SetCroutch(false);
        }
    }

    protected void SetCroutch(bool enable)
    {
        _playerCroutchHitbox.enabled = enable;
        _playerBoxCollider.isTrigger = enable;
        _playerState.SetCroutching(enable);
    }

    protected void OnStandingUpAfterHit(int hitPoints)
    {
        OnStandingUp();
    }

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
        return _rigidbody.velocity.x != 0 || _playerState.IsMoving;
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
        return _playerTouchesFlyingPlatform.OnFlyingPlatform || PlayerIsOnEdgeOfFlyingPlatform();
    }

    private bool PlayerIsOnEdgeOfFlyingPlatform()
    {
        return !PlayerIsMovingHorizontally() && !PlayerTouchesGround() && _playerTouchesFlyingPlatform.HasFlyingPlatform();
    }

    private bool PlayerCanDropFromFlyingPlatform()
    {
        return !IsJumping() && !_playerState.IsKnockedBack && PlayerTouchesFlyingPlatform();
    }

    private bool PlayerCanMove()
    {
        return !_playerState.IsKnockedBack && !_playerState.IsCroutching && !_playerState.IsAttacking;
    }

    private bool PlayerIsAlmostStopped()
    {
        return PlayerIsAlmostStoppedAndIsFacingRight() || PlayerIsAlmostStoppedAndIsFacingLeft();
    }

    private bool PlayerIsAlmostStoppedAndIsFacingRight()
    {
        return _rigidbody.velocity.x < 1 && _orientation.IsFacingRight;
    }

    private bool PlayerIsAlmostStoppedAndIsFacingLeft()
    {
        return _rigidbody.velocity.x > -1 && !_orientation.IsFacingRight;
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

    private bool IsInJumpingStateAndIsJumping()
    {
        return PlayerIsJumping() == _playerState.IsJumping;
    }

    private bool IsInFallingStateAndIsFalling()
    {
        return PlayerIsFalling() == _playerState.IsFalling;
    }

    private void Update()
    {
        if (!IsInJumpingStateAndIsJumping())
        {
            _playerState.SetJumping();
        }
        else if (!IsInFallingStateAndIsFalling())
        {
            _playerState.SetFalling();
        }

        UpdateMovement();
    }

    protected void Flip(bool goesRight)
    {
        if (_orientation.Flip(goesRight))
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
