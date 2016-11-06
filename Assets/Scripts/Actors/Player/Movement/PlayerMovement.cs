using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    protected InputManager _inputManager;
    protected Rigidbody2D _rigidbody;
    protected BoxCollider2D _basicAttackBox;
    protected BoxCollider2D _playerBoxCollider;
    protected BoxCollider2D _playerBoxColliderFeet;
    protected BoxCollider2D _playerBoxColliderTorso;
    protected CircleCollider2D _playerCircleColliderTorso;
    protected SpriteRenderer _playerSpriteRenderer;
    protected InventoryManager _inventoryManager;
    protected Animator _anim;
    protected Transform _spriteTransform;
    protected ShowItems _showItems;
    protected GameObject _touchesGroundHitbox;

    public delegate void OnFallingHandler();
    public event OnFallingHandler OnFalling;
    public delegate void OnLandingHandler();
    public event OnLandingHandler OnLanding;

    protected const float INITIAL_GRAVITY_SCALE = 5;
    private const float PRECISION_MARGIN = 0.2f;
    protected const float TERMINAL_SPEED = -18;
    protected const float SPEED_REDUCTION_WHEN_STOPPING = 0.94f;
    protected const float LINEAR_DRAG = 30f;
    protected const float KNOCKBACK_DURATION = 15;
    protected const float PLAYER_COLLIDER_BOX_Y_SIZE_WHEN_STAND = 0.8622845f;
    protected const float PLAYER_COLLIDER_BOX_Y_OFFSET_WHEN_STAND = -0.008171797f;
    protected const float FEET_COLLIDER_BOX_Y_OFFSET_WHEN_STAND = -0.45f;
    protected const float TORSO_CIRCLE_COLLIDER_BOX_Y_OFFSET_WHEN_STAND = 0.21f;
    protected const float TORSO_BOX_COLLIDER_BOX_Y_OFFSET_WHEN_STAND = -0.4f;
    protected const float CROUCHING_OFFSET = 0.6f;
    protected const float CROUCHING_SPRITE_POSITION_OFFSET = 0.35f;

    protected float _speed = 7;
    protected float _jumpingSpeed = 17;
    protected bool _isKnockedBack = false;
    protected float _knockbackCount = 0;
    protected bool _canDoubleJump = false;
    protected bool _wasFalling = false;
    protected bool _isCrouching = false;

    public float Speed { get { return _speed; } set { _speed = value; } }
    public float JumpingSpeed { get { return _jumpingSpeed; } set { _jumpingSpeed = value; } }
    public bool IsKnockedBack { get { return _isKnockedBack; } set { _isKnockedBack = value; } }
    public bool IsCrouching { get { return _isCrouching; } set { _isCrouching = value; } }
    public float TerminalSpeed { get { return TERMINAL_SPEED; } }

    private void Start()
    {
        _anim = GameObject.Find("CharacterSprite").GetComponent<Animator>();
        _playerSpriteRenderer = GameObject.Find("CharacterSprite").GetComponent<SpriteRenderer>();
        _inventoryManager = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryManager>();
        _playerBoxCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>();
        _playerBoxColliderFeet = GameObject.Find("CharacterTouchesGround").GetComponent<BoxCollider2D>();
        _playerBoxColliderTorso = GameObject.Find("CharacterWaterHitbox").GetComponent<BoxCollider2D>();
        _playerCircleColliderTorso = GameObject.Find("CharacterFloatingHitbox").GetComponent<CircleCollider2D>();
        _spriteTransform = _anim.GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _inputManager = GetComponentInChildren<InputManager>();
        _basicAttackBox = GameObject.Find("CharacterBasicAttackBox").GetComponent<BoxCollider2D>();
        _showItems = GameObject.Find("ItemCanvas").GetComponent<ShowItems>();
        _touchesGroundHitbox = GameObject.Find("CharacterTouchesGround");

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

    protected virtual void OnMove(Vector3 vector, bool goesRight) { }

    protected virtual void OnJump() { }

    protected virtual void OnJumpDown() { }

    protected virtual void OnUnderwaterControl(bool goesDown) { }

    protected virtual void OnIronBootsEquip() { }

    protected virtual void OnStop() { }

    protected virtual void OnCrouch() { }

    protected virtual void OnStandingUp() { }

    protected virtual void UpdateMovement() { }

    public virtual void ChangeGravity() { }

    public virtual bool IsJumping()
    {
        return !((PlayerIsOnGround() || VerticalVelocityIsZeroOnFlyingPlatform() || PlayerIsNotMoving()) && !(VerticalVelocityIsPositive()));
    }

    private bool VerticalVelocityIsPositive()
    {
        return _rigidbody.velocity.y > 0;
    }

    private bool VerticalVelocityIsZeroOnFlyingPlatform()
    {
        return _touchesGroundHitbox.GetComponent<PlayerTouchesFlyingPlatform>().OnFlyingPlatform && _rigidbody.velocity.y == 0;
    }

    private bool PlayerIsNotMoving()
    {
        return _rigidbody.velocity == Vector2.zero;
    }

    private bool PlayerIsOnGround()
    {
        return PlayerTouchesGround() && !PlayerTouchesFlyingPlatform();
    }

    private bool PlayerTouchesGround()
    {
        return StaticObjects.GetPlayer().GetComponent<PlayerTouchesGround>().OnGround;
    }

    private bool PlayerTouchesFlyingPlatform()
    {
        return _touchesGroundHitbox.GetComponent<PlayerTouchesFlyingPlatform>().OnFlyingPlatform;
    }

    private void Update()
    {
        _anim.SetFloat("Speed", Mathf.Abs(_rigidbody.velocity.x));
        _anim.SetBool("IsJumping", IsJumping() && _rigidbody.velocity.y > 0);
        _anim.SetBool("IsFalling", IsJumping() && _rigidbody.velocity.y < 0);
        _anim.SetBool("IsCrouching", IsCrouching);

        if (IsJumping() && _rigidbody.velocity.y < 0)
        {
            _wasFalling = true;
            if (OnFalling != null)
            {
                OnFalling();
            }
        }
        else if (_wasFalling)
        {
            _wasFalling = false;
            if (OnLanding != null)
            {
                OnLanding();
            }
        }

        UpdateMovement();
    }

    protected void Flip(bool goesRight)
    {
        if (GetComponent<FlipPlayer>().Flip(goesRight))
        {
            _basicAttackBox.offset = new Vector2(_basicAttackBox.offset.x * -1, _basicAttackBox.offset.y);
            _spriteTransform.localScale = new Vector3(-1 * _spriteTransform.localScale.x, _spriteTransform.localScale.y, _spriteTransform.localScale.z);
        }
    }

    protected void ChangePlayerVerticalVelocity(float verticalVelocity)
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, verticalVelocity);
    }
}
