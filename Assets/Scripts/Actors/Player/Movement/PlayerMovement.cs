using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    protected InputManager _inputManager;
    protected Rigidbody2D _rigidbody;
    protected BoxCollider2D _basicAttackBox;
    protected InventoryManager _inventoryManager;
    protected Animator _anim;
    protected Transform _spriteTransform;
    protected ShowItems _showItems;

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
        _inventoryManager = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryManager>();
        _spriteTransform = _anim.GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _inputManager = GetComponent<InputManager>();
        _basicAttackBox = GameObject.Find("CharacterBasicAttackBox").GetComponent<BoxCollider2D>();
        _showItems = GameObject.Find("SelectedWeaponCanvas").GetComponent<ShowItems>();

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

    public virtual bool IsJumping()
    {
        return !(((GameObject.Find("CharacterTouchesGround").GetComponent<PlayerTouchesGround>().OnGround
                && !GameObject.Find("CharacterTouchesGround").GetComponent<PlayerTouchesFlyingPlatform>().OnFlyingPlatform)
                || (GameObject.Find("CharacterTouchesGround").GetComponent<PlayerTouchesFlyingPlatform>().OnFlyingPlatform && _rigidbody.velocity.y == 0)
                || _rigidbody.velocity == Vector2.zero)
                && !(_rigidbody.velocity.y > 0));
    }

    private void Update()
    {
        _anim.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal")));
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
