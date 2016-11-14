using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    protected InputManager _inputManager;
    protected Rigidbody2D _rigidbody;
    protected BoxCollider2D _basicAttackBox;
    protected BoxCollider2D _playerBoxCollider;
    protected SpriteRenderer _playerSpriteRenderer;
    protected InventoryManager _inventoryManager;
    protected PlayerTouchesGround _playerTouchesGround;
    protected Animator _anim;
    protected ShowItems _showItems;
    protected Health _playerHealth;
    protected ActorBasicAttack _playerBasicAttack;
    protected PlayerTouchesFlyingPlatform _playerTouchesFlyingPlatform;

    public delegate void OnFallingHandler();
    public event OnFallingHandler OnFalling;
    public delegate void OnLandingHandler();
    public event OnLandingHandler OnLanding;

    protected const float INITIAL_GRAVITY_SCALE = 5;
    protected const float TERMINAL_SPEED = -18;
    protected const float SPEED_REDUCTION_WHEN_STOPPING = 0.94f;
    protected const float LINEAR_DRAG = 30f;
    protected const float KNOCKBACK_DURATION = 0.25f;
    protected const float PLAYER_COLLIDER_BOX_Y_SIZE_WHEN_STAND = 0.8622845f;
    protected const float PLAYER_COLLIDER_BOX_Y_OFFSET_WHEN_STAND = -0.008171797f;
    protected const float FEET_COLLIDER_BOX_Y_OFFSET_WHEN_STAND = -0.45f;
    protected const float TORSO_CIRCLE_COLLIDER_BOX_Y_OFFSET_WHEN_STAND = 0.21f;
    protected const float TORSO_BOX_COLLIDER_BOX_Y_OFFSET_WHEN_STAND = -0.4f;
    protected const float ATTACK_BOX_COLLIDER_Y_WHEN_STAND = 3.415288f;
    protected const float CROUCHING_OFFSET = 0.6f;
    protected const float CROUCHING_SPRITE_POSITION_OFFSET = 0.35f;
    protected const float TIME_TO_WAIT_BEFORE_CROUCH_ALLOWED = 0.3f;

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
        _anim = StaticObjects.GetPlayer().GetComponentInChildren<Animator>();
        _playerTouchesFlyingPlatform = StaticObjects.GetPlayer().GetComponentInChildren<PlayerTouchesFlyingPlatform>();
        _playerSpriteRenderer = StaticObjects.GetPlayer().GetComponentInChildren<SpriteRenderer>();
        _inventoryManager = StaticObjects.GetPlayer().GetComponent<InventoryManager>();
        _playerBoxCollider = StaticObjects.GetPlayer().GetComponent<BoxCollider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _inputManager = GetComponentInChildren<InputManager>();
        _basicAttackBox = GameObject.Find("CharacterBasicAttackBox").GetComponent<BoxCollider2D>();
        _showItems = StaticObjects.GetItemCanvas().GetComponent<ShowItems>();
        _playerHealth = StaticObjects.GetPlayer().GetComponent<Health>();
        _playerTouchesGround = GetComponentInChildren<PlayerTouchesGround>();
        _playerBasicAttack = StaticObjects.GetPlayer().GetComponent<ActorBasicAttack>();
        
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

    protected bool _stoppedEnoughToCrouch = false;

    protected virtual void OnMove(Vector3 vector, bool goesRight)
    {
        if (_playerBasicAttack.IsAttacking())
        {
            OnStop();
        }
        else if (!IsKnockedBack && !IsCrouching)
        {
            _stoppedEnoughToCrouch = false;
            _rigidbody.velocity = new Vector2(vector.x * _speed, _rigidbody.velocity.y);
            Flip(goesRight);
        }
    }

    protected virtual void OnJumpDown()
    {
        if (!IsJumping() && !_isKnockedBack && _playerTouchesFlyingPlatform.OnFlyingPlatform)
        {
            _playerTouchesFlyingPlatform.DisablePlatformHitbox();
        }
    }

    protected virtual void OnStop()
    {
        if (!_stoppedEnoughToCrouch)
        {
            StartCoroutine("CountTimeSincePlayerStoppedCoroutine");
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
        return !((PlayerIsOnGround() || VerticalVelocityIsZeroOnFlyingPlatform() || PlayerIsNotMoving()) && !(VerticalVelocityIsPositive()));
    }

    private bool VerticalVelocityIsPositive()
    {
        return _rigidbody.velocity.y > 0;
    }

    private bool VerticalVelocityIsZeroOnFlyingPlatform()
    {
        return _playerTouchesFlyingPlatform.OnFlyingPlatform && _rigidbody.velocity.y == 0;
    }

    private bool PlayerIsNotMoving()
    {
        return _rigidbody.velocity == Vector2.zero;
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

    // À modifier absolument
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
        Transform animTransform = _anim.GetComponent<Transform>();
        if (GetComponent<FlipPlayer>().Flip(goesRight))
        {
            _basicAttackBox.offset = new Vector2(_basicAttackBox.offset.x * -1, _basicAttackBox.offset.y);
            animTransform.localScale = new Vector3(-1 * animTransform.localScale.x, animTransform.localScale.y, animTransform.localScale.z);
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

    protected IEnumerator CountTimeSincePlayerStoppedCoroutine()
    {
        float counter = 0;
        while (counter < TIME_TO_WAIT_BEFORE_CROUCH_ALLOWED)
        {
            counter += Time.deltaTime;
            yield return null;
        }
        _stoppedEnoughToCrouch = true;
    }
}
